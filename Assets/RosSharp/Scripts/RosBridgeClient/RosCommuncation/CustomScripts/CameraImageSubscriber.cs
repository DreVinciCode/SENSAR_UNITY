using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class CameraImageSubscriber : UnitySubscriber<MessageTypes.Sensor.CompressedImage>
    {
        public CameraProjector CameraProjector;

        protected override void Start()
        {
            base.Start();
        }

        protected override void ReceiveMessage(MessageTypes.Sensor.CompressedImage message)
        {
            CameraProjector.WriteCameraImage(message);
        }
    }
}
