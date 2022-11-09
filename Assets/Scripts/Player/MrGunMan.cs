using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrGunMan : MonoBehaviour
{
    [SerializeField] private bool b_usePool;
    [Header("Movement Parameters")]
    [SerializeField] private float f_moveSpeed;
    [SerializeField] private GameObject _base;
    [Header("Attack Parameters")]
    [SerializeField] private Transform t_firePoint;
    [SerializeField] private ManaBar mb_mana;
    [SerializeField] private float f_attackCost;
    [SerializeField] private int i_numberOfBullets;
    private PlayerInputs pi_inputs;
    private Rigidbody rb;

    public ManaBar Mana { get { return mb_mana; } }
    [Space]
    [SerializeField] private Transform t_camParent;
    [SerializeField] private float f_lookSpeed;
    private float f_camRot;


    void Start()
    {
        pi_inputs = GetComponent<PlayerInputs>();
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        if (pi_inputs.Attacking && mb_mana.CanCast(f_attackCost))
        {
            if(b_usePool)
                ShootWithPool();
            else
                ShootWithInstantiate();
        }

        // This is a nightmare...
        f_camRot = Mathf.Clamp(f_camRot - Input.GetAxis("Mouse Y") * Time.deltaTime * f_lookSpeed, -80, 70);

        t_camParent.localEulerAngles = Vector3.right * f_camRot; // (1,0,0)
        transform.localEulerAngles += Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * f_lookSpeed;
    }

    private void ShootWithPool()
    {
        Vector3 _randPos = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
        GameObject[] _bullets = PoolManager.x.SpawnMultipleObjects("Bullet", i_numberOfBullets, t_firePoint.position, Vector3.one - Vector3.up, t_camParent.forward * 30f, Quaternion.identity);
        mb_mana.ReduceMana(f_attackCost);
    }

    private void ShootWithInstantiate()
    {
        for (int i = 0; i < i_numberOfBullets; i++)
        {
            Vector3 _randPos = t_firePoint.position;
            _randPos.y += Random.Range(-1f, 1f);
            _randPos.x += Random.Range(-1f, 1f);
            GameObject _bullet = Instantiate(_base);
            _bullet.transform.position = _randPos;
            _bullet.GetComponent<Rigidbody>().velocity = t_camParent.forward * 30f;
            _bullet.SetActive(true);
            _bullet.GetComponent<Bullet>().SetOwner(gameObject, false);
        }
        mb_mana.ReduceMana(f_attackCost);
    }

    private void LateUpdate()
    {
        rb.velocity = (pi_inputs.MoveDirection * f_moveSpeed) + (Vector3.up * rb.velocity.y);
    }


}
