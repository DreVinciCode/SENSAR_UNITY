using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TouchInteraction : MonoBehaviour, IMixedRealityTouchHandler
{
    //public TouchEvent OnTouchCompleted;
    //public TouchEvent OnTouchStarted;
    //public TouchEvent OnTouchUpdated;
    public UnityEvent OnConnectComplete;
    public Image ConnectRing;
    public Gradient ColorRamp;

    private float _duration = 2f;
    private float _currentTime = 0f;
    private bool _locked;

    public bool SuperUser { get; set; }

    private void Start()
    {
        _locked = true;
        SuperUser = false;
    }

    void IMixedRealityTouchHandler.OnTouchStarted(HandTrackingInputEventData eventData)
    {
        if (!_locked)
        {
            _currentTime = 0f;
            ConnectRing.fillAmount = 0;
            _locked = !_locked;

        }
    }
    void IMixedRealityTouchHandler.OnTouchCompleted(HandTrackingInputEventData eventData)
    {
        if (_locked)
        {
            ConnectRing.fillAmount = 0; 
            _currentTime = 0f;
        }
    }

    void IMixedRealityTouchHandler.OnTouchUpdated(HandTrackingInputEventData eventData)
    {
        //OnTouchUpdated.Invoke(eventData);
        if (_locked)
        {
            StartCoroutine(Timer(_duration));
            _currentTime += Time.deltaTime;
        }
    } 

    public IEnumerator Timer(float duration)
    {
        var startTime = Time.time;
        var value = 0f;

        while (Time.time - startTime < duration && _locked)
        {
            value = _currentTime / duration;
            var color = Color.Lerp(ColorRamp.Evaluate(0f), ColorRamp.Evaluate(1f), value);
            ConnectRing.color = color;
            ConnectRing.fillAmount = value;

            if(ConnectRing.fillAmount >= 1)
            {
                _locked = false;
                OnConnectComplete.Invoke();
            }

            yield return null;
        }
    }

    public void ConnectedColor()
    {
        ConnectRing.color = Color.cyan;
    }

    public void DisconnectColor()
    {
        ConnectRing.color = Color.red;
    }
}

