using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class PeopleDetectionSubscriber : UnitySubscriber<MessageTypes.People.PositionMeasurementArray>
    {
        public PeopleProjector peopleProjector;

        protected override void Start()
        {
            base.Start();
        }

        protected override void ReceiveMessage(MessageTypes.People.PositionMeasurementArray message)
        {
            peopleProjector.Write(message);
        }
    }
}

