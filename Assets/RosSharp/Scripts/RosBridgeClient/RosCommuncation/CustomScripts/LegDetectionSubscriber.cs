using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class LegDetectionSubscriber : UnitySubscriber<MessageTypes.People.PositionMeasurementArray>
    {
        public LegProjector legProjector;

        protected override void Start()
        {
            base.Start();
        }

        protected override void ReceiveMessage(MessageTypes.People.PositionMeasurementArray message)
        {
            legProjector.Write(message);
        }
    }
}

