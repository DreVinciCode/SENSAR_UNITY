using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class PoseCovarianceStampedProcessor : MonoBehaviour
    {
        public Vector3 _position;
        public Quaternion _rotation;
        private bool isMessageReceived;

        private void Update()
        {
            if (isMessageReceived)
                ProcessMessage();
        }

        public void Write(MessageTypes.Geometry.PoseWithCovarianceStamped message)
        {
            var point = message.pose.pose.position;
            var rotation = message.pose.pose.orientation;
            _position = new Vector3((float)point.x, (float)point.y, (float)point.z).Ros2Unity();
            _rotation = new Quaternion((float)rotation.x, (float)rotation.y, (float)rotation.z, (float)rotation.w).Ros2Unity();
            isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            isMessageReceived = false;
        }
    }
}

