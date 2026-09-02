using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage;
    public float time;
    public float starttime;

    private Animator anim;
    private PolygonCollider2D poly;
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        poly = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();

    }

    void Attack()
    {
        if (Input.GetButtonDown("Attack"))
        {
            
            anim.SetTrigger("Attack");
            SoundManager.PlayerAttack();
            StartCoroutine(startHitBox());

        }

    }
    IEnumerator startHitBox()
    {
        yield return new WaitForSeconds(starttime);
        poly.enabled = true;
        StartCoroutine(disableHitBox());
    }
    IEnumerator disableHitBox()
    {
        yield return new WaitForSeconds(time);
        poly.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

}
