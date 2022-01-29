using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class PolygonSubscriber : UnitySubscriber<MessageTypes.Geometry.PolygonStamped>
    {
        public PolygonProjector footprintProjector;

        protected override void Start()
        {
            base.Start();
        }

        protected override void ReceiveMessage(MessageTypes.Geometry.PolygonStamped message)
        {
            footprintProjector.Write(message);
        }
    }
}

