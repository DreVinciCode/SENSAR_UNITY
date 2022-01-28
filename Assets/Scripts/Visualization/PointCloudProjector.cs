using System;
using UnityEngine;
using UnityEngine.UI;

namespace RosSharp.RosBridgeClient
{
    public class PointCloudProjector : MonoBehaviour
    {
        private bool _isMessageReceived;
        private byte _height;
        private byte _width;

        private MessageTypes.Sensor.PointField[] _fields;

        private byte[] _data;


        private void Update()
        {
            if (_isMessageReceived)
                ProcessMessage();
        }

        public void Write(MessageTypes.Sensor.PointCloud2 message)
        {
            _height = (byte)message.height;
            _width = (byte)message.width;
            _fields = message.fields;
            _data = message.data;

            _isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            _isMessageReceived = false;

        }
    }
}