using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class LaptopChargeSubscriber : UnitySubscriber<MessageTypes.Sensor.BatteryState>
    {
        public LaptopProjector laptopProjector;

        protected override void Start()
        {
            base.Start();
        }

        protected override void ReceiveMessage(MessageTypes.Sensor.BatteryState message)
        {
            laptopProjector.Write(message);
        }
    }
}
