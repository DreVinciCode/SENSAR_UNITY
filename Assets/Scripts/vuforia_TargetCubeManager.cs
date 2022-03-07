using System;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using TMPro;

public class vuforia_TargetCubeManager : MonoBehaviour
{
    public GameObject TargetCube;
    public TMP_Text TargetCubeName;
    private MultiTargetBehaviour[] _targetCubes;

    public void SelectCube(Button button)
    {
        var name = button.name;
        //Find better way to do this; ideally from a rostopic
        var robotname = name.Replace("_Cube", "");
        TargetCubeName.text = "Robot: " + robotname; 
        _targetCubes = TargetCube.GetComponents<MultiTargetBehaviour>();

        for (int i = 0; i < _targetCubes.Length; i++)
        {
            if(_targetCubes[i].TargetName == name)
            {
                _targetCubes[i].enabled = true;
            }
            else
            {
                _targetCubes[i].enabled = false;
            }
        }
    }
}
