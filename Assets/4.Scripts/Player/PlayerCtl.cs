using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PlayerCtl : MonoBehaviour
{
    //Component Variables
    Rigidbody2D rigid2D;    
    Animator anim;
    Transform tr;
    Collider2D coll;
    public RawImage panel;
    public CameraShakes vmCam;

    //Move Variables
    public float moveSpeed;             
    public float jumpPower;
    public bool isGrounded = true;
    public bool canDoubleJump = false;
    public GameObject jumpDust;
    public GameObject doubleJumpDust;
    private float horizontal;

    //Dash Variables
    private int direction = -1;
    private bool isDash = false;
    public bool canDash = true;
    private float dashingTime = 0.15f;
    private float dashingPower = 20f;
    private float dashingCoolDown = 1f;
    public Transform[] dashdusts;

    //Attack Variables
    private int attackDamage = 30;
    private int COMBO_ATTACK = 0;
    private int BOW_ATTACK = 1;
    public int attackValue = 0;
    [SerializeField] private Transform pos;
    [SerializeField] private Vector2[] comboAttackBoxSizes;
    [SerializeField] private Vector2[] poses;
    public bool attacking = false;
    [SerializeField]private int comboAttackIndex = 0;

    //HP Variables
    [SerializeField] private int hp;
    private int currHp;
    private bool isDead;


    //===============================================Run Field===============================================
    void Awake()//init Variables
    {
        Debug.Log("awake");
        rigid2D = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        coll = GetComponent<Collider2D>();
        gameObject.SetActive(false);
        anim = GetComponent<Animator>();
        currHp = hp;
    }

    void Update()
    {
        //Event Stop
        if (GameManager.Instance.isStoryProcessing)
        {
            return;
        }
        if(GameManager.Instance.isSceneChanging)
        {
            return;
        }
        if (isDead)
        {
            return;
        }

        //Get move key
        horizontal = Input.GetAxisRaw("Horizontal");

        //Sprite Flip
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

        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded == true || canDoubleJump)
            {
                if (isGrounded)
                {
                    jumpDust.SetActive(true);
                    jumpDust.GetComponent<FxControl>().JumpDustStart();
                }
                else if (canDoubleJump)
                {
                    doubleJumpDust.SetActive(true);
                    doubleJumpDust.GetComponent<FxControl>().JumpDustStart();
                }
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

        //Dash
        if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (canDash)
            { 
                StartCoroutine(Dash());
                for (int i = 0; i < dashdusts.Length; i++)
                {
                    if (!dashdusts[i].gameObject.activeSelf)
                    {
                        dashdusts[i].gameObject.SetActive(true);
                        dashdusts[i].GetComponent<FxControl>().dustStart();
                        break;
                    }
                }
            }
        }

        //Weapon Change
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            switch (attackValue)
            {
                case 0:
                    attackValue = BOW_ATTACK;
                    break;
                case 1:
                    attackValue = COMBO_ATTACK;
                    break;
            }
        }

        //Bow shot
        if(attackValue == BOW_ATTACK)
        {
            if (Input.GetKeyUp(KeyCode.Z))
            {
                ArrowShoot();
            }
        }
    }


    void FixedUpdate()
    {
        //EventStop
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
        if (isDead)
        {
            return;
        }
        
        
        //Movement
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
    //Attack anim Field
    public void attackAnimStart()
    {
        GetComponent<Animator>().SetBool("isAttacking", false);
        attacking = true;
    }

    public void attackAnimEnd()
    {
        attacking = false;
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
                collider.GetComponent<BaseMonster>().hit(attackDamage);
            }
        }
    }

    void ArrowShoot()
    {

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
        if (col.collider.CompareTag("GROUND") || col.collider.CompareTag("TGROUND"))
        {
            canDoubleJump = true;
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("GROUND") || collision.collider.CompareTag("TGROUND"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            collision.gameObject.SetActive(false);
        }
    }

    //===============================================Hit Field===============================================
    public void Hit(int damage)
    {
        
        if (isDash)
        {

        }
        else
        {
            vmCam.Attacked();
            currHp -= damage;
            Debug.Log("hurts");
        }

        if(hp <= 0 && isDead == false)
        {
            isDead = true;
            anim.SetTrigger("DIE");
            
            StartCoroutine(PlayerDieSceneChange());
        }
    }

    public void DieAnimEvent()
    {
        StartCoroutine(PlayerDieSceneChange());
    }

    IEnumerator PlayerDieSceneChange()
    {
        panel.gameObject.SetActive(true);
        panel.color = new Color(0, 0, 0, 0f);
        GameManager.Instance.isSceneChanging = true;
        while (panel.color.a < 1f)
        {
            panel.color += new Color(0, 0, 0, 0.03f);
            yield return new WaitForSeconds(0.01f);
        }
        SceneManager.LoadScene("Last");

        isDead = false;
        currHp = hp;

        panel.color = new Color(0, 0, 0, 1f);
        while (panel.color.a > 0f)
        {
            panel.color -= new Color(0, 0, 0, 0.03f);
            yield return new WaitForSeconds(0.01f);
        }
        GameManager.Instance.isSceneChanging = false;
        panel.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
