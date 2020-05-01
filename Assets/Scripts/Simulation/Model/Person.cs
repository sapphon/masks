using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class Person : ParameterDrivenBehavior, IMaskable
{
    public Boolean isInfected { get; private set; }
    private float timeOfInfection { get; set; }
    public Boolean isMasked { get; private set; }
    private Material normalMaterial;
    private Location destination;

    void Start()
    {
        base.Start();
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
            timeOfInfection = Time.time;
            chooseMaterial();
        }
    }

    protected virtual void becomeContagious()
    {
        SneezerFactory.makeSneezer(this, this.gameObject);
    }

    public void mask()
    {
        this.isMasked = true;
        chooseMaterial();
    }

    public void unmask()
    {
        this.isMasked = false;
        chooseMaterial();
    }

    void Update()
    {
        checkArrival();
        checkContagion();
    }

    void checkContagion()
    {
        if (this.isInfected)
        {
            if (FloatingPointComparer.isBetweenInclusive(
                this.timeOfInfection + this.simulationParameters.infectionLatencyTime, Time.time,
                this.timeOfInfection + simulationParameters.infectionContagionTime +
                simulationParameters.infectionLatencyTime))
            {
                if (this.GetComponent<Sneezer>() == null)
                {
                    this.becomeContagious();
                }
            }
            else if (timeOfInfection + simulationParameters.infectionContagionTime + simulationParameters.infectionLatencyTime < Time.time)
            {
                this.isInfected = false;
                chooseMaterial();
            }
        }
        }

        void checkArrival()
        {
            if (Vector3.Distance(this.transform.position, destination.transform.position) < 2)
            {
                chooseDestinationAndSpeed();
            }
        }

        void chooseMaterial()
        {
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            MaterialCache materials = GameObject.FindObjectOfType<MaterialCache>();

            if (this.isInfected && this.isMasked)
            {
                renderer.material = materials.characterInfectedAndMaskedMaterial;
            }

            else if (this.isInfected)
            {
                renderer.material = materials.characterInfectedMaterial;
            }

            else if (this.isMasked)
            {
                renderer.material = materials.characterMaskedMaterial;
            }
            else
            {
                renderer.material = this.normalMaterial;
            }
        }
    }