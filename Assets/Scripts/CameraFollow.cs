using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float smooth;

    public Vector2 minPos;
    public Vector2 maxPos;
    void Start()
    {
        
    }

    // Update is called once per frame
   
    void LateUpdate()
    {
       if(target != null)
        {
            if(transform.position != target.position)
            {
                Vector3 targetPos = target.position;
                targetPos.x = Mathf.Clamp(targetPos.x, minPos.x, maxPos.x);
               targetPos.y = Mathf.Clamp(targetPos.y, minPos.y, maxPos.y);
                transform.position = Vector3.Lerp(transform.position, targetPos, smooth);//œﬂ–‘≤Â÷µ
            }
        } 
    }

    
}
