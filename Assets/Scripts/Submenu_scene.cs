using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submenu_scene : MonoBehaviour
{
    public GameObject IPAddressPanel;
    public GameObject SensorPanel;

    private bool _toggleIPAddressPanel = false;
    private bool _toggleSensorsPanel = false;

    private void Start()
    {
        IPAddressPanel.SetActive(_toggleIPAddressPanel);
        SensorPanel.SetActive(_toggleSensorsPanel);
    }

    public void ToggleIPAddressPanel()
    {
        _toggleIPAddressPanel = !_toggleIPAddressPanel;
        IPAddressPanel.SetActive(_toggleIPAddressPanel);
    }

    public void ToggleSensorsPanel()
    {
        _toggleSensorsPanel = !_toggleSensorsPanel;
        SensorPanel.SetActive(_toggleSensorsPanel);
    }
}
