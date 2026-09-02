using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam;
    public GameObject bullet;
    public Transform muzzle;
    public GameObject gunpick;
   

    private Vector3 mousePos;
    private Vector2 gunDirection;
    private bool hold;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        mousePos = cam.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

        gunDirection = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(gunDirection.y, gunDirection.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, 0, angle);

        if (Input.GetMouseButtonDown(0)&&hold)
        {
            SoundManager.PlayerShot();
            
            Instantiate(bullet, muzzle.position, Quaternion.Euler(transform.eulerAngles));
        }
        if (Input.GetKeyDown(KeyCode.G)&& Buymenu.hasGun)
        {
            hold = true;
            gunpick.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            hold = false;
            gunpick.SetActive(false);
        }

    }
    

}
