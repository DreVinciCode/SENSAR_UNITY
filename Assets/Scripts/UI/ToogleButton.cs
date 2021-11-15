using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToogleButton : MonoBehaviour
{
    public Color SelectedColor;
    public Color DeselectedColor;
    public Toggle ToggleButtonSelected;

    private ColorBlock _colorBlock;

    public void ToggleButtonColor()
    {
        if (ToggleButtonSelected.isOn)
        {           
            _colorBlock = ToggleButtonSelected.colors;
            _colorBlock.normalColor = SelectedColor;
            _colorBlock.highlightedColor = SelectedColor;
            _colorBlock.selectedColor = SelectedColor;         
            ToggleButtonSelected.colors = _colorBlock;
        }
        else
        {
            _colorBlock = ToggleButtonSelected.colors;
            _colorBlock.normalColor = DeselectedColor;
            _colorBlock.highlightedColor = DeselectedColor;
            _colorBlock.selectedColor = DeselectedColor;
            ToggleButtonSelected.colors = _colorBlock;
        }
    }
}
