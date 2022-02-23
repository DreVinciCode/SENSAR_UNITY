using UnityEngine;
using Vuforia;

public class CameraDistance : MonoBehaviour
{
    public ObserverBehaviour observerBehaviour;
    public ImageTargetBehaviour imageTargetBehaviour;
    public Transform parent;
    public GameObject markerObject;
    private Vector3 _distance;

    private float _safetydistance = 0.1f;


    private void Update()
    {
        if (observerBehaviour.TargetStatus.Status == Status.TRACKED)
        {
            _distance = (Camera.main.transform.position - imageTargetBehaviour.transform.position);
            Vector3 projectOnPlane = Vector3.ProjectOnPlane(_distance, Vector3.up);
            var magnitude = projectOnPlane.magnitude;
            var _distanceMinusSD = magnitude - _safetydistance;


            var direction = projectOnPlane.normalized;

            var newVector3 = imageTargetBehaviour.transform.position + direction * _distanceMinusSD;
            markerObject.transform.position = newVector3;

            //after projecting desired point, publish to Ros topic 
            //how to determine if this is a valid point???

            //need a two button action call, 1) see if point is valid, and 2) actually send command to  /goalpoint topic 

            //Figrue out how to do a summon command if robot is out in the floor going from point  A - Z and i'm in the office.

            //Send stop command (actionlib_msgs /cancel) 
            //How would I specify that I am at a specific location?

            //
        }
    }
}
