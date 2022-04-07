using UnityEngine;
using UnityEngine.Events;

using Microsoft.MixedReality.Toolkit.UI;

public class AR_ToggleListCheck : MonoBehaviour
{
    //public UnityEvent test;
    public GameObject ToggleList;

    private Interactable[] _rosTopicList;

    public void CheckToggleList()
    {
        _rosTopicList = ToggleList.GetComponentsInChildren<Interactable>();

        foreach (Interactable toggleObject in _rosTopicList)
        {
            if(toggleObject.IsToggled)
            {
                Debug.Log(toggleObject.name + " toggled");
            }
        }
    }
}
