using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class GlobalEvents : MonoBehaviour
    {
        public static GlobalEvents Instance;
        private void Awake()
        {
            if (Instance == null) Instance = this;
            else if (Instance != null && Instance != this) Destroy(this);
        }

        public UnityEvent OnCursorLocked;
        public UnityEvent OnCursorUnlocked;
        public UnityEvent OnPlayerDeath;
        public UnityEvent OnNewCheckpoint;
    }

}
