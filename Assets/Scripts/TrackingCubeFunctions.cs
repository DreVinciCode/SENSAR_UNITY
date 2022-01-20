using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingCubeFunctions : MonoBehaviour
{
    public GameObject TargetCube;
    private Transform _initialPose;

    public void ResetTrackedCube()
    {
        //Cant figure out how to store the initial position of the cube and set it as new pose when calling this function.
        TargetCube.GetComponent<Transform>().position = new Vector3(0f, 0.57f, 0f);
        TargetCube.GetComponent<Transform>().rotation = new Quaternion(-0.7071068f, 0, 0, 0.7071068f);
    }




}
