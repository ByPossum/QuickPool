using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A bullet in our game.
/// </summary>
public class Bullet : MonoBehaviour
{
    private const float DEATHTIME = 3f;
    // The bullet's owner
    private GameObject go_owner;
    private float f_currentTime = 0f;

    private void OnEnable()
    {
        DeathTimer();
    }

    private void Update()
    {

    }

    // OnCollisionEnter is a unity function that gives you the collider of whatever is currently colliding with this object.
    private void OnCollisionEnter(Collision collision)
    {
        // Kill this object when colliding with anything other than it's owner
        if (collision.gameObject != go_owner && !collision.gameObject.GetComponent<Bullet>())
            Die();
    }

    private void Die()
    {
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

    private async void DeathTimer()
    {
        if (gameObject.activeInHierarchy)
        {
            for (float i = 0; i < DEATHTIME; i += Time.deltaTime)
            {
                f_currentTime += Time.deltaTime;
                await System.Threading.Tasks.Task.Yield();
            }
            Die();
        }
    }
}
