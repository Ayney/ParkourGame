using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float Speed; 
    int checkpointToGoTo = 0;
    bool IsGoingClockwise = true;
    Rigidbody rb => GetComponent<Rigidbody>();

    [SerializeField] Transform[] Checkpoints;
    Vector3 Direction;
    

    float DistanceToNextCheckpoint()
    {
        return (transform.position - Checkpoints[checkpointToGoTo].position).magnitude;
    }

    private void Start()
    {
        SetMoveDirectionToCheckpoint(1);
    }
    private void Update()
    {
        if (DistanceToNextCheckpoint() > 0.2f) transform.Translate(Speed * Time.deltaTime * Direction);
        else CheckAndSetNextCheckpoint();
    }

    void CheckAndSetNextCheckpoint()
    {
        if (checkpointToGoTo + 1 >= Checkpoints.Length) IsGoingClockwise = false;
        else if (checkpointToGoTo - 1 < 0) IsGoingClockwise = true;
            
        if(IsGoingClockwise) SetMoveDirectionToCheckpoint(checkpointToGoTo + 1);
        else if (!IsGoingClockwise) SetMoveDirectionToCheckpoint(checkpointToGoTo - 1);
    }

    void SetMoveDirectionToCheckpoint(int checkpoint)
    {
        checkpointToGoTo = checkpoint;
        Direction = (Checkpoints[checkpoint].localPosition - transform.localPosition).normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.root.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.parent.SetParent(null);
    }
}
