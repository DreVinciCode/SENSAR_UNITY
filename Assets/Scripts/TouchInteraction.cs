using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TouchInteraction : MonoBehaviour, IMixedRealityTouchHandler
{
    public TouchEvent OnTounchComplete;

    public UnityEvent OnConnectComplete;
    public Image ConnectRing;
    public Gradient ColorRamp;
    public TMP_Text RequiredUserStatus;

    public SpriteRenderer spriteRenderer;

    private float _duration = 2f;
    private float _delay = -0.5f;
    private float _currentTime = 0f;
    private bool _locked;

    public ROSProfileConnection rosConnection;


    public GameManager.State setSatus;
    private GameManager.State superUserStatus = GameManager.State.SuperUser;

    private void Start()
    {
        _locked = true;
        SetSpriteColorAndText();
    }

    void IMixedRealityTouchHandler.OnTouchStarted(HandTrackingInputEventData eventData)
    {     
        if (_locked && (GameManager.Userstatus == setSatus || GameManager.Userstatus == superUserStatus))
        {
            _currentTime = _delay;
            ConnectRing.fillAmount = 0;
        }
    }
    void IMixedRealityTouchHandler.OnTouchCompleted(HandTrackingInputEventData eventData)
    {
        if(_currentTime < 0f)
        {
            OnTounchComplete.Invoke(eventData);
        }

        if (_locked && (GameManager.Userstatus == setSatus || GameManager.Userstatus == superUserStatus))
        {
            ConnectRing.fillAmount = 0;
            _currentTime = _delay;
        }
    }

    void IMixedRealityTouchHandler.OnTouchUpdated(HandTrackingInputEventData eventData)
    {
        if (_locked && (GameManager.Userstatus == setSatus || GameManager.Userstatus == superUserStatus))
        {
            StartCoroutine(Timer(_duration));
            _currentTime += Time.deltaTime;
        }
    } 

    public IEnumerator waitToConnect()
    {
        yield return new WaitForSeconds(5);

        if(rosConnection.ConnectionCheck)
        {
            ConnectRing.fillAmount = 0;
            _locked = true;
        }
    }

    public IEnumerator Timer(float duration)
    {
        var startTime = Time.time;
        var value = 0f;

        if (_currentTime > 0)
        {
           
            while (Time.time - startTime < duration && _locked)
            {




                value = _currentTime / duration;
                var color = Color.Lerp(ColorRamp.Evaluate(0f), ColorRamp.Evaluate(1f), value);
                ConnectRing.color = color;
                ConnectRing.fillAmount = value;

                if (ConnectRing.fillAmount >= 1)
                {
                    _locked = false;
                    OnConnectComplete.Invoke();
                    StartCoroutine(waitToConnect());
                }

                yield return null;
            }
        }


    }

    public void SetSpriteColorAndText()
    {     
        if (setSatus == GameManager.State.Locked)
        {
            spriteRenderer.color = Color.red;
            RequiredUserStatus.text = setSatus.ToString();
        }
        else if (setSatus == GameManager.State.OrdinaryUser)
        {
            spriteRenderer.color = Color.green;
            RequiredUserStatus.text = setSatus.ToString();
        }
        else if (setSatus == GameManager.State.SuperUser)
        {
            spriteRenderer.color = Color.blue;
            RequiredUserStatus.text = setSatus.ToString();
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

