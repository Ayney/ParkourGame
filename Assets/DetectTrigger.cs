using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTrigger : MonoBehaviour
{

    public List<GameObject> ObjectsInTrigger;
    public bool ObjectWithinRange;

    private void OnTriggerEnter(Collider other)
    {
        ObjectsInTrigger.Add(other.gameObject);
        ObjectWithinRange = true;
    }
    private void OnTriggerStay(Collider other)
    {
        ObjectWithinRange = true;
    }
    private void OnTriggerExit(Collider other)
    {
        ObjectsInTrigger.Remove(other.gameObject);
        ObjectWithinRange = false;
    }

    public GameObject NearestGameObject()
    {
        GameObject NearestGameObject = null;
        float CurrentDistanceOfNGO = 999;
        for (int i = 0; i < ObjectsInTrigger.Count; i++)
        {
            if (Vector3.Distance(transform.position, ObjectsInTrigger[i].transform.position) < CurrentDistanceOfNGO)
            {

                NearestGameObject = ObjectsInTrigger[i];
                CurrentDistanceOfNGO = Vector3.Distance(transform.position, ObjectsInTrigger[i].transform.position);
            }
        }
        return NearestGameObject;
    }


}
