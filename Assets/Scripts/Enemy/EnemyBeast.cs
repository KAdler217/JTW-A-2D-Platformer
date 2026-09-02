using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeast : Enemy
{
    // Start is called before the first frame update
    private  Animator anim;
    public static bool trigger;
    void Start()
    {
        base.Start();
        trigger = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        Burn();
        
    }
    public void Burn()
    {
        if (trigger)
        {
            anim.Play("Beast_burn");
            
            Destroy(gameObject, 1.0f);
        }
    }
}
