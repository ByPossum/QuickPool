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
        if (pi_inputs.Attacking && mb_mana.CanCast(f_attackCost))
        {
            for(int i = 0; i < 20; i++)
            {
                Vector3 _randPos = t_firePoint.position;
                _randPos.y += Random.Range(-1f, 1f);
                _randPos.x += Random.Range(-1f, 1f);
                GameObject _bullet = PoolManager.x.SpawnObject("Bullet", _randPos, transform.forward * 30f, t_firePoint.rotation);
                _bullet.GetComponent<Bullet>().SetOwner(gameObject);
            }
            mb_mana.ReduceMana(f_attackCost);
        }
    }

    private void LateUpdate()
    {
        rb.velocity = pi_inputs.MoveDirection * f_moveSpeed;
    }
}
