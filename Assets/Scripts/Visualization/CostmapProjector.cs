using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class CostmapProjector : MonoBehaviour
    {
        public Transform CostmapOrigin;
        public Vector3 Offset;
        public Gradient ColorRamp;
        public Material ParticleMaterial;

        private int _totalParticles;
        private MessageTypes.Geometry.Pose[] _poses;
        private float _resolution;
        private float _width;
        private float _height;
        private sbyte[] _receivedData;
        private sbyte[] _currentData;
        private bool _isMessageReceived;
        private Vector3 position;
        private Quaternion rotation;

        private void Start()
        {
            _currentData = new sbyte[0];
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
            _receivedData = message.data;
            position = GetPosition(message).Ros2Unity();
            rotation = GetRotation(message).Ros2Unity();
            _isMessageReceived = true;
        }

        private void ProcessMessage()
        {
            if (!_receivedData.SequenceEqual<sbyte>(_currentData))
            {
                _currentData = _receivedData;

                CostmapOrigin.position = position;
                CostmapOrigin.rotation = rotation;

                Vector3 xAxis = CostmapOrigin.transform.forward.normalized;
                Vector3 zAxiz = -1 * CostmapOrigin.transform.right.normalized;
                Vector3 x_inc = _resolution * xAxis;
                Vector3 z_inc = _resolution * zAxiz;
                Vector3 origin = CostmapOrigin.transform.position;
                Vector3 current = origin;

                var widthCounter = 0;

                for (int i = 0; i < _totalParticles; i++)
                {
                    if (widthCounter == _width)
                    {
                        current += z_inc;
                        current -= x_inc * widthCounter;
                        widthCounter = 0;
                    }

                    if (_receivedData[i] != -1)
                    {
                        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
                        quad.transform.parent = CostmapOrigin.transform;
                        quad.transform.name = i.ToString();
                        quad.transform.localScale = Vector3.one * _resolution;
                        quad.transform.position = current;
                        quad.transform.eulerAngles = new Vector3(90, transform.eulerAngles.y, transform.eulerAngles.z);

                        var material = new Material(ParticleMaterial);
                        material.EnableKeyword("_EMISSION");
                        var color = Color.Lerp(ColorRamp.Evaluate(0f), ColorRamp.Evaluate(1f), _receivedData[i] / 100.0f);
                        color.a = _receivedData[i] / 100.0f;
                        material.color = color;
                        material.SetColor("_EmissionColor", material.color);
                        quad.transform.GetComponent<MeshRenderer>().material = material;
                        quad.transform.GetComponent<MeshRenderer>().enabled = true;
                    }

                    current += x_inc;
                    widthCounter++;
                }
            }

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

