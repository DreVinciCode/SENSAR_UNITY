using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class PointStampSubscriber : UnitySubscriber<MessageTypes.Geometry.PointStamped>
    {
        public PointStampProjector pointStampProjector;

        protected override void Start()
        {
            base.Start();
        }

        protected override void ReceiveMessage(MessageTypes.Geometry.PointStamped message)
        {
            pointStampProjector.Write(message);
        }
    }
}

