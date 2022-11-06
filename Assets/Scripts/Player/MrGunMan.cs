using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrGunMan : MonoBehaviour
{
    [SerializeField] private Transform t_firePoint;
    [SerializeField] private ManaBar mb_mana;
    [SerializeField] private float f_attackCost;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && mb_mana.CanCast(f_attackCost))
        {
            GameObject _bullet = PoolManager.x.SpawnObject("Bullet", t_firePoint.position, transform.forward * 30f, t_firePoint.rotation);
            _bullet.GetComponent<Bullet>().SetOwner(gameObject);
            mb_mana.ReduceMana(f_attackCost);
        }
    }
}
