using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrGunMan : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private float f_moveSpeed;
    [Header("Attack Parameters")]
    [SerializeField] private Transform t_firePoint;
    [SerializeField] private ManaBar mb_mana;
    [SerializeField] private float f_attackCost;
    private PlayerInputs pi_inputs;
    private Rigidbody rb;

    public ManaBar Mana { get { return mb_mana; } }
    
    void Start()
    {
        pi_inputs = GetComponent<PlayerInputs>();
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && mb_mana.CanCast(f_attackCost))
        {
            GameObject _bullet = PoolManager.x.SpawnObject("Bullet", t_firePoint.position, transform.forward * 30f, t_firePoint.rotation);
            _bullet.GetComponent<Bullet>().SetOwner(gameObject);
            mb_mana.ReduceMana(f_attackCost);
        }
    }

    private void LateUpdate()
    {
        rb.velocity = pi_inputs.MoveDirection * f_moveSpeed;
    }
}
