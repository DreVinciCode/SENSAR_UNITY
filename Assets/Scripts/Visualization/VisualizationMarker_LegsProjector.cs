using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class VisualizationMarker_LegsProjector : MonoBehaviour
    {
        public Vector3 Offset;
        public GameObject LegObject;
        public Transform _parent;

        private int _totalCount;
        private bool _isMessageReceived;
        private MessageTypes.Visualization.Marker[] _markers;

        private void Update()
        {
            if (_isMessageReceived)
                ProcessMessage();
        }

        public void Write(MessageTypes.Visualization.MarkerArray message)
        { 
            _markers = message.markers;
            _totalCount = _markers.Length;

            _isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            DestroyChildren(_parent);

            for (int i = 0; i < _totalCount; i++)
            {
                var leg = _markers[i];
                var position = GetPosition(leg.pose).Ros2Unity();
                var LegMarker = Instantiate(LegObject, _parent.transform);
                LegMarker.transform.localPosition = position;
                LegMarker.name = leg.text;
            }

            _isMessageReceived = false;
        }

        private Vector3 GetPosition(MessageTypes.Geometry.Pose message)
        {
            return new Vector3(
                (float)message.position.x,
                (float)message.position.y,
                (float)message.position.z);
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

