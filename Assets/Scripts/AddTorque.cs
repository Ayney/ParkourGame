using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTorque : MonoBehaviour
{
    [SerializeField] float XTorque;
    [SerializeField] float YTorque;
    [SerializeField] float ZTorque;
    void Update()
    {
        transform.rotation *= Quaternion.Euler(XTorque * Time.deltaTime, YTorque * Time.deltaTime, ZTorque * Time.deltaTime);
    }
}
