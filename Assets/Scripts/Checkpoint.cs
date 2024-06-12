using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] Transform NewCheckpoint;
    [SerializeField] UnityEvent OnThisCheckpointSet;
    private void OnTriggerEnter(Collider other)
    {
        CheckpointManager.Instance.ChangeCurrentCheckpoint(NewCheckpoint);
        OnThisCheckpointSet?.Invoke();
    }
}
