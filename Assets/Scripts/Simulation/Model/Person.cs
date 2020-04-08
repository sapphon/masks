using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    private Vector3 velocity;
    private Boolean isInfected;
    public Material infectedMaterial;
    private Material normalMaterial;

    void Start()
    {
        chooseMovementVector();
        saveNormalMaterial();
    }

    void saveNormalMaterial()
    {
        this.normalMaterial = GetComponent<MeshRenderer>().material;
    }

    void chooseMovementVector()
    {
        this.velocity = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f)).normalized * UnityEngine.Random.Range(1f, 2f);
    }

    
    void move()
    {
        this.gameObject.transform.Translate(velocity * Time.deltaTime);
    }

    void FixedUpdate()
    {
        move();
    }

    void infect()
    {
        this.isInfected = true;
        GetComponent<MeshRenderer>().material = this.infectedMaterial;
    }


}
