using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class PolygonProjector : MonoBehaviour
    {
        private bool _isMessageReceived;
        private MessageTypes.Geometry.Polygon _polygon;
        private MessageTypes.Geometry.Point32[] _points;
        private int _totalPoints;

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
            //find a way to display a closed polygon with the given points. 
            // round the points in order to check for any differences. 

            Debug.Log("Total points: " + _totalPoints);

            _isMessageReceived = false;
        }
    }
}