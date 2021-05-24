using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPBar : MonoBehaviour
{
    public Image hpbar;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetHp(int hp)
    {
        hpbar.fillAmount = (float)hp / 100f;
    }
}
