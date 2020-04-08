using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        chooseMovementVector();
    }

    void chooseMovementVector()
    {
        this.velocity = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f)).normalized * UnityEngine.Random.Range(1f, 2f);
    }

    
    void move()
    {
        this.gameObject.transform.Translate(velocity * Time.deltaTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move();
    }
}
