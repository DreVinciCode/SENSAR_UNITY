using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_scene : MonoBehaviour
{
    public GameObject IPAddressPanel;

    private bool _toggleIPAddressPanel = false;

    void Start()
    {
        IPAddressPanel.SetActive(_toggleIPAddressPanel);
    }

    public void ToggleIPAddressPanel()
    {
        _toggleIPAddressPanel = !_toggleIPAddressPanel;
        IPAddressPanel.SetActive(_toggleIPAddressPanel);
    }
}
