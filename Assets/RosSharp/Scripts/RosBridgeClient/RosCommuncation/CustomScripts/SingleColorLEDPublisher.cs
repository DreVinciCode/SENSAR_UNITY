using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class SingleColorLEDPublisher : UnityPublisher<MessageTypes.SENSAR.SingleColorLED>
    {
        private MessageTypes.SENSAR.SingleColorLED message;
        private MessageTypes.Std.ColorRGBA _color;
        private float _milliseconds;
        private sbyte[] _leds;
        private bool _repeat;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.SENSAR.SingleColorLED();
            _color = new MessageTypes.Std.ColorRGBA();
        }

        public void UpdateMessage()
        {
            _color.r = 0;
            _color.g = 0;
            _color.b = 0;
            _color.a = 0;

            message.color = _color;
            message.duration = _milliseconds;
            message.leds = _leds;
            message.repeating = _repeat;

            Publish(message);
        }
    }
}
