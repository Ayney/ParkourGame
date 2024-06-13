using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class CheckpointManager : MonoBehaviour
    {
        public static CheckpointManager Instance;

        [SerializeField] Transform PlayerTransform;
        [SerializeField] Transform StartingPoint;
        [SerializeField] Vector3 Offset;
        public Transform CurrentCheckpoint;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else if (Instance != null && Instance != this) Destroy(this);
        }

        private void Start()
        {
            CurrentCheckpoint = StartingPoint;
            TeleportPlayerToLastCheckpoint();
        }
        public void TeleportPlayerToLastCheckpoint()
        {
            PlayerTransform.position = CurrentCheckpoint.position + Offset;
        }

        public void ChangeCurrentCheckpoint(Transform newCheckpoint)
        {
            CurrentCheckpoint = newCheckpoint;
            GlobalEvents.Instance.OnNewCheckpoint?.Invoke();
        }
    }

}
