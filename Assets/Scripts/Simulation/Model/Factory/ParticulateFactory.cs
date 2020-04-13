using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ParticulateFactory
{
    public static GameObject makeParticulateCloud(Transform origin, Quaternion direction)
    {
        GameObject cloud = Resources.Load("Prefabs/Particulate") as GameObject;
        return GameObject.Instantiate(cloud, findCenterGivenOriginDirectionAndLength(origin.position, direction, cloud.GetComponent<BoxCollider>().size.z), direction);
    }

    private static Vector3 findCenterGivenOriginDirectionAndLength(Vector3 origin, Quaternion direction, float length)
    {
        return origin + (direction.eulerAngles.normalized * (length/2));
    }
}
