using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class PathProjector : MonoBehaviour
    {
        public float ParticleSize;
        public Vector3 Offset;
        public Material ParticleMaterial;

        [SerializeField]
        private ParticleSystem _pathParticles;
        private ParticleSystem.Particle[] _particles;
        private bool _isMessageReceived;
        private MessageTypes.Geometry.PoseStamped[] _poses;
        private int _pathSize;

        private void Start()
        {
            _pathParticles.GetComponent<ParticleSystemRenderer>().enabled = true;
            _pathParticles.GetComponent<ParticleSystemRenderer>().material = ParticleMaterial;
        }

        private void Update()
        {
            if (_isMessageReceived)
                ProcessMessage();
        }

        public void Write(MessageTypes.Nav.Path message)
        {
            _pathSize = message.poses.Length;
            _poses = message.poses;
            _isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            _particles = new ParticleSystem.Particle[_pathSize];

            for (int i = 0; i < _pathSize; i++)
            {
                var particle = _poses[i].pose;
                _particles[i].position = GetPosition(particle).Ros2Unity() + Offset;
                _particles[i].rotation = GetRotation(particle).Ros2Unity().y;
                _particles[i].startSize = ParticleSize;
            }

            _pathParticles.SetParticles(_particles, _pathSize);
            _pathParticles.GetComponent<ParticleSystemRenderer>().enabled = true;
            _isMessageReceived = false;
        }

        private Vector3 GetPosition(MessageTypes.Geometry.Pose message)
        {
            return new Vector3(
                (float)message.position.x,
                (float)message.position.y,
                (float)message.position.z);
        }

        private Quaternion GetRotation(MessageTypes.Geometry.Pose message)
        {
            return new Quaternion(
                (float)message.orientation.x,
                (float)message.orientation.y,
                (float)message.orientation.z,
                (float)message.orientation.w);
        }
    }
}

