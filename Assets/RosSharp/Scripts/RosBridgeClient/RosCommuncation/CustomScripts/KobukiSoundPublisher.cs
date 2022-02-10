using UnityEngine;

/* The available sound sequences:
uint8 ON            = 0
uint8 OFF           = 1
uint8 RECHARGE      = 2
uint8 BUTTON        = 3
uint8 ERROR         = 4
uint8 CLEANINGSTART = 5
uint8 CLEANINGEND   = 6
*/

namespace RosSharp.RosBridgeClient
{
    public class KobukiSoundPublisher : UnityPublisher<MessageTypes.Kobuki.Sound>
    {
        private MessageTypes.Kobuki.Sound message;

        protected override void Start()
        {
            base.Start();
        }

        public void PlaySound(int value)
        {
            message = new MessageTypes.Kobuki.Sound();
            message.value = (byte)value;
            Publish(message);
        }
    }
}

