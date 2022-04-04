using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public Vector3 RotateVector;

    private void Update()
    {
        transform.Rotate(RotateVector * Time.deltaTime);
    }
}