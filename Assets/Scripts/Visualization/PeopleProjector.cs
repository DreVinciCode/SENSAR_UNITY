using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class PeopleProjector : MonoBehaviour
    {
        public Vector3 Offset;

        [SerializeField]
        private ParticleSystem _localizationParticles;

        private ParticleSystem.Particle[] _particles;
        private bool _isMessageReceived;

        private MessageTypes.People.PositionMeasurementArray[] _people;
        private int _totalCount;
        private float[] _coocurrence;


        private void Update()
        {
            if (_isMessageReceived)
                ProcessMessage();
        }

        public void Write(MessageTypes.People.PositionMeasurementArray message)
        {
            _totalCount = message.people.Length;
            _coocurrence = message.cooccurrence;
            _isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            Debug.Log(_totalCount + " People detected");
            _isMessageReceived = false;
        }

        private Vector3 GetPosition(MessageTypes.Nav.OccupancyGrid message)
        {
            return new Vector3(
                (float)message.info.origin.position.x,
                (float)message.info.origin.position.y,
                (float)message.info.origin.position.z);
        }

        private Quaternion GetRotation(MessageTypes.Nav.OccupancyGrid message)
        {
            return new Quaternion(
                (float)message.info.origin.orientation.x,
                (float)message.info.origin.orientation.y,
                (float)message.info.origin.orientation.z,
                (float)message.info.origin.orientation.w);
        }
    }
}