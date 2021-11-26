using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonBehavior : MonoBehaviour
{
    public Button Button;
    public Color InitialColor;
    public Color ConnectedColor;
    public Color DisconnectedColor;

    private ColorBlock _colorBlock;

    private void Start()
    {
        _colorBlock = Button.colors;
        _colorBlock.normalColor = InitialColor;
        Button.colors = _colorBlock;
    }

    public void ROSConnectionEstablished()
    {
        _colorBlock = Button.colors;
        _colorBlock.normalColor = ConnectedColor;
        _colorBlock.highlightedColor = ConnectedColor;
        _colorBlock.selectedColor = ConnectedColor;
        Button.colors = _colorBlock;
        Debug.Log("Established");
    }

    public void ROSConnectionLost()
    {
        _colorBlock = Button.colors;
        _colorBlock.normalColor = DisconnectedColor;
        _colorBlock.highlightedColor = DisconnectedColor;
        _colorBlock.selectedColor = DisconnectedColor;
        Button.colors = _colorBlock;
        Debug.Log("Disconect");

    }
}
