using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage;
    public int health;
    public float flashtime;
    public GameObject bloodEffect;

    private Color originalColor;
    private SpriteRenderer sr;
    private PlayerHealth playerHealth;
    // Start is called before the first frame update
    protected void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        FlashColor(flashtime);
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
    }
    void FlashColor(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColor", time);
    }

    void ResetColor()
    {
        sr.color = originalColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player")&&collision.GetType().ToString()== "UnityEngine.CapsuleCollider2D")
        {
            if(playerHealth != null) 
            
            { 
            playerHealth.DamagePlayer(damage);
                SoundManager.PlayerHit();
            }
           
        }
    }
}
