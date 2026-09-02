using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerHealth playerHealth2;

    public int healthRecovery;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

  

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") &&
             other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            PlayerHealth.health += healthRecovery;
            SoundManager.PlayerRecovery();
            HealthBar.HealthCurrent = PlayerHealth.health;
            if (HealthBar.HealthCurrent > 20)
            {
                HealthBar.HealthCurrent = 20;
            }
            Destroy(gameObject);

        }
    }
}
