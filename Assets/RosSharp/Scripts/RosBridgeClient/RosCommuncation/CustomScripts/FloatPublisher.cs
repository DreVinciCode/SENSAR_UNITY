using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class FloatPublisher : UnityPublisher<MessageTypes.Std.Float64>
    {
        private MessageTypes.Std.Float64 message;
        private float value = 0.0f; 

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.Std.Float64();   
        }

        public  void UpdateMessage()
        {
            message.data = value;
            Publish(message);
        }

        //create functions to increase and decrease float value. 
    }
}
