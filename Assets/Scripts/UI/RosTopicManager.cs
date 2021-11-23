using System.Collections;
using System.Collections.Generic;
using RosSharp.RosBridgeClient;
using UnityEngine;
using UnityEngine.UI;

public class RosTopicManager : MonoBehaviour
{
    //public GameObject RosConnector;
    public GameObject RostopicButtonList;
    private Toggle[] _toggleButtonList;

    private void Start()
    {
        _toggleButtonList = RostopicButtonList.GetComponentsInChildren<Toggle>();
        Debug.Log(GetComponents(typeof(LaserScanSubscriber)).Length + "Test");

    }

    public void CheckToggleList()
    {
        foreach ( Toggle data in _toggleButtonList)
        {
            if(data.isOn)
            {
                //Add to active subscriber. Active buttons correspond to rostopic list


                Debug.Log(data.name);
            }
        }
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
