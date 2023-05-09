using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtl : MonoBehaviour
{
    Rigidbody2D rigid2D;
    SpriteRenderer spriteRenderer;
    Animator anim;
    Transform tr;
    Collider2D coll;

    public float moveSpeed;
    public float jumpPower;
    bool isJumping = false;
    bool canDoubleJump = true;
    public Transform pos;
    public Vector2 boxSize;

    private int direction = -1;
    private bool isDash = false;
    private bool canDash = true;
    private float dashingTime = 0.15f;
    private float dashingPower = 10f;
    private float dashingCoolDown = 1f;

    private float horizontal;

    void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        tr = GetComponent<Transform>();
        coll = GetComponent<Collider2D>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (GameManager.Instance.isStoryProcessing)
        {
            return;
        }
        if(GameManager.Instance.isSceneChanging)
        {
            return;
        }
        if (Input.GetKey(KeyCode.Z))
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
            foreach(Collider2D collider in collider2Ds)
            {
                if(collider.gameObject.tag == "Monster")
                {
                    collider.GetComponent<MonsterCtl>().Damage(1);
                }
            }
            anim.SetTrigger("Attack");
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(isJumping == false && !anim.GetBool("isJump"))
            {
                isJumping = true;
                rigid2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                anim.SetBool("isJump", true);
                canDoubleJump = true;
            }
            else
            {
                if (canDoubleJump)
                {
                    canDoubleJump = false;
                    rigid2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

                }
            }
            
        }

        if (Input.GetAxisRaw("Horizontal") != 0)
            anim.SetBool("isRun", true);
        else
            anim.SetBool("isRun", false);


        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }


        horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal < 0)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }

        if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (canDash)
            { 
                StartCoroutine(Dash());
            }
        }

    }


    void FixedUpdate()
    {
        if (GameManager.Instance.isSceneChanging)
        {
            return;
        }
        if (isDash)
        {
            return;
        }
        rigid2D.velocity = new Vector2(horizontal * moveSpeed, rigid2D.velocity.y);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        
            Gizmos.DrawWireCube(pos.position, boxSize);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.CompareTag("GROUND"))
        {
            isJumping = false;
            canDoubleJump = false;
            anim.SetBool("isJump", false);
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDash = true;
        float originalGravity = rigid2D.gravityScale;
        rigid2D.gravityScale = 0f;
        rigid2D.velocity = new Vector2(direction * tr.transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rigid2D.gravityScale = originalGravity;
        isDash = false;
        yield return new WaitForSeconds(dashingCoolDown);
        canDash = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Item"))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
