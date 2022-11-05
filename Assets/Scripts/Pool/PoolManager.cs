using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // This means the object is what's called a singleton. This is another programming patter which we'll discuss at a later date
    public static PoolManager x;

    // SerializeField means we can read/write this variable in the inspector.
    [SerializeField] GameObject[] goA_basePoolObjects;
    // One thing to note, having 2 seperate arrays to manage the objects and their sizes is bad.
    // There are ways around this but they're outside of the scope of this session.
    [SerializeField] private int[] iA_poolSizes;
    // This is the actual pool we'll be getting our objects from
    private Pool[] p_pools;

    // Awake is called before Start which means this is likely to be one of the first things that's set up.
    void Awake()
    {
        // This is us ensuring we don't accidently create multiple versions of our manager.
        if (x != null)
        {
            Destroy(this);
        }
        else
            x = this;

        // Here we're initializing our Pool array to be the same size as the number of objects we want to be pooled.
        p_pools = new Pool[goA_basePoolObjects.Length];
        for (int i = 0; i < goA_basePoolObjects.Length; i++)
        {
            // This is a cheeky way of guarding against potential size mismatches between goA_basePoolObjects and iA_poolSizes
            int poolSize = 0;
            if (iA_poolSizes.Length > i)
                poolSize = iA_poolSizes[i];
            // Here we're calling the constructor we created in our Pool class using the "new" keyword.
            p_pools[i] = new Pool(goA_basePoolObjects[i], poolSize);
        }
    }

    public GameObject SpawnObject(string _objectName, Vector3 _position, Vector3 _velocity, Quaternion _rotation)
    {
        for (int i = 0; i < p_pools.Length; i++)
        {
            if (p_pools[i].GetName() == _objectName)
                return p_pools[i].SpawnObjectFromPool(_position, _velocity, _rotation);
        }
        Debug.LogError("The object you're asking for is not in the pool.");
        return null;
    }

    /// <summary>
    /// Returns an object to it's pool.
    /// </summary>
    /// <param name="_objectToKill"></param>
    public void KillObject(GameObject _objectToKill)
    {
        for (int i = 0; i < p_pools.Length; i++)
        {
            if (p_pools[i].GetName() == _objectToKill.name)
            {
                p_pools[i].ReturnObjectToPool(_objectToKill);
                return;
            }
        }
        Debug.LogError("There was no pool to return the object to.");
    }
}
