using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RosSharp.RosBridgeClient
{
    public class StateProjector : MonoBehaviour
    {
        private bool _isMessageReceived;
        private float _percentage;

        private void Update()
        {
            if (_isMessageReceived)
                ProcessMessage();
        }

        public void Write(MessageTypes.Sensor.BatteryState message)
        {
            _percentage = message.percentage;
            _isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            //apply radial progress bar


            _isMessageReceived = false;
        }
    }
}