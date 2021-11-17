using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Protocols;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ROS_Profile_Connection : MonoBehaviour
{
    public TMP_Text IPText;
    private RosConnector _rosConnector;
    private string _deviceIP;

    private void Start()
    {
        _rosConnector = GetComponent<RosConnector>();
        _rosConnector.Serializer = RosSocket.SerializerEnum.Newtonsoft_JSON;
        _rosConnector.protocol = RosSharp.RosBridgeClient.Protocols.Protocol.WebSocketSharp;
    }

    public void SetIPAddress()
    {
        _deviceIP = IPText.text;
        _rosConnector.RosBridgeServerUrl = "ws://" + _deviceIP + ":9090";
        _rosConnector.RosConnect();
    }

}
