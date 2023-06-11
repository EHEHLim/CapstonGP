using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingProjectile : MonoBehaviour
{
    private Rigidbody2D rigid;
    public float moveSpeed;
    public Vector3 target;
    public int damage;
    public Vector3 start;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //rigid.velocity = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        transform.position += new Vector3(target.x - start.x, target.y - start.y) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameManager.Instance.player.GetComponent<PlayerCtl>().Hit(damage);
        }
        CancelInvoke("ActiveFalse");
        gameObject.SetActive(false);
    }

    public void ActiveFalseInvoke(float time)
    {
        Invoke("ActiveFalse", time);
    }

    private void ActiveFalse()
    {
        gameObject.SetActive(false);
    }
}
