using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Sneezer : MonoBehaviour
{

    private float rateOfSneezeMinimum;

    private float rateOfSneezeMaximum;

    private float nextSneezeTime;

    public IMaskable maskable { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        this.rateOfSneezeMaximum = 25;
        this.rateOfSneezeMinimum = 5;
    }

    // Update is called once per frame
    void Update()
    {
        checkSneeze();
    }

    void checkSneeze()
    {
        if (Time.time > nextSneezeTime)
        {
            sneeze();
            setTimeOfNextSneeze();
        }
    }

    void sneeze()
    {
        Transform thisTransform = this.gameObject.transform;
        if (this.maskable != null && this.maskable.isMasked)
        {
            ParticulateFactory.makeMaskedParticulateCloud(thisTransform, getSneezeDirection());
        }
        else
        {
            ParticulateFactory.makeParticulateCloud(thisTransform, getSneezeDirection());
        }
    }

    void setTimeOfNextSneeze()
    {
        nextSneezeTime += UnityEngine.Random.Range(rateOfSneezeMinimum, rateOfSneezeMaximum);
    }

    Quaternion getSneezeDirection()
    {
        float angle = UnityEngine.Random.Range(-135f, 135f);
        return Quaternion.AngleAxis(angle, this.gameObject.transform.up);
    }
}
