using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class PolygonFootprintSubscriber : UnitySubscriber<MessageTypes.Geometry.PolygonStamped>
    {
        public PolygonFootprintProjector polygonProjector;

        protected override void Start()
        {
            base.Start();
        }

        protected override void ReceiveMessage(MessageTypes.Geometry.PolygonStamped message)
        {
            polygonProjector.Write(message);
        }
    }
}

