using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectiousVolume : MonoBehaviour
{
    private Dictionary<GameObject, float> objectsEntered;

    void Awake()
    {
        objectsEntered = new Dictionary<GameObject, float>();
    }

    private void OnTriggerEnter(Collider other)
    {
        this.objectsEntered.Add(other.gameObject, Time.timeSinceLevelLoad);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("EXIT");
        Person exiter = other.gameObject.GetComponent<Person>();
        if (exiter != null)
        {
            float timeSpentInArea = Time.timeSinceLevelLoad - this.objectsEntered[other.gameObject];
            Debug.Log("Time Spent: " + timeSpentInArea);
            if (timeSpentInArea > 1 * (exiter.isMasked ? 3 : 1))
            {
                exiter.infect();
            }
        }
        this.objectsEntered.Remove(other.gameObject);
    }
}
