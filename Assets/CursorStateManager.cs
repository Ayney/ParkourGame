using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Managers
{
    public class CursorStateManager : MonoBehaviour
    {
        public static CursorStateManager Instance;
        private void Awake()
        {
            if (Instance == null) Instance = this;
            else if (Instance != null && Instance != this) Destroy(this);
        }


        public void LockCursorState()
        {
            Cursor.lockState = CursorLockMode.Locked;
            GlobalEvents.Instance.OnCursorLocked?.Invoke();
        }

        public void UnlockCursorState()
        {
            Cursor.lockState = CursorLockMode.None;
            GlobalEvents.Instance.OnCursorUnlocked?.Invoke();
        }

    }

}
