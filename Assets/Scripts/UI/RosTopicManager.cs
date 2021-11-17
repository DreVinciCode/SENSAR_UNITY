using System.Collections;
using System.Collections.Generic;
using RosSharp.RosBridgeClient;
using UnityEngine;
using UnityEngine.UI;

public class RosTopicManager : MonoBehaviour
{
    public GameObject RostopicList;
    private Toggle[] _toggleButtonList;

    private void Start()
    {
        _toggleButtonList = RostopicList.GetComponentsInChildren<Toggle>();

        Debug.Log(_toggleButtonList.Length);
    }

    public void ToggleROSTopicSubscription()
    {
        if(true)
        {
            Debug.Log("On!");
        }
        else
        {
            Debug.Log("Off!");
        }
    }
}
