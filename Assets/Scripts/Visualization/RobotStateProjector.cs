using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RosSharp.RosBridgeClient
{
    public class RobotStateProjector : MonoBehaviour
    {
        public Gradient ColorRamp;
        public Image LaptopBatteryLevel;
        public TMP_Text BatteryLevel;

        private float _maxLevel = 100f;
        private float _currentLevel;
        private float _lerpSpeed = 3f;
        private bool _isMessageReceived;
        private float _percentage;
        private string _name;
        private int _messageCount;
        private string _robotBatteryName = "/Power System/Battery";
        private string _valueName = "Percent";
        private MessageTypes.Diagnostic.DiagnosticStatus[] _statuses;
        private MessageTypes.Diagnostic.KeyValue[] _values;

        private void Update()
        {
            if (_isMessageReceived)
                ProcessMessage();
        }

        public void Write(MessageTypes.Diagnostic.DiagnosticArray message)
        {
            _statuses = message.status;
            _messageCount = _statuses.Length;

            for (int i = 0; i < _messageCount; i++)
            {
                _name = _statuses[i].name;

                if(_name == _robotBatteryName)
                {
                    _values = _statuses[i].values;

                    foreach (var value in _values)
                    {
                       if(value.key == _valueName)
                       {
                            _percentage = (float)Math.Round(float.Parse(value.value), 2);
                       }
                    }
                }
            }

            _isMessageReceived = true;
        }

        private void ProcessMessage()
        {          
            BatteryLevel.text = _percentage.ToString() + "%";
            _currentLevel = _percentage / _maxLevel;
            LaptopBatteryLevel.fillAmount = Mathf.Lerp(LaptopBatteryLevel.fillAmount, _currentLevel, _lerpSpeed * Time.deltaTime);
            var color = Color.Lerp(ColorRamp.Evaluate(0f), ColorRamp.Evaluate(1f), _currentLevel);
            LaptopBatteryLevel.color = color;        
            _isMessageReceived = false;
        }
    }
}