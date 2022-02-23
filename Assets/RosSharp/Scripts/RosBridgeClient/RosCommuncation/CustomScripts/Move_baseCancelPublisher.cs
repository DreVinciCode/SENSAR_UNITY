using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class Move_baseCancelPublisher : UnityPublisher<MessageTypes.Actionlib.GoalID>
    {
        private MessageTypes.Actionlib.GoalID message;

        protected override void Start()
        {
            base.Start();
        }

        public void CancelPlan()
        {
            message = new MessageTypes.Actionlib.GoalID();    
            Publish(message);
        }
    }
}
