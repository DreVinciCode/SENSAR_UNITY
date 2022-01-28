using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class FootprintSubscriber : UnitySubscriber<MessageTypes.Geometry.PolygonStamped>
    {
        public FootprintProjector footprintProjector;

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

