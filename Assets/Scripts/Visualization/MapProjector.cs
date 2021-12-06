using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RosSharp.RosBridgeClient
{
    public class MapProjector : MonoBehaviour
    {
        public Transform MapOrigin;

        private float resolution;
        private float width;
        private float height;
        private sbyte[] data;

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


            
            var rotation_angle = rotation.eulerAngles.z;
            Vector2 x_axis = new Vector2(1, 0);
            Vector2 y_axis = Vector2.Perpendicular(x_axis);

            x_axis = Quaternion.AngleAxis(rotation_angle, Vector3.forward) * x_axis;

            Vector3 origin = new Vector3(position.x, position.y, position.z);
            Vector3 x_inc = resolution * Vector3.Normalize(new Vector3(x_axis.x, x_axis.y, 0));
            Vector3 y_inc = resolution * Vector3.Normalize(new Vector3(y_axis.x, y_axis.y, 0));

            Vector3 current = origin;

            var widthCounter = 0;

            for (int i = 0; i < data.Length; i++)
            {
                if(widthCounter == width)
                {
                    current += y_inc;
                    current -= x_inc * widthCounter;
                    widthCounter = 0;
                }

                GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                quad.transform.parent = MapOrigin.transform;
                quad.transform.localScale = Vector3.one * resolution;
                quad.transform.localPosition = current;

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
