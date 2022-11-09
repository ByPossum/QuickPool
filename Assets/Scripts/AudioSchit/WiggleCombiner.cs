using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiggleCombiner : MonoBehaviour
{
    private float[] f_wave;
    // Start is called before the first frame update
    void Start()
    {
        f_wave = new float[100];
        int iter = 0;

        for (float i = 0; i < 1; i += 0.01f)
        {
            f_wave[iter] = Mathf.Sin(i);
            iter++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
