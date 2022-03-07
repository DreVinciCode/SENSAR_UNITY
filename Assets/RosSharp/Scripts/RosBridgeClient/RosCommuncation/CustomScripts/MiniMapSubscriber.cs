using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class MiniMapSubscriber : UnitySubscriber<MessageTypes.Nav.OccupancyGrid>
    {    
        public MiniMapProjector miniMapProjector;

        protected override void Start()
		{
			base.Start();
		}
		
        protected override void ReceiveMessage(MessageTypes.Nav.OccupancyGrid message)
        {
            miniMapProjector.Write(message);
        }
    }
}