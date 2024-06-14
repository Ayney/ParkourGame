using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
public class EndColTrig : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GlobalEvents.Instance.OnGettingToEnd?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        GlobalEvents.Instance.OnGettingToEnd?.Invoke();
    }
}
