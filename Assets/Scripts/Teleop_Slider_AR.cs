using Microsoft.MixedReality.Toolkit.UI;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RosSharp.RosBridgeClient
{
    public class Teleop_Slider_AR : UnityPublisher<MessageTypes.Geometry.Twist>
    {

        [SerializeField] private float _moveSpeed = 1;
        [SerializeField] private PinchSlider MoveSlider;
        [SerializeField] private PinchSlider TurnSlider;

        private float sliderOffset = 0.5f;
        private MessageTypes.Geometry.Twist message;

        public bool _publishMessageCheck { get; set; }


        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.Geometry.Twist();
            message.linear = new MessageTypes.Geometry.Vector3();
            message.angular = new MessageTypes.Geometry.Vector3();
        }

        private void PublishMessage()
        {
            Debug.Log("slider value: " + (MoveSlider.SliderValue - sliderOffset));
            Vector3 linearVelocity = new Vector3(-1*(MoveSlider.SliderValue - sliderOffset) * _moveSpeed, 0f, 0f);
            Vector3 angularVelocity = new Vector3(0f, 0f, (TurnSlider.SliderValue - sliderOffset) * _moveSpeed);

            message.linear = GetGeometryVector3(linearVelocity);
            message.angular = GetGeometryVector3(-angularVelocity);

            Publish(message);
        }

        private void FixedUpdate()
        {
            if (_publishMessageCheck)
            {
                //if (MoveSlider.SliderValue - sliderOffset != 0.0f && TurnSlider.SliderValue - sliderOffset != 0.0f)
                PublishMessage();
            }
        }

        private static MessageTypes.Geometry.Vector3 GetGeometryVector3(Vector3 vector3)
        {
            MessageTypes.Geometry.Vector3 geometryVector3 = new MessageTypes.Geometry.Vector3();
            geometryVector3.x = vector3.x;
            geometryVector3.y = vector3.y;
            geometryVector3.z = vector3.z;
            return geometryVector3;
        }
    }
}
