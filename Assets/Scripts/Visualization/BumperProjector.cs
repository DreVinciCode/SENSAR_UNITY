using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class BumperProjector : MonoBehaviour
    {
        private bool isMessageReceived;

        private void Update()
        {
            if (isMessageReceived)
                ProcessMessage();
        }

        public void Write(MessageTypes.Sensor.PointCloud2 message)
        {
            var data = message.data;

            isMessageReceived = true;
        }

        private void ProcessMessage()
        {


            isMessageReceived = false;
        }
    }
}

