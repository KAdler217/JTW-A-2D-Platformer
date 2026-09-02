using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public static int health;
    public float time;
    private Animator anim;
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        health = 20;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void Die()
    {
        health = 0;
        HealthBar.HealthCurrent = health;
        anim.SetTrigger("Die");
        Diemenu.GameIsOver = true;
        Destroy(gameObject, 2.0f);
        
    }
    public void DamagePlayer(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            health = 0;
        }
        HealthBar.HealthCurrent = health;
        if(health <= 0)
        {
            Die();
        }
        else
        {
            anim.Play("Hit");        
        }
       
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Die"))
        {
            health = 0;
            Destroy(gameObject);
            Diemenu.GameIsOver = true;
            HealthBar.HealthCurrent = health;
        }
        if (collision.gameObject.CompareTag("Spike"))
        {
            health -= 4;
            if (health == 0) Die();
            else {
                anim.Play("Hit");
                HealthBar.HealthCurrent = health;
            }
            
        }
        if (collision.gameObject.CompareTag("Trigger"))
        {
            EnemyBeast.trigger = true;
            
            health -= 4;
            if (health == 0) Die();
            else
            {
                anim.Play("Hit");
                HealthBar.HealthCurrent = health;
            }
        }
    }



}
    
    

