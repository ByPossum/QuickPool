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

    Dictionary<string, Pool> D_pools = new Dictionary<string, Pool>();

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
        for (int i = 0; i < goA_basePoolObjects.Length; i++)
        {
            // This is a cheeky way of guarding against potential size mismatches between goA_basePoolObjects and iA_poolSizes
            int poolSize = 0;
            if (iA_poolSizes.Length > i)
                poolSize = iA_poolSizes[i];
            // Here we're calling the constructor we created in our Pool class using the "new" keyword.
            D_pools.Add(goA_basePoolObjects[i].name, new Pool(goA_basePoolObjects[i], poolSize));
            //p_pools[i] = new Pool(goA_basePoolObjects[i], poolSize);
        }

    }

    /// <summary>
    /// This is O(n)
    /// </summary>
    /// <param name="_objectName"></param>
    /// <param name="_position"></param>
    /// <param name="_velocity"></param>
    /// <param name="_rotation"></param>
    /// <returns></returns>
    public GameObject SpawnObject(string _objectName, Vector3 _position, Vector3 _velocity, Quaternion _rotation)
    {
        if (D_pools.ContainsKey(_objectName))
            return D_pools[_objectName].SpawnObjectFromPool(_position, _velocity, _rotation);
        Debug.LogError("The object you're asking for is not in the pool.");
        return null;
    }

    public GameObject[] SpawnMultipleObjects(string _objectName, int _amount, Vector3 _position, Vector3 _offset, Vector3 _velocity, Quaternion _rotation)
    {
        Pool pool = GetPool(_objectName);
        GameObject[] objs = pool.SpawnMany(_amount);
        for (int i = 0; i < objs.Length; i++)
        {
            objs[i].SetActive(true);
            _position.x += Random.Range(-_offset.x, _offset.x);
            _position.y += Random.Range(-_offset.y, _offset.y);
            _position.z += Random.Range(-_offset.z, _offset.z);
            objs[i].transform.position = _position;
            objs[i].transform.rotation = _rotation;
            objs[i].GetComponent<Rigidbody>().velocity = _velocity;
        }
        return objs;
    }

    private Pool GetPool(string _poolName)
    {
        if (D_pools.ContainsKey(_poolName))
            return D_pools[_poolName];
        Debug.LogError($"No pool named {_poolName}");
        return null;
    }

    /// <summary>
    /// Returns an object to it's pool.
    /// </summary>
    /// <param name="_objectToKill"></param>
    public void KillObject(GameObject _objectToKill, string _poolName)
    {
        D_pools[_poolName].ReturnObjectToPool(_objectToKill);
        //Debug.LogError("There was no pool to return the object to.");
    }
}
