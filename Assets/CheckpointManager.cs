using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    [SerializeField] Transform PlayerTransform;
    [SerializeField] Transform StartingPoint;

    public Transform CurrentCheckpoint;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != null && Instance != this) Destroy(this);
    }

    private void Start()
    {
        CurrentCheckpoint = StartingPoint;
    }
    public void TeleportPlayerToLastCheckpoint()
    {
        PlayerTransform.position = CurrentCheckpoint.position;
    }
}
