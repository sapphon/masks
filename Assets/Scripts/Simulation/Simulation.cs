using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class Simulation : MonoBehaviour, IObserver<INormalizedParameterChange>
{
    private float infectionRadius;
    private List<Person> people;
    private SimulationParameters simulationParameters;

    //IObserver<ISimulationParametersData> implementation
    public void OnCompleted()
    {
    }

    public void OnError(Exception error)
    {
    }

    public void OnNext(INormalizedParameterChange value)
    {
        switch (value.getParameterName())
        {
            case "populationMaskPercentage":
                adjustPopulationMasking(value.getParameterValue());
                break;
            case "particulateLifetimeAvg":
                break;
        }
    }
    //End IObserver implementation

    private void Start()
    {
        people = new List<Person>(FindObjectsOfType<Person>());
        simulationParameters = FindObjectOfType<SimulationParameters>();
        infectionRadius = 3;
        infectPatientZero();
        adjustPopulationMasking(simulationParameters.percentOfPopulationMasked);
        subscribeToObservables();
    }

    private void subscribeToObservables()
    {
        simulationParameters.Subscribe(this);
    }

    private void Update()
    {
        if (simulationParameters.infectOthersWithinInfectionRadius) infectPeopleBasedOnDistance();
    }

    private void infectPeopleBasedOnDistance()
    {
        for (var i = 0; i < people.Count; i++)
        for (var j = i + 1; j < people.Count; j++)
            if (infectionPossible(people[i], people[j]) && withinInfectionDistance(people[i], people[j]))
                infect(new List<Person> {people[i], people[j]});
    }

    private bool infectionPossible(Person personA, Person personB)
    {
        return personA.isInfected != personB.isInfected;
    }

    private bool withinInfectionDistance(Person personA, Person personB)
    {
        return Vector3.Distance(personA.transform.position, personB.transform.position) <= infectionRadius;
    }

    private void infect(List<Person> people)
    {
        people.ForEach(person => person.infect());
    }

    private void infectPatientZero()
    {
        people[new Random().Next(people.Count)].infect();
    }

    private void adjustPopulationMasking(float whatPercentNormalized)
    {
        var numberOfMaskedToReach = Convert.ToInt32(Math.Round(people.Count * whatPercentNormalized));
        var numberAlreadyMasked = getMaskedPeople().Count();
        if (numberAlreadyMasked == numberOfMaskedToReach) return;
        if (numberAlreadyMasked < numberOfMaskedToReach)
            maskPeople(numberOfMaskedToReach - numberAlreadyMasked);
        else
            unmaskPeople(numberAlreadyMasked - numberOfMaskedToReach);
    }

    private void maskPeople(int numberToMask)
    {
        var initiallyUnmaskedPeople = getUnmaskedPeople();
        var random = new Random();
        foreach (var p in initiallyUnmaskedPeople.OrderBy(x => random.Next()).Take(numberToMask)) p.mask();
    }

    private void unmaskPeople(int numberToUnmask)
    {
        var initiallyMaskedPeople = getMaskedPeople();
        var random = new Random();
        foreach (var p in initiallyMaskedPeople.OrderBy(x => random.Next()).Take(numberToUnmask)) p.unmask();
    }

    public HashSet<Person> getMaskedPeople()
    {
        return new HashSet<Person>(people.FindAll(delegate(Person person) { return person.isMasked; }));
    }

    public HashSet<Person> getUnmaskedPeople()
    {
        return new HashSet<Person>(people.FindAll(delegate(Person person) { return !person.isMasked; }));
    }
}