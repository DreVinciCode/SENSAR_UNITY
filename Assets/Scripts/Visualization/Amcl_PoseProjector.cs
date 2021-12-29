using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class Amcl_PoseProjector : MonoBehaviour
    {
        public Transform PublishedTransform;

        private Vector3 position;
        private Quaternion rotation;
        private bool isMessageReceived;

        private void Update()
        {
            if (isMessageReceived)
                ProcessMessage();
        }

        private void ProcessMessage()
        {
            PublishedTransform.position = position;

            var yaw = rotation.eulerAngles.y;
            var rot = new Vector3(0f, yaw, 0f);
            PublishedTransform.eulerAngles = rot;

  
            isMessageReceived = false;
        }

        public void Write(MessageTypes.Geometry.PoseWithCovarianceStamped message)
        {
            position = GetPosition(message).Ros2Unity();
            rotation = GetRotation(message).Ros2Unity();
            isMessageReceived = true;
        }

        private Vector3 GetPosition(MessageTypes.Geometry.PoseWithCovarianceStamped message)
        {
            return new Vector3(
                (float)message.pose.pose.position.x,
                (float)message.pose.pose.position.y,
                (float)message.pose.pose.position.z);
        }

        private Quaternion GetRotation(MessageTypes.Geometry.PoseWithCovarianceStamped message)
        {
            return new Quaternion(
                (float)message.pose.pose.orientation.x,
                (float)message.pose.pose.orientation.y,
                (float)message.pose.pose.orientation.z,
                (float)message.pose.pose.orientation.w);
        }
    }
}
