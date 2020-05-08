using UnityEngine;

public class ParticulateFactory
{
    public static GameObject makeParticulateCloud(Transform origin, Quaternion direction)
    {
        var resource = Resources.Load("Prefabs/Particulate") as GameObject;
        var cloud = Object.Instantiate(resource,
            findCenterGivenOriginDirectionAndLength(origin.position, direction,
                resource.GetComponent<BoxCollider>().size.z), direction);
        return cloud;
    }

    public static GameObject makeMaskedParticulateCloud(Transform origin, Quaternion direction)
    {
        var resource = (GameObject) Resources.Load("Prefabs/Particulate");
        var cloud = Object.Instantiate(resource,
            findCenterGivenOriginDirectionAndLength(origin.position, direction,
                resource.GetComponent<BoxCollider>().size.z *
                Object.FindObjectOfType<SimulationParameters>().maskedSneezeCloudSizePercent), direction);
        cloud.transform.localScale = new Vector3(cloud.transform.localScale.x, cloud.transform.localScale.y,
            Object.FindObjectOfType<SimulationParameters>().maskedSneezeCloudSizePercent);
        return cloud;
    }

    private static Vector3 findCenterGivenOriginDirectionAndLength(Vector3 origin, Quaternion direction, float length)
    {
        var aimpoint = origin + direction.eulerAngles.normalized * (length / 2);
        return new Vector3(aimpoint.x, 1, aimpoint.z);
    }
}