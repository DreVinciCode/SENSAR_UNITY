using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class StateSubscriber : UnitySubscriber<MessageTypes.Sensor.BatteryState>
    {
        public StateProjector stateProjector;

        protected override void Start()
        {
            base.Start();
        }

        protected override void ReceiveMessage(MessageTypes.Sensor.BatteryState message)
        {
            stateProjector.Write(message);
        }
    }
}
