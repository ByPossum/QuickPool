using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField] private float f_maxMana;
    [SerializeField] private Image i_bar;
    private float f_currentMana;
    // Start is called before the first frame update
    void Start()
    {
        f_currentMana = f_maxMana;
        i_bar.fillAmount = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        i_bar.fillAmount = f_currentMana / f_maxMana;
    }

    public void ReduceMana(float _amount)
    {
        f_currentMana -= Mathf.Clamp(_amount, 0, f_maxMana);
    }

    public void IncreaseMana(float _amount)
    {
        f_currentMana += Mathf.Clamp(_amount, 0, f_maxMana);
    }

    public bool CanCast(float _cost)
    {
        return f_currentMana - _cost > 0;
    }
}
