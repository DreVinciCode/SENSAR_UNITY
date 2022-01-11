using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class RobotStateSubscriber : UnitySubscriber<MessageTypes.Diagnostic.DiagnosticArray>
    {
        public RobotStateProjector robotStateProjector;

        protected override void Start()
        {
            base.Start();
        }

        protected override void ReceiveMessage(MessageTypes.Diagnostic.DiagnosticArray message)
        {
            robotStateProjector.Write(message);
        }
    }
}
