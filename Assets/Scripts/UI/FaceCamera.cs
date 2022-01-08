using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Transform TargetToLookAt;

    private Transform _localTransform;

    private void Start()
    {
        _localTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        if(TargetToLookAt)
        {
            _localTransform.LookAt(2 * _localTransform.position - TargetToLookAt.position);
        }
    }
}
