using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtl : MonoBehaviour
{
    public LayerMask isLayer;
    public GameObject bullet;
    public Transform pos;

    public float distance;
    public float atkDistance;
    public float coolTime;
    private float currentTime;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
     
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * -1, distance, isLayer);
        if(raycast.collider != null)
        {
            currentTime = 0;
            if (Vector2.Distance(transform.position, raycast.collider.transform.position) < atkDistance)
            {

                if (currentTime <= 0)
                {
                    GameObject bulletcopy = Instantiate(bullet, pos.position, transform.rotation);
                    currentTime = coolTime;
                }

            }
            currentTime -= Time.deltaTime;
        }


    }
}
