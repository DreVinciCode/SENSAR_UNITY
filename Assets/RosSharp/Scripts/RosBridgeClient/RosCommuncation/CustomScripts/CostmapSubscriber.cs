using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class CostmapSubscriber : UnitySubscriber<MessageTypes.Nav.OccupancyGrid>
    {
        public CostmapProjector costmapProjector;

        protected override void Start()
        {
            base.Start();
        }
        protected override void ReceiveMessage(MessageTypes.Nav.OccupancyGrid message)
        {
            costmapProjector.Write(message);
        }
    }
}
