using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A bullet in our game.
/// </summary>
public class Bullet : MonoBehaviour
{
    // The bullet's owner
    private GameObject go_owner;

    // OnCollisionEnter is a unity function that gives you the collider of whatever is currently colliding with this object.
    private void OnCollisionEnter(Collision collision)
    {
        // Kill this object when colliding with anything other than it's owner
        if (collision.gameObject != go_owner)
            PoolManager.x.KillObject(gameObject);
    }

    /// <summary>
    /// This makes sure the object doesn't kill itself when it collides with the object that spawns it.
    /// </summary>
    /// <param name="_newOwner">The object we want to not kill it.</param>
    public void SetOwner(GameObject _newOwner)
    {
        go_owner = _newOwner;
    }
}
