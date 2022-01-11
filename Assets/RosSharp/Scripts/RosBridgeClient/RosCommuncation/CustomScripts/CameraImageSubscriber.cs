using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class CameraImageSubscriber : UnitySubscriber<MessageTypes.Sensor.Image>
    {
        public CameraProjector CameraProjector;

        protected override void Start()
        {
            base.Start();
        }

        protected override void ReceiveMessage(MessageTypes.Sensor.Image message)
        {
            CameraProjector.WriteCameraImage(message);
        }
    }
}
