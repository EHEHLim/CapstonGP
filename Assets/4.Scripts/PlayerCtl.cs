using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtl : MonoBehaviour
{
    //Component Variables
    Rigidbody2D rigid2D;    
    Animator anim;
    Transform tr;
    Collider2D coll;

    //Move Variables
    public float moveSpeed;             
    public float jumpPower;
    public bool isGrounded = true;
    public bool canDoubleJump = false;
    private float horizontal;

    //Dash Variables
    private int direction = -1;
    private bool isDash = false;
    public bool canDash = true;
    private float dashingTime = 0.15f;
    private float dashingPower = 20f;
    private float dashingCoolDown = 1f;

    //Attack Variables
    [SerializeField] private Transform pos;
    [SerializeField]private Vector2[] comboAttackBoxSizes;
    [SerializeField] private Vector2[] poses;
    public bool attacking = false;
    [SerializeField]private int comboAttackIndex = 0;


    //===============================================Run Field===============================================
    void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        coll = GetComponent<Collider2D>();
        gameObject.SetActive(false);
        anim = GetComponent<Animator>();
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(isGrounded == true || canDoubleJump)
            {
                rigid2D.velocity = Vector2.up * jumpPower;
                
                if (isGrounded)
                {
                    isGrounded = false;
                }
                else
                {
                    canDoubleJump = false;
                    anim.SetTrigger("doubleJump");
                }
            }
            
        }

        

        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetButton("Horizontal"))
        {
            if (horizontal < 0)
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }
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
        if (attacking)
        {
            return;
        }
        transform.position += new Vector3(horizontal * moveSpeed * Time.deltaTime, 0);
        
    }

    //===============================================Dash Field===============================================
    private IEnumerator Dash()
    {
        canDash = false;
        isDash = true;
        float originalGravity = rigid2D.gravityScale;
        rigid2D.gravityScale = 0f;
        rigid2D.velocity = new Vector2(direction * tr.transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rigid2D.velocity = Vector2.zero;
        rigid2D.gravityScale = originalGravity;
        isDash = false;
        yield return new WaitForSeconds(dashingCoolDown);
        canDash = true;
    }

    public bool IsDash
    {
        get
        {
            return isDash;
        }
    }

    //===============================================Attack Field===============================================
    public void attackAnimStart()
    {
        GetComponent<Animator>().SetBool("isAttacking", false);
        attacking = true;
    }

    public void attackAnimEnd()
    {
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Item"))
        {
            collision.gameObject.SetActive(false);
        }
    }

    public void DrawComboAttack(int idx)
    {
        comboAttackIndex = idx;
        pos.localPosition = poses[comboAttackIndex];
        pos.localPosition = new Vector3(pos.localPosition.x * direction,pos.localPosition.y,0);
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, comboAttackBoxSizes[comboAttackIndex], 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.gameObject.tag == "Monster")
            {
                collider.GetComponent<MonsterCtl>().Damage(1);
            }
        }
    }

    void OnDrawGizmos()
    {
        pos.localPosition = poses[comboAttackIndex];
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, comboAttackBoxSizes[comboAttackIndex]);
    }

    //===============================================Contact Field===============================================
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("GROUND"))
        {
            canDoubleJump = true;
            isGrounded = true;
            Debug.Log("ground on");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("GROUND"))
        {
            Debug.Log("ground out");
            isGrounded = false;

        }
    }
}
