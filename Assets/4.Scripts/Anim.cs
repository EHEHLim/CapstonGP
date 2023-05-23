using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Anim : MonoBehaviour
{
    Animator anim;
    SpriteRenderer spriteRenderer;
    PlayerCtl playerctl;

    public int noOfClicks = 0;
    float lastClickTime = 0;
    public float maxComboDelay = 0;

    void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerctl = GetComponent<PlayerCtl>();
        anim.SetBool("isJump", false);
        anim.SetBool("isRun", false);
    }
    void Start()
    {
        
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            return;
        }
        if (GameManager.Instance.isStoryProcessing)
        {
            return;
        }
        if (GameManager.Instance.isSceneChanging)
        {
            return;
        }

        // Attack animation
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    anim.SetTrigger("Attack");
        //}
        if (Input.GetKeyDown(KeyCode.C))
        {
            anim.SetTrigger("Attack4");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.LeftShift))
            {
                anim.SetTrigger("isDash");
            }
            //if (playerctl.isGrounded == true || playerctl.canDoubleJump)
            //{
                //anim.SetTrigger("Jump");

            //}
        }

        // Move animation
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.LeftShift))
            {
                anim.SetTrigger("isDash");
            }
            anim.SetBool("isRun", true);
        }
        else
            anim.SetBool("isRun", false);
            if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.LeftShift))
            {
                anim.SetTrigger("isDash");
            }

        if (Input.GetButton("Horizontal"))
        {
            
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }


        //ComboAttck
        if(Time.time - lastClickTime > maxComboDelay)
        {
            noOfClicks = 0;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            lastClickTime = Time.time;
            noOfClicks++;
            if (noOfClicks == 1)
            {
                anim.SetBool("ComboAttack1",true);
            }
            noOfClicks = Mathf.Clamp(noOfClicks,0,4);
        }
    }

    //z키를 몇번이상눌렀는지
    public void return1()
    {
        if (noOfClicks >= 2)
        {
            anim.SetBool("ComboAttack2",true);
           
        }
        else
        {
            anim.SetBool("ComboAttack1", false);
            noOfClicks = 0;
        }
    }
    public void return2()
    {
        if (noOfClicks >= 3)
        {
            anim.SetBool("ComboAttack3",true);
        }
        else
        {
            anim.SetBool("ComboAttack2", false);
            noOfClicks = 0;
        }
    }
    public void return3()
    {
        if (noOfClicks >= 4)
        {
            anim.SetBool("ComboAttack4",true);
        }
        else
        {
            anim.SetBool("ComboAttack3", false);
            noOfClicks = 0;
        }
    }
    public void return4()
    {
        anim.SetBool("ComboAttack1", false);
        anim.SetBool("ComboAttack2", false);
        anim.SetBool("ComboAttack3", false);
        anim.SetBool("ComboAttack4", false);
        noOfClicks = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("GROUND"))
        {
            anim.SetBool("isJump", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("GROUND"))
        {
            anim.SetBool("isJump", true);
        }
    }
}
