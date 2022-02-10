/* 
 * This message is auto generated by ROS#. Please DO NOT modify.
 * Note:
 * - Comments from the original code will be written in their own line 
 * - Variable sized arrays will be initialized to array of size 0 
 * Please report any issues at 
 * <https://github.com/siemens/ros-sharp> 
 */



namespace RosSharp.RosBridgeClient.MessageTypes.Kobuki
{
    public class MotorPower : Message
    {
        public const string RosMessageName = "kobuki_msgs-noetic/MotorPower";

        //  Turn on/off Kobuki's motors
        //  State
        public const byte OFF = 0;
        public const byte ON = 1;
        public byte state { get; set; }

        public MotorPower()
        {
            this.state = 0;
        }

        public MotorPower(byte state)
        {
            this.state = state;
        }
    }
}
