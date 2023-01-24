using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target; // assign the player object in the inspector
    [SerializeField] private float smoothSpeed = 0.125f;
    public bool isFollowing = true; // start following the player by default
    [SerializeField] private Vector3 locationOffset;
    [SerializeField] private Vector3 rotationOffset;

    void LateUpdate()
    {
        if (isFollowing)
        {
            Vector3 desiredPosition = new Vector3(target.position.x,0,target.position.z) + target.rotation * locationOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            Quaternion desiredrotation = target.rotation * Quaternion.Euler(rotationOffset);
            Quaternion smoothedrotation = Quaternion.Lerp(transform.rotation, desiredrotation, smoothSpeed);
            transform.rotation = smoothedrotation;
        }
        
    }

    public void StopFollowing()
    {
        isFollowing = false;
    }
}