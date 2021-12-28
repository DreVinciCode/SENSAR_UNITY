using UnityEngine;
using UnityEngine.UI;

public class RostopicChecked : MonoBehaviour
{
    public GameObject VisualizeButton;
    public GameObject[] RostopicGroups;
    private Toggle[] RostopicList;

    private void Start()
    {
        VisualizeButton.SetActive(false);
    }

    public void CheckToggleList()
    {
        foreach (var topicGroup in RostopicGroups)
        {
            RostopicList = topicGroup.GetComponentsInChildren<Toggle>();

            foreach (Toggle topic in RostopicList)
            {
                if(topic.isOn)
                {
                    VisualizeButton.SetActive(true);
                    return;
                }
                else
                {
                    VisualizeButton.SetActive(false);
                }
            }
        }     
    }
}
