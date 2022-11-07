using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class houses objects we want to have multiple of.
/// </summary>
public class Pool
{
    private string s_poolName;
    private List<GameObject> L_pool = new List<GameObject>();

    /// <summary>
    /// This is a constructor for the Pool class. Because it isn't a Monobehaviour we have to create our own function that initializes the class.
    /// </summary>
    /// <param name="_base">The type of object we want our pool to contain.</param>
    /// <param name="_size">How many of those objects we want.</param>
    public Pool(GameObject _base, int _size = 0)
    {
        s_poolName = _base.name;
        if (_size == 0)
            _size = 10;
        L_pool.Add(_base);
        L_pool = CreatePool(_base, _size);
    }

    /// <summary>
    /// Creates the pool of object to be spawned
    /// </summary>
    /// <param name="_base">The base object we want created.</param>
    /// <param name="_size">How many we want of these objects.</param>
    /// <returns>The list of objects we've created.</returns>
    private List<GameObject> CreatePool(GameObject _base, int _size)
    {
        for (int i = 0; i < _size; i++)
        {
            GameObject _newObject = GameObject.Instantiate(_base);
            _newObject.transform.position = Vector3.one * 100; // Stores our object at 100, 100, 100.
            _newObject.SetActive(false);
            // When unity Instantiates an object, it adds (clone) to it's name, however we don't want this so we remove the clone from the name.
            _newObject.name = _newObject.name.Remove(s_poolName.Length);
            // Adds the object to the pool
            L_pool.Add(_newObject);
        }
        return L_pool;
    }

    /// <summary>
    /// Searches L_pool for an object not being used.
    /// </summary>
    /// <returns>The first inactive object from the pool.</returns>
    private GameObject GetObjectFromPool()
    {
        // Searching the pool for the next inactive object.
        foreach(GameObject _obj in L_pool)
        {
            if (!_obj.activeInHierarchy)
                return _obj;
        }
        // I'm not yet resizing the pool so for now if you're out of object we return nothing.
        return null;
    }

    /// <summary>
    /// Spawn an object from this pool.
    /// </summary>
    /// <param name="_position">Where we want the object.</param>
    /// <param name="_velocity">The velocity we want the object to have.</param>
    /// <param name="_rotation">What rotation the object should have.</param>
    /// <returns></returns>
    public GameObject SpawnObjectFromPool(Vector3 _position, Vector3 _velocity, Quaternion _rotation)
    {
        // Get our object from the pool.
        GameObject _target = GetObjectFromPool();
        _target.SetActive(true);
        // Initialize it's position and rotation.
        _target.transform.position = _position;
        _target.transform.rotation = _rotation;
        // Check to see if we have something to set the velocity.
        Rigidbody rb = _target.GetComponent<Rigidbody>();
        if (rb)
            rb.velocity = _velocity;
        // Turn the object on.
        return _target;
    }

    /// <summary>
    /// Resets all the transform values of the object and sets it inactive.
    /// </summary>
    /// <param name="_target">The object we want to kill.</param>
    public void ReturnObjectToPool(GameObject _target)
    {
        // Stoping the objects momentum
        Rigidbody rb = _target.GetComponent<Rigidbody>();
        if (rb)
            rb.velocity = Vector3.zero;

        _target.transform.position = Vector3.one * 100; // Storing our object back at 100, 100, 100
        _target.transform.rotation = Quaternion.identity; // Resetting it's rotation
        _target.SetActive(false);
    }

    public string GetName()
    {
        return s_poolName;
    }
}
