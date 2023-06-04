using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCtl : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigid;
    public int nextMove;
    SpriteRenderer spriteRenderer;


    [SerializeField] private int hp;
    [SerializeField] private float moveSpeed;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Invoke("Think",1);

    }
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
    }
    void Think()
    {
        nextMove = Random.Range(-1, 2);
        Invoke("Think", 1);
        if(nextMove != 0)
        {
            spriteRenderer.flipX = nextMove == 1;
        }
    }

   
    public void Damage(int damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            anim.SetTrigger("hit");
            Invoke("Destroy", 3f);
        }
        if (hp == 0)
        {
            anim.SetTrigger("dead");
        }
    }
}
