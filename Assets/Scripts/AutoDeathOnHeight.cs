using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
public class AutoDeathOnHeight : MonoBehaviour
{
    [SerializeField] float DeathHeight;
    // Update is called once per frame
    void Update()
    {
        if (IsBelowDeathHeight()) GlobalEvents.Instance.OnPlayerDeath?.Invoke();
    }

    bool IsBelowDeathHeight()
    {
        return transform.position.y < DeathHeight ? true : false;
    }
}
