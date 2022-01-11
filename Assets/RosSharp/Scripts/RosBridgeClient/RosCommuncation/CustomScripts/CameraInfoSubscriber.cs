using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class CameraInfoSubscriber : UnitySubscriber<MessageTypes.Sensor.CameraInfo>
    {
        public CameraProjector CameraProjector;

        protected override void Start()
        {
            base.Start();
        }

        protected override void ReceiveMessage(MessageTypes.Sensor.CameraInfo message)
        {
            CameraProjector.WriteCameraInfo(message);
        }
    }
}
