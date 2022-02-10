using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class vuforia_TargetCubeManager : MonoBehaviour
{
    public GameObject TargetCube;
    private MultiTargetBehaviour[] _targetCubes;

    public void SelectCube(Button button)
    {
        var name = button.name;
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
