using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ParticulateFactory
{
    public static GameObject makeParticulateCloud(Transform origin, Quaternion direction)
    {
        GameObject resource = Resources.Load("Prefabs/Particulate") as GameObject;
        GameObject cloud = GameObject.Instantiate(resource,
            findCenterGivenOriginDirectionAndLength(origin.position, direction,
                resource.GetComponent<BoxCollider>().size.z), direction);
        return cloud;
    }
    
    public static GameObject makeMaskedParticulateCloud(Transform origin, Quaternion direction)
    {
        GameObject resource = (GameObject)Resources.Load("Prefabs/Particulate");
        GameObject cloud = GameObject.Instantiate(resource, findCenterGivenOriginDirectionAndLength(origin.position, direction, resource.GetComponent<BoxCollider>().size.z * GameObject.FindObjectOfType<SimulationParameters>().maskedSneezeCloudSizePercent), direction);
        cloud.transform.localScale = new Vector3(cloud.transform.localScale.x, cloud.transform.localScale.y, GameObject.FindObjectOfType<SimulationParameters>().maskedSneezeCloudSizePercent);
        Debug.Log("Local scale is now: " + cloud.transform.localScale);
        return cloud;
    }

    private static Vector3 findCenterGivenOriginDirectionAndLength(Vector3 origin, Quaternion direction, float length)
    {
        return origin + (direction.eulerAngles.normalized * (length / 2));// - new Vector3(0,1.75f,0);
    }
}
