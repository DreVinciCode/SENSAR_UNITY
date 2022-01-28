using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class PointStampProjector : MonoBehaviour
    {
        [SerializeField] GameObject PointObject;

        public Transform _parent;

        private bool _isMessageReceived;
        private MessageTypes.Geometry.Point _point;

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
            DestroyChildren(_parent);

            var position = new Vector3((float)_point.x, (float)_point.y, (float)_point.z).Ros2Unity();
            var _pointObject =  Instantiate(PointObject, _parent.transform);
            _pointObject.transform.localPosition = position;

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
