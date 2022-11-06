using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] private Vector3 v_moveDir;
    public Vector3 MoveDirection { get { return v_moveDir; } }

    void Start()
    {
        
    }

    void Update()
    {
        v_moveDir.x = Input.GetKey(KeyCode.A) ? -1 : Input.GetKey(KeyCode.D) ? 1 : 0;
        v_moveDir.z = Input.GetKey(KeyCode.S) ? -1 : Input.GetKey(KeyCode.W) ? 1 : 0;
        v_moveDir.Normalize();
    }
}
