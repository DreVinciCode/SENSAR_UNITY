using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PushButtons : MonoBehaviour
{
    public static event Action<string> ButtonPressed = delegate {};

    private int _dividerPosition;
    private string _buttonName, _buttonValue;

    private void Start()
    {
        _buttonName = gameObject.name;
        _dividerPosition = _buttonName.IndexOf("_");
        _buttonValue = _buttonName.Substring(0, _dividerPosition);

        gameObject.GetComponent<Button>().onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
    {
        ButtonPressed(_buttonValue);
    }


}
