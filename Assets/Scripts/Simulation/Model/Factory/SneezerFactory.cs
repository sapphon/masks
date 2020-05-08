using UnityEngine;

public class SneezerFactory : MonoBehaviour
{
    public static Sneezer makeSneezer(IMaskable sneezingThing, GameObject parent)
    {
        var added = parent.AddComponent<Sneezer>();
        added.maskable = sneezingThing;
        return added;
    }
}