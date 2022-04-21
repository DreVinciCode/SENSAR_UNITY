using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class PoseCovarianceStampedSubscriber : UnitySubscriber<MessageTypes.Geometry.PoseWithCovarianceStamped>
    {
        public PoseCovarianceStampedProcessor poseCovarianceProcessor;

        protected override void Start()
        {
            base.Start();
        }

        protected override void ReceiveMessage(MessageTypes.Geometry.PoseWithCovarianceStamped message)
        {
            poseCovarianceProcessor.Write(message);
        }
    }
}
