using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buymenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject buymenu;
    public static bool hasGun = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PickUp.pickup)
        {
            buymenu.SetActive(true);
        }
    }

    public void Buy()
    {
        if (CoinUI.currentCoin >= 20)
        {
            CoinUI.currentCoin -= 20;
            hasGun = true;
            PickUp.pickup = false;
            buymenu.SetActive(false);
        }
    }
    public void Cancel()
    {

        
        PickUp.pickup = false;
        buymenu.SetActive(false);
        
    }
}
