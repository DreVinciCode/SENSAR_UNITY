using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class PolygonSafetyzoneSubscriber : UnitySubscriber<MessageTypes.Geometry.PolygonStamped>
    {
        public PolygonSafetyzoneProjector polygonProjector;

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

