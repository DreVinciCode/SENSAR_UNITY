using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class VisualizationMarker_PeopleSubscriber : UnitySubscriber<MessageTypes.Visualization.MarkerArray>
    {
        public VisualizationMarker_PeopleProjector visualizationMarker_LegsProjector;

        protected override void Start()
        {
            base.Start();
        }

        protected override void ReceiveMessage(MessageTypes.Visualization.MarkerArray message)
        {
            visualizationMarker_LegsProjector.Write(message);
        }
    }
}
