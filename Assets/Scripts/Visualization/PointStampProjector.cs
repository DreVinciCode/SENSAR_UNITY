using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class PointStampProjector : MonoBehaviour
    {
        [SerializeField] GameObject PointObject;
        public Transform _parent;
        public Transform _goalParent;

        private Vector3 _worldPosition;
        private bool _isMessageReceived;
        private MessageTypes.Geometry.Point _point;
        private MessageTypes.Geometry.Point _PreviousPoint;

        public bool _objectDetected { get; set; }
        private bool _objectPlaced;

        private void Start()
        {
            _objectPlaced = false;
            _objectDetected = false;
            _PreviousPoint.x = 0;
            _PreviousPoint.y = 0;
            _PreviousPoint.z = 0;
        }


        private void Update()
        {
            if (_isMessageReceived)
                ProcessMessage();
        }

        public void Write(MessageTypes.Geometry.PointStamped message)
        {
            _point = message.point;
            _isMessageReceived = true;
            
        }

        private void ProcessMessage()
        {
            DestroyChildren(_goalParent);
            DestroyChildren(_parent);

            var position = new Vector3((float)_point.x, (float)_point.y, (float)_point.z).Ros2Unity();
            
            var _pointObject =  Instantiate(PointObject, _parent.transform);
            _pointObject.transform.localPosition = position;

            _worldPosition = _pointObject.transform.position;

            var _tempObject = Instantiate(PointObject, _goalParent);
            _tempObject.transform.position = _worldPosition;

            Debug.Log("Object Placed");
            _objectPlaced = true;

            _isMessageReceived = false;
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
