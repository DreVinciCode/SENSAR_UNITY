using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class PathSubscriber : UnitySubscriber<MessageTypes.Nav.Path>
    {
        public PathProjector pathProjector;

        protected override void Start()
        {
            base.Start();
        }

        protected override void ReceiveMessage(MessageTypes.Nav.Path message)
        {
            pathProjector.Write(message);
        }
    }
}

