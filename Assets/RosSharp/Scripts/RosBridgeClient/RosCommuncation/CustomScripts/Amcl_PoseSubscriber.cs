using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class Amcl_PoseSubscriber : UnitySubscriber<MessageTypes.Geometry.PoseWithCovarianceStamped>
    {
        public Amcl_PoseProjector poseProjector;

        protected override void Start()
        {
            base.Start();
        }

        protected override void ReceiveMessage(MessageTypes.Geometry.PoseWithCovarianceStamped message)
        {
            poseProjector.Write(message);
        }
    }
}
