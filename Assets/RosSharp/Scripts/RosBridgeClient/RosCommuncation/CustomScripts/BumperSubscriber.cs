using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class BumperSubscriber : UnitySubscriber<MessageTypes.Sensor.PointCloud2>
    {
        public BumperProjector bumperProjector;

        protected override void Start()
        {
            base.Start();
        }

        protected override void ReceiveMessage(MessageTypes.Sensor.PointCloud2 message)
        {
            bumperProjector.Write(message);
        }
    }
}
