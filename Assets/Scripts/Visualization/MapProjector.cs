using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class MapProjector : MonoBehaviour
    {
        public Transform OccupiedMesh;
        public Transform VacantMesh;
        public Transform MapOrigin;
        public Vector3 mapOffset;
        public float PositionThreshold = 0.1f;
        public float RotationThreshold = 0.3f;

        private float resolution;
        private float width;
        private float height;
        private sbyte[] _receivedData;
        private sbyte[] _currentData;
        private Vector3 _receivedPosition;
        private Quaternion _receivedRotation;
        private Vector3 _currentPosition;
        private Quaternion _currentRotation;
        private bool isMessageReceived;

        private void Start()
        {
            _currentPosition = Vector3.zero;
            _currentRotation = Quaternion.identity;
            _currentData = new sbyte[0];
        }

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
            _receivedData = message.data;
            _receivedPosition = GetPosition(message).Ros2Unity();
            _receivedRotation = GetRotation(message).Ros2Unity();
            isMessageReceived = true;
        }

        private void ProcessMessage()
        {            
            if (((Vector3.Distance(_currentPosition, _receivedPosition) > PositionThreshold) || CompareQuaterions(_currentRotation,_receivedRotation, RotationThreshold)) && _receivedData.SequenceEqual<sbyte>(_currentData))
            {
                _currentPosition = _receivedPosition;
                _currentRotation = _receivedRotation;

                MapOrigin.position = _currentPosition;
                MapOrigin.rotation = _currentRotation;
            }
            else if (!_receivedData.SequenceEqual<sbyte>(_currentData))
            {
                _currentData = _receivedData;
                _currentPosition = _receivedPosition;
                _currentRotation = _receivedRotation;

                MapOrigin.position = _receivedPosition;
                MapOrigin.rotation = _receivedRotation;

                Vector3 xAxis = MapOrigin.transform.forward.normalized;
                Vector3 zAxiz = -1 * MapOrigin.transform.right.normalized;
                Vector3 x_inc = resolution * xAxis;
                Vector3 z_inc = resolution * zAxiz;
                Vector3 origin = MapOrigin.transform.position;
                Vector3 current = origin;

                var widthCounter = 0;

                for (int i = 0; i < _currentData.Length; i++)
                {
                    if (widthCounter == width)
                    {
                        current += z_inc;
                        current -= x_inc * widthCounter;
                        widthCounter = 0;
                    }

                    if(_currentData[i] == 0)
                    {
                        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                        quad.transform.parent = VacantMesh.transform;
                        quad.transform.name = i.ToString();
                        quad.transform.localScale = Vector3.one * resolution;
                        quad.transform.position = current;
                        quad.transform.eulerAngles = new Vector3(90, transform.eulerAngles.y, transform.eulerAngles.z);
                    }
                    else if (_currentData[i] == 100)
                    {
                        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                        quad.transform.parent = OccupiedMesh.transform;
                        quad.transform.name = i.ToString();
                        quad.transform.localScale = Vector3.one * resolution;
                        quad.transform.position = current;
                        quad.transform.eulerAngles = new Vector3(90, transform.eulerAngles.y, transform.eulerAngles.z);
                    }

                    current += x_inc;
                    widthCounter++;
                }

                OccupiedMesh.GetComponent<CombinedMeshes>().MergeMeshes();
                VacantMesh.GetComponent<CombinedMeshes>().MergeMeshes();
                DestroyChildren(OccupiedMesh);
                DestroyChildren(VacantMesh);
            }

            isMessageReceived = false;
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

        public static bool CompareQuaterions(Quaternion quatA, Quaternion value, float acceptableRange)
        {
            return 1 - Mathf.Abs(Quaternion.Dot(quatA, value)) > acceptableRange;
        }

        private void DestroyChildren(Transform parent)
        {
            foreach (Transform child in parent)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}
