using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPickup : MonoBehaviour
{
    [SerializeField] private float f_manaAmount;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<MrGunMan>())
        {
            collision.transform.GetComponent<MrGunMan>().Mana.IncreaseMana(f_manaAmount);
            PoolManager.x.KillObject(gameObject, name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<MrGunMan>())
        {
            other.transform.GetComponent<MrGunMan>().Mana.IncreaseMana(f_manaAmount);
            PoolManager.x.KillObject(gameObject, name);
        }
    }
}
