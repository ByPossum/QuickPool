using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    private Vector3 v_moveDir;
    private bool b_attacking;
    public Vector3 MoveDirection { get { return v_moveDir; } }
    public bool Attacking { get { return b_attacking; } }
    

    void Start()
    {
        
    }

    void Update()
    {
        v_moveDir.x = Input.GetKey(KeyCode.A) ? -1 : Input.GetKey(KeyCode.D) ? 1 : 0;
        v_moveDir.z = Input.GetKey(KeyCode.S) ? -1 : Input.GetKey(KeyCode.W) ? 1 : 0;
        v_moveDir.Normalize();
        b_attacking = Input.GetMouseButtonDown(0);
    }
}
