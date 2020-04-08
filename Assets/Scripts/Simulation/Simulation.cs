﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulation : MonoBehaviour
{
    private List<Person> people;
    private float infectionRadius;
    void Start()
    {
        infectionRadius = 3;
        people = new List<Person>(GameObject.FindObjectsOfType<Person>());
        infectPatientZero();
    }
    
    void Update()
    {
        infectPeopleBasedOnDistance();
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
        infect(new List<Person>{people[new System.Random().Next(people.Count)]});
    }
}
