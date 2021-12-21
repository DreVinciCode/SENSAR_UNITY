using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RosSharp.RosBridgeClient
{
    public class MapProjector : MonoBehaviour
    {
        public Transform MapOrigin;
        public Vector3 mapOffset;

        private float resolution;
        private float width;
        private float height;
        private sbyte[] data;

        [SerializeField]
        private Material _mappingMaterial;

        [SerializeField]
        private Color openColor;

        [SerializeField]
        private Color occupliedColor;

        private Vector3 position;
        private Quaternion rotation;
        private bool isMessageReceived;

        private void Update()
        {
            if (isMessageReceived)
                ProcessMessage();
        }

        public void Write(MessageTypes.Nav.OccupancyGrid message)
        {
            resolution = message.info.resolution;
            width = message.info.width;
            height = message.info.height;
            data = message.data;

            position = GetPosition(message).Ros2Unity();
            rotation = GetRotation(message).Ros2Unity();

            isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            MapOrigin.position = position;
            MapOrigin.rotation = rotation;

            DestroyChildren();

            var rotation_angle = rotation.eulerAngles.y;
            //Debug.Log(rotation_angle);

            Vector3 xAxis = MapOrigin.transform.forward.normalized;
            Vector3 zAxiz = -1 * MapOrigin.transform.right.normalized;

            Vector2 x_axis = new Vector2(1, 0);
            x_axis = Quaternion.AngleAxis(rotation_angle, Vector3.up) * x_axis;

            Vector2 y_axis = Vector2.Perpendicular(x_axis);

            Vector3 origin = MapOrigin.transform.position;

            Vector3 x_inc = resolution * xAxis;
            Vector3 z_inc = resolution * zAxiz;

            Vector3 current = origin;

            var widthCounter = 0;

            for (int i = 0; i < data.Length; i++)
            {
                if(widthCounter == width)
                {
                    current += z_inc;
                    current -= x_inc * widthCounter;
                    widthCounter = 0;
                }

                if(data[i] != -1 && i % 2 == 0)
                {
                    GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    quad.transform.parent = MapOrigin.transform;
                    quad.transform.localScale = Vector3.one * resolution;
                    quad.transform.position = current + mapOffset;
                    //quad.transform.rotation = rotation;
                    quad.transform.eulerAngles = new Vector3(90, transform.eulerAngles.y, transform.eulerAngles.z);
                    //quad.transform.localRotation = rotation;
                    quad.GetComponent<MeshRenderer>().material = _mappingMaterial;
                    quad.GetComponent<MeshRenderer>().material.color = Color.Lerp(openColor, occupliedColor, data[i] / 100);   
                }
   
                current += x_inc;

                widthCounter++;
            }
        }

        private Vector3 GetPosition(MessageTypes.Nav.OccupancyGrid message)
        {
            return new Vector3(
                (float)message.info.origin.position.x,
                (float)message.info.origin.position.y,
                (float)message.info.origin.position.z);
        }

        private Quaternion GetRotation(MessageTypes.Nav.OccupancyGrid message)
        {
            return new Quaternion(
                (float)message.info.origin.orientation.x,
                (float)message.info.origin.orientation.y,
                (float)message.info.origin.orientation.z,
                (float)message.info.origin.orientation.w);
        }

        private void DestroyChildren()
        {
            foreach (Transform child in MapOrigin.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}
