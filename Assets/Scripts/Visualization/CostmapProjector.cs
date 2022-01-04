using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class CostmapProjector : MonoBehaviour
    {
        public Transform CostmapOrigin;
        public Vector3 Offset;
        public float ParticleSize = 0.02f;
        public Gradient ColorRamp;
        public Material ParticleMaterial;


        [SerializeField]
        private ParticleSystem _localizationParticles;

        [SerializeField]
        private Mesh _particleShape;

        private ParticleSystem.Particle[] _particles;
        private int _totalParticles;
        private MessageTypes.Geometry.Pose[] _poses;
        private float _resolution;
        private float _width;
        private float _height;
        private sbyte[] _data;
        private bool _isMessageReceived;
        private Vector3 position;
        private Quaternion rotation;

        private void Start()
        {
            _localizationParticles.GetComponent<ParticleSystemRenderer>().mesh = _particleShape;
            _localizationParticles.GetComponent<ParticleSystemRenderer>().material = ParticleMaterial;
        }

        private void Update()
        {
            if (_isMessageReceived)
                ProcessMessage();
        }

        public void Write(MessageTypes.Nav.OccupancyGrid message)
        {
            _totalParticles = message.data.Length;
            _resolution = message.info.resolution;
            _width = message.info.width;
            _height = message.info.height;
            _data = message.data;
            position = GetPosition(message).Ros2Unity();
            rotation = GetRotation(message).Ros2Unity();
            _isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            CostmapOrigin.position = position;
            CostmapOrigin.rotation = rotation;

            Vector3 xAxis = CostmapOrigin.transform.forward.normalized;
            Vector3 zAxiz = -1 * CostmapOrigin.transform.right.normalized;
            Vector3 x_inc = _resolution * xAxis;
            Vector3 z_inc = _resolution * zAxiz;
            Vector3 origin = CostmapOrigin.transform.position;
            Vector3 current = origin;

            var widthCounter = 0;

            _particles = new ParticleSystem.Particle[_totalParticles];

            for (int i = 0; i < _totalParticles; i++)
            {
                if (widthCounter == _width)
                {
                    current += z_inc;
                    current -= x_inc * widthCounter;
                    widthCounter = 0;
                }

                if (_data[i] != -1)
                {
                    _particles[i].position = current;
                    _particles[i].startSize = ParticleSize;
                    _particles[i].startColor = Color.Lerp(ColorRamp.Evaluate(0f), ColorRamp.Evaluate(1f), _data[i] / 100.0f);                
                   
                }

                current += x_inc;
                widthCounter++;
            }

            _localizationParticles.SetParticles(_particles, _totalParticles);
            _localizationParticles.GetComponent<ParticleSystemRenderer>().enabled = true;

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

