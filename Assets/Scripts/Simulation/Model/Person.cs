using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.U2D;

public class Person : ParameterDrivenBehavior, IMaskable, IInfectable
{
    private Location destination;
    private Material normalMaterial;
    public bool isRecovered;
    public bool isInfected { get; private set; }
    private float timeOfInfection { get; set; }
    public bool isMasked { get; private set; }

    public void mask()
    {
        isMasked = true;
        chooseMaterial();
    }

    internal override void Start()
    {
        base.Start();
        chooseDestinationAndSpeed();
        saveNormalMaterial();
    }

    private void chooseDestinationAndSpeed()
    {
        chooseMovementDestination();
        chooseWalkSpeed(simulationParameters.personWalkSpeedAvg);
    }

    private void saveNormalMaterial()
    {
        normalMaterial = GetComponent<MeshRenderer>().material;
    }

    private void chooseMovementDestination()
    {
        var possibleDestinations = FindObjectsOfType<Location>();
        destination = possibleDestinations[Random.Range(0, possibleDestinations.Length)];
        GetComponent<NavMeshAgent>().destination = destination.gameObject.transform.position;
    }

    public void chooseWalkSpeed(float average)
    {
        GetComponent<NavMeshAgent>().speed = Random.Range(.5f * average, 1.5f * average);
    }

    public void infect()
    {
        if (!isInfected)
        {
            isInfected = true;
            timeOfInfection = Time.time;
            chooseMaterial();
        }
    }

    protected virtual void becomeContagious()
    {
        SneezerFactory.makeSneezer(this, gameObject);
    }

    public void unmask()
    {
        isMasked = false;
        chooseMaterial();
    }

    private void Update()
    {
        checkArrival();
        checkContagion();
    }

    private void checkContagion()
    {
        if (isInfected)
        {
            if (FloatingPointComparer.isBetweenInclusive(
                timeOfInfection + simulationParameters.infectionLatencyTime, Time.time,
                timeOfInfection + simulationParameters.infectionContagionTime +
                simulationParameters.infectionLatencyTime))
            {
                if (GetComponent<Sneezer>() == null) becomeContagious();
            }
            else if (timeOfInfection + simulationParameters.infectionContagionTime +
                     simulationParameters.infectionLatencyTime < Time.time)
            {
                isInfected = false;
                isRecovered = true;
                chooseMaterial();
            }
        }
    }

    private void checkArrival()
    {
        if (Vector3.Distance(transform.position, destination.transform.position) < 2) chooseDestinationAndSpeed();
    }

    private void chooseMaterial()
    {
        var renderer = GetComponent<MeshRenderer>();
        var materials = FindObjectOfType<MaterialCache>();

        if (isRecovered)
            renderer.material = materials.characterRecoveredMaterial;
        else if (isInfected && isMasked)
            renderer.material = materials.characterInfectedAndMaskedMaterial;

        else if (isInfected)
            renderer.material = materials.characterInfectedMaterial;

        else if (isMasked)
            renderer.material = materials.characterMaskedMaterial;
        else
            renderer.material = normalMaterial;
    }
}