using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RosSharp.RosBridgeClient
{
    public class LaptopProjector : MonoBehaviour
    {
        public Gradient ColorRamp;
        public Image LaptopBatteryLevel;
        public TMP_Text BatteryLevel;

        private float _maxLevel = 100f;
        private float _currentLevel;
        private float _lerpSpeed = 3f;
        private bool _isMessageReceived;
        private float _percentage;

        private void Update()
        {
            if (_isMessageReceived)
                ProcessMessage();
        }

        public void Write(MessageTypes.Sensor.BatteryState message)
        {
            _percentage = message.percentage;
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