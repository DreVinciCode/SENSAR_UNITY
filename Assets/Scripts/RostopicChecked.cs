using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RostopicChecked : MonoBehaviour
{
    public Toggle[] RostopicList;
    public GameObject VisualizeButton;

    private void Start()
    {
        VisualizeButton.SetActive(false);
    }

    public void CheckToggleList()
    {
        foreach (Toggle topic in RostopicList)
        {
            if(topic.isOn)
            {
                VisualizeButton.SetActive(true);
                break;
            }
            else
            {
                VisualizeButton.SetActive(false);
            }
        }
    }
}
