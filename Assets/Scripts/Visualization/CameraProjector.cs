using UnityEngine;
using UnityEngine.UI;

namespace RosSharp.RosBridgeClient
{
    public class CameraProjector : MonoBehaviour
    {
        public RawImage RobotCameraImage;
        public Canvas CameraDisplayCanvas;
        private bool isMessageReceived;
        private byte[] _data;

        private void Update()
        {
            if (isMessageReceived)
                ProcessMessage();
        }

        public void WriteCameraImage(MessageTypes.Sensor.CompressedImage message)
        {
            _data = message.data;
            isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(_data);
            texture.Apply();
            RobotCameraImage.texture = texture;
            CameraDisplayCanvas.enabled = true;
            isMessageReceived = false;
        }
    }
}
