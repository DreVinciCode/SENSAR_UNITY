using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class MapSubscriber : UnitySubscriber<MessageTypes.Nav.OccupancyGrid>
    {    
        public MapProjector mapProjector;

        protected override void Start()
		{
			base.Start();
		}
		
        protected override void ReceiveMessage(MessageTypes.Nav.OccupancyGrid message)
        {
            mapProjector.Write(message);
        }
    }
}