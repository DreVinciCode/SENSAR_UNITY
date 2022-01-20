using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class PointCloudSubscriber : UnitySubscriber<MessageTypes.Sensor.PointCloud2>
    {
        public PointCloudProjector pointCloudProjector;

        protected override void Start()
        {
            base.Start();
        }

        protected override void ReceiveMessage(MessageTypes.Sensor.PointCloud2 message)
        {
            pointCloudProjector.Write(message);
        }
    }
}
