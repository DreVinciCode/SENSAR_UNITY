using TMPro;
using UnityEngine;


namespace RosSharp.RosBridgeClient
{
    public class VisualizationMarker_PeopleProjector : MonoBehaviour
    {
        public Vector3 Offset;
        public GameObject PersonObject;
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
                var person = _markers[i];
                var duration = person.lifetime;
                var position = GetPosition(person.pose).Ros2Unity();
                var PersonMarker = Instantiate(PersonObject, _parent.transform);
                var reliability_text = PersonMarker.GetComponentInChildren<TMP_Text>();         
                //reliability_text.text = System.Math.Round(float.Parse(person.text), 2).ToString();
                PersonMarker.transform.localPosition = position + Offset;
                PersonMarker.name = person.text;
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

