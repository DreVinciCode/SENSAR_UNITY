using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class RGBAPublisher : UnityPublisher<MessageTypes.Std.ColorRGBA>
    {
        private MessageTypes.Std.ColorRGBA message;
        private float _r_value;
        private float _g_value;
        private float _b_value;

        protected override void Start()
        {
            base.Start();
        }

        public void PublishColorMessage(int state_color)
        {
            message = new MessageTypes.Std.ColorRGBA();

            switch (state_color)
            {
                case 0: //red
                    message.r = 255f;
                    message.g = 0;
                    message.b = 0; 
                    break;

                case 1: //blue
                    message.r = 0;
                    message.g = 0;
                    message.b = 255f;
                    break;

                case 2: //green
                    message.r = 0;
                    message.g = 255f;
                    message.b = 0;
                    break;

                case 3: //white
                    message.r = 255f;
                    message.g = 255f;
                    message.b = 255f;
                    break;

                case 4: // yellow
                    message.r = 255f;
                    message.g = 255f;
                    message.b = 0;
                    break;
            }

            Publish(message);
        }
    }
}
