using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.PlayerLoop;

public class Person : MonoBehaviour
{
    private Vector3 velocity;
    public Boolean isInfected { get; private set; }
    public Material infectedMaterial;
    private Material normalMaterial;
    private Location destination;

    void Start()
    {
        chooseDestinationAndSpeed();
        saveNormalMaterial();
    }

    private void chooseDestinationAndSpeed()
    {
        chooseMovementDestination();
        chooseWalkSpeed();
    }

    void saveNormalMaterial()
    {
        this.normalMaterial = GetComponent<MeshRenderer>().material;
    }

    void chooseMovementDestination()
    {
        var possibleDestinations = GameObject.FindObjectsOfType<Location>();
        this.destination = possibleDestinations[UnityEngine.Random.Range(0, possibleDestinations.Length)];
        GetComponent<NavMeshAgent>().destination = destination.gameObject.transform.position;
    }

    void chooseWalkSpeed()
    {
        GetComponent<NavMeshAgent>().speed = UnityEngine.Random.Range(5, 10);
    }

    public void infect()
    {
        if (!this.isInfected)
        {
            this.isInfected = true;
            GetComponent<MeshRenderer>().material = this.infectedMaterial;
        }
    }

    void Update()
    {
        checkArrival();
    }

    void checkArrival()
    {
        if (Vector3.Distance(this.transform.position, destination.transform.position) < 2)
        {
            chooseDestinationAndSpeed();
        }
    }
}
