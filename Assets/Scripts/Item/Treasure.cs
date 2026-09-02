using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Treasure : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject coin;
    public GameObject Gunpick;
    private bool canOpen;
    private bool isOpened;
    private Animator anim;
    private Vector3 pos1;
    private Vector3 pos2;
    private Vector3 pos3;
    void Start()
    {
        isOpened = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
            if(canOpen && !isOpened)
            {
                anim.SetTrigger("Opening");
                isOpened = true;
                pos1 = transform.position;
                pos2 = new Vector3(pos1.x-0.5f, pos1.y, pos1.z);
                pos3 = new Vector3(pos1.x + 0.5f, pos1.y, pos1.z);
                Instantiate(coin, pos1, Quaternion.identity);
                Instantiate(coin, pos2, Quaternion.identity);
                Instantiate(coin, pos3, Quaternion.identity);
                Destroy(gameObject, 1.0f);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {

            canOpen = true;
            
            Debug.Log(canOpen);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {

            canOpen = false;
        }
    }
}
