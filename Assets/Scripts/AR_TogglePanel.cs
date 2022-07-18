using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_TogglePanel : MonoBehaviour
{
    private bool _check = false;

    public void TogglePanel()
    {
        _check = !_check;
        gameObject.SetActive(_check);
    }
}
