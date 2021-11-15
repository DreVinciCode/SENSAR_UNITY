using System;
using System.Linq;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DigitalDisplay : MonoBehaviour
{
    public TMP_Text IPDisplay;
    public GameObject ContinueButton;

    private string _IPAddress;

    private void Start()
    {
        _IPAddress = "192.168.0.1";
        IPDisplay.text = _IPAddress;

        PushButtons.ButtonPressed += AddDigitToIPSequence;
    }

    public bool ValidateIPv4(string ipString)
    {
        if (String.IsNullOrEmpty(ipString))
        {
            return false;
        }

        string[] splitValues = ipString.Split('.');
        if (splitValues.Length != 4)
        {
            return false;
        }

        byte tempForParsing;

        return splitValues.All(r => byte.TryParse(r, out tempForParsing));
    }

    private void AddDigitToIPSequence(string digitsEntered)
    {
        if(_IPAddress.Length > 15)
        {
            _IPAddress = _IPAddress.Substring(0, _IPAddress.Length - 1);
        }

        switch (digitsEntered)
        {
            case "0":
                _IPAddress += "0";
                IPDisplay.text = _IPAddress;
                break;

            case "1":
                _IPAddress += "1";
                IPDisplay.text = _IPAddress;
                break;
            case "2":
                _IPAddress += "2";
                IPDisplay.text = _IPAddress;
                break;
            case "3":
                _IPAddress += "3";
                IPDisplay.text = _IPAddress;
                break;
            case "4":
                _IPAddress += "4";
                IPDisplay.text = _IPAddress;
                break;
            case "5":
                _IPAddress += "5";
                IPDisplay.text = _IPAddress;
                break;
            case "6":
                _IPAddress += "6";
                IPDisplay.text = _IPAddress;
                break;
            case "7":
                _IPAddress += "7";
                IPDisplay.text = _IPAddress;
                break;
            case "8":
                _IPAddress += "8";
                IPDisplay.text = _IPAddress;
                break;
            case "9":
                _IPAddress += "9";
                IPDisplay.text = _IPAddress;
                break;
            case ".":
                _IPAddress += ".";
                IPDisplay.text = _IPAddress;
                break;
            case "x":
                _IPAddress = _IPAddress.Substring(0, _IPAddress.Length - 1);
                IPDisplay.text = _IPAddress;
                break;
            default:
                break;
        }

        if(ValidateIPv4(_IPAddress))
        {
            ContinueButton.SetActive(true);
        }
        else
        {
            ContinueButton.SetActive(false);
        }
    }
}
