using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class LocalizationProjector : MonoBehaviour
    {
        public Vector3 Offset;
        public float ParticleSize = 0.02f;
        public Material ParticleMaterial;

        [SerializeField]
        private ParticleSystem _localizationParticles;

        [SerializeField]
        private Mesh _particleShape;

        private ParticleSystem.Particle[] _particles;
        private int _totalParticles;
        private MessageTypes.Geometry.Pose[] _poses;
        private bool isMessageReceived;


        private void Start()
        {
            _localizationParticles.GetComponent<ParticleSystemRenderer>().mesh = _particleShape;
            _localizationParticles.GetComponent<ParticleSystemRenderer>().enabled = true;
            _localizationParticles.GetComponent<ParticleSystemRenderer>().material = ParticleMaterial;
        }

        private void Update()
        {
            if (isMessageReceived)
                ProcessMessage();
        }

        private void ProcessMessage()
        {
            _particles = new ParticleSystem.Particle[_totalParticles];

            for (int i = 0; i < _totalParticles; i++)
            {
                var particle = _poses[i];
                _particles[i].position = GetPosition(particle).Ros2Unity() + Offset;
                _particles[i].rotation = GetRotation(particle).Ros2Unity().y;
                _particles[i].startSize = ParticleSize;
            }

            _localizationParticles.SetParticles(_particles, _totalParticles);
            isMessageReceived = false;
        }
        public void Write(MessageTypes.Geometry.PoseArray message)
        {
            _totalParticles = message.poses.Length;
            _poses = message.poses;
            isMessageReceived = true;
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
