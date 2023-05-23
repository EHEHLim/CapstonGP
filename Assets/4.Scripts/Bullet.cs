using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LayerMask isLayer;

    public float distance;
    public float speed;
    void Start()
    {
        Invoke("DestroyBullet", 2);
    }


    void Update()
    {
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * -1, distance, isLayer);
        if (raycast.collider != null)
        {
            if(raycast.collider.tag == "Player")
            {
                Debug.Log("당했다!");
            }
            DestroyBullet();
        }
        transform.Translate(transform.right * -1f * speed * Time.deltaTime);

    }
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
