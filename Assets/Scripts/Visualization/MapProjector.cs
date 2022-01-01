using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace RosSharp.RosBridgeClient
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class MapProjector : MonoBehaviour
    {
        public Transform MapOrigin;
        public Vector3 mapOffset;
        public float PositionThreshold = 0.1f;
        public float RotationThreshold = 0.3f;

        private float resolution;
        private float width;
        private float height;
        private sbyte[] _receivedData;
        private sbyte[] _currentData;

        [SerializeField]
        private Material _occupiedMaterial;

        [SerializeField]
        private Material _vacantMaterial;

        [SerializeField]
        private Color openColor;

        [SerializeField]
        private Color occupliedColor;

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

                DestroyChildren();

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

                    //need to separate into 2 groups for both data 0 and 100 and combine the meshes separately and apply the cooresponding material.
                    // When spawning the quad assign to specific parent. 


                    if (_currentData[i] != -1)
                    {
                        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                      
                        quad.transform.parent = MapOrigin.transform;
                        quad.transform.name = i.ToString();
                        quad.transform.localScale = Vector3.one * resolution;
                        quad.transform.position = current + mapOffset;
                        quad.transform.eulerAngles = new Vector3(90, transform.eulerAngles.y, transform.eulerAngles.z);
                        //quad.GetComponent<MeshRenderer>().material = _vacantMaterial;
                        //quad.GetComponent<MeshRenderer>().material.color = Color.Lerp(openColor, occupliedColor, _currentData[i] / 100);
                    }

                    current += x_inc;
                    widthCounter++;
                }
             
                //Place this set of lines in separate script and assign it to the child of MapOrigin. Change Destroy Objects function.
                MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
                CombineInstance[] combine = new CombineInstance[meshFilters.Length];

                int j = 0;
                while(j < meshFilters.Length)
                {
                    combine[j].mesh = meshFilters[j].sharedMesh;
                    combine[j].transform = meshFilters[j].transform.localToWorldMatrix;
                    meshFilters[j].gameObject.SetActive(false);
                    j++; 
                }

                var meshFilter = transform.GetComponent<MeshFilter>();
                meshFilter.mesh = new Mesh();
                meshFilter.mesh.CombineMeshes(combine);
                transform.gameObject.SetActive(true);
                transform.localScale = new Vector3(1,1,1);
                transform.rotation = Quaternion.identity;
                transform.position = Vector3.zero;
                transform.GetComponent<MeshRenderer>().enabled = true;
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

        private void DestroyChildren()
        {
            foreach (Transform child in MapOrigin.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}
