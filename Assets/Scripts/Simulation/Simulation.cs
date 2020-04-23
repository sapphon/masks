using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Simulation : MonoBehaviour, IObserver<INormalizedParameterChange>
{
    private List<Person> people;
    private float infectionRadius;
    private SimulationParameters simulationParameters;
    
    void Start()
    {
        people = new List<Person>(GameObject.FindObjectsOfType<Person>());
        simulationParameters = GameObject.FindObjectOfType<SimulationParameters>();
        infectionRadius = 3;
        infectPatientZero();
        adjustPopulationMasking(simulationParameters.percentOfPopulationMasked);
        subscribeToObservables();
    }

    private void subscribeToObservables()
    {
        simulationParameters.Subscribe(this);
    }

    void Update()
    {
        if (simulationParameters.infectOthersWithinInfectionRadius)
        {
            infectPeopleBasedOnDistance();
        }
    }

    void infectPeopleBasedOnDistance()
    {
        for (int i = 0; i < people.Count; i++)
        {
            for (int j = i + 1; j < people.Count; j++)
            {
                if (infectionPossible(people[i], people[j]) && withinInfectionDistance(people[i], people[j]))
                {
                    infect(new List<Person>{people[i], people[j]});
                }
            }
        }
    }

    private Boolean infectionPossible(Person personA, Person personB)
    {
        return personA.isInfected != personB.isInfected;
    }
    
    private Boolean withinInfectionDistance(Person personA, Person personB)
    {
        return Vector3.Distance(personA.transform.position, personB.transform.position) <= infectionRadius;
    }

    private void infect(List<Person> people)
    {
        people.ForEach((person) => person.infect());
    }

    private void infectPatientZero()
    {
        people[new System.Random().Next(people.Count)].infect();
    }

    private void adjustPopulationMasking(float whatPercentNormalized)
    {
        int numberOfMaskedToReach = Convert.ToInt32(Math.Round(people.Count  * whatPercentNormalized));
        int numberAlreadyMasked = getMaskedPeople().Count();
        if (numberAlreadyMasked == numberOfMaskedToReach) return;
        else if (numberAlreadyMasked < numberOfMaskedToReach)
        {
            maskPeople(numberOfMaskedToReach - numberAlreadyMasked);
        }
        else
        {
            unmaskPeople(numberAlreadyMasked - numberOfMaskedToReach);
        }
    }

    void maskPeople(int numberToMask)
    {
        HashSet<Person> initiallyUnmaskedPeople = getUnmaskedPeople();
        System.Random random = new System.Random();
        foreach(Person p in initiallyUnmaskedPeople.OrderBy(x => random.Next()).Take(numberToMask))
        {
            p.mask();
        }
    }
    
    void unmaskPeople(int numberToUnmask)
    {
        HashSet<Person> initiallyMaskedPeople = getMaskedPeople();
        System.Random random = new System.Random();
        foreach(Person p in initiallyMaskedPeople.OrderBy(x => random.Next()).Take(numberToUnmask))
        {
            p.unmask();
        }
    }

    public HashSet<Person> getMaskedPeople()
    {
        return new HashSet<Person>(people.FindAll(delegate(Person person) { return person.isMasked; }));
    }
    
    public HashSet<Person> getUnmaskedPeople()
    {
        return new HashSet<Person>(people.FindAll(delegate(Person person) { return !person.isMasked; }));
    }

    //IObserver<ISimulationParametersData> implementation
    public void OnCompleted(){}

    public void OnError(Exception error)
    {}

    public void OnNext(INormalizedParameterChange value)
    {
        switch (value.getParameterName())
        {
            case "populationMaskPercentage":
                this.adjustPopulationMasking(value.getParameterValue());
                break;
        }
        

    }
}
