using UnityEngine;
using UnityEngine.UI;

namespace RosSharp.RosBridgeClient
{
    public class CameraProjector : MonoBehaviour
    {
        public RawImage RobotCameraImage;

        private bool isMessageReceived;

        private uint _height;
        private uint _width;

        private uint _rawHeight;
        private uint _rawWidth;

        private byte[] _data;

        private void Update()
        {
            if (isMessageReceived)
                ProcessMessage();
        }

        public void WriteCameraInfo(MessageTypes.Sensor.CameraInfo message)
        {
            _height = message.height;
            _width = message.width;

           // Debug.Log("height: " + _height + " width: " + _width);
        }

        public void WriteCameraImage(MessageTypes.Sensor.Image message)
        {
            _rawHeight = message.height;
            _rawWidth = message.width;
            _data = message.data;

            isMessageReceived = true;
        }

        private void ProcessMessage()
        {

            Texture2D tex = new Texture2D((int)_rawWidth, (int)_rawHeight);
            RobotCameraImage.GetComponent<RawImage>().texture = tex;

            var startPoint = 0;

            for (var y = 0; y < _rawWidth; y++)
            {
                for (var x = 0; x < _rawHeight; x++)
                {
                    var pixel = new Color(_data[startPoint], _data[startPoint + 1], _data[startPoint + 2]);
                    startPoint += 3;
                    tex.SetPixel(y, x, pixel);
                }
            }

            tex.Apply();


            isMessageReceived = false;
        }
    }
}
