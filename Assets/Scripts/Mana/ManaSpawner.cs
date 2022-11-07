using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaSpawner : MonoBehaviour
{
    private ManaPickup mp_currentPickup;
    private float f_waitTime = 3f;
    private float f_currentTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if(mp_currentPickup == null || !mp_currentPickup.gameObject.activeInHierarchy)
        {
            if(f_currentTime >= f_waitTime)
            {
                mp_currentPickup = PoolManager.x.SpawnObject("ManaPickup", transform.position, Vector3.zero, transform.rotation).GetComponent<ManaPickup>();
                f_currentTime = 0f;
                return;
            }
            f_currentTime += Time.deltaTime;
        }
    }
}
