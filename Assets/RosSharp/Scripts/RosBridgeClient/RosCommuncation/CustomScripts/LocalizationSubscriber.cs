using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class LocalizationSubscriber : UnitySubscriber<MessageTypes.Geometry.PoseArray>
    {
        public LocalizationProjector particleProjector;

        protected override void Start()
        {
            base.Start();
        }

        protected override void ReceiveMessage(MessageTypes.Geometry.PoseArray message)
        {
            particleProjector.Write(message);
        }
    }
}
