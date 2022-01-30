using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class PolygonSafetyzoneProjector : MonoBehaviour
    {
        [SerializeField] LineRenderer lineRenderer;
        private bool _isMessageReceived;
        private MessageTypes.Geometry.Polygon _polygon;
        private MessageTypes.Geometry.Point32[] _points;
        private int _totalPoints;

        private void Start()
        {
            lineRenderer.startWidth = 0.005f;
        }

        private void Update()
        {
            if (_isMessageReceived)
                ProcessMessage();
        }

        public void Write(MessageTypes.Geometry.PolygonStamped message)
        {
            _polygon = message.polygon;
            _points = _polygon.points;
            _totalPoints = _points.Length;
            _isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            lineRenderer.positionCount = _totalPoints + 1;
            Vector3[] waypoints = new Vector3[_totalPoints + 1];

            for (int i = 0; i < _totalPoints; i++)
            {
                var vertex = _points[i];
                var position = new Vector3((float)vertex.x, (float)vertex.y, (float)vertex.z).Ros2Unity();
                waypoints[i] = position;
            }

            waypoints[_totalPoints] = new Vector3((float)_points[0].x, (float)_points[0].y, (float)_points[0].z).Ros2Unity();
            lineRenderer.SetPositions(waypoints);
            lineRenderer.enabled = true;
            _isMessageReceived = false;
        }
    }
}