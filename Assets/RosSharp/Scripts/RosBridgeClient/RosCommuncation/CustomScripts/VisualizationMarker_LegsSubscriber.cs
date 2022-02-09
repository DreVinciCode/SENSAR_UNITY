using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class VisualizationMarker_LegsSubscriber : UnitySubscriber<MessageTypes.Visualization.MarkerArray>
    {
        public VisualizationMarker_LegsProjector visualizationMarker_LegsProjector;

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
