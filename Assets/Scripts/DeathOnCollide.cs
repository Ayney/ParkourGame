using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Managers;
namespace Obstacles
{
    public class DeathOnCollide : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            GlobalEvents.Instance.OnPlayerDeath?.Invoke();
        }
    }
}


