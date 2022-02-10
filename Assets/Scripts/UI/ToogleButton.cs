using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToogleButton : MonoBehaviour
{
    public Color SelectedColor;
    public Color DeselectedColor;
    public Color TextColorSelected;
    public Color TextColorDeselected;
    public Toggle ToggleButtonSelected;
    public Image ToggleImageSelected;
    public TMP_Text ToggleTextSelected;

    private ColorBlock _colorBlock;

    public void ToggleButtonColor()
    {
        if (ToggleButtonSelected != null)
        {
            if (ToggleButtonSelected.isOn)
            {
                _colorBlock = ToggleButtonSelected.colors;
                _colorBlock.normalColor = SelectedColor;
                _colorBlock.highlightedColor = SelectedColor;
                _colorBlock.selectedColor = SelectedColor;
                ToggleButtonSelected.colors = _colorBlock;
                ToggleImageSelected.color = SelectedColor;
                ToggleTextSelected.color = TextColorSelected;
            }
            else
            {
                _colorBlock = ToggleButtonSelected.colors;
                _colorBlock.normalColor = DeselectedColor;
                _colorBlock.highlightedColor = DeselectedColor;
                _colorBlock.selectedColor = DeselectedColor;
                ToggleButtonSelected.colors = _colorBlock;
                ToggleImageSelected.color = DeselectedColor;
                ToggleTextSelected.color = TextColorDeselected;
            }
        }
    }
}
