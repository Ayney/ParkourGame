using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [SerializeField] float RotationSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.Euler( Vector3.up* RotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.root.SetParent(transform, true);

    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.parent.SetParent(null);
    }
}
