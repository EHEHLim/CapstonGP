using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Anim : MonoBehaviour
{
    Animator anim;
    SpriteRenderer spriteRenderer;
    PlayerCtl playerctl;

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
        if (SceneManager.GetActiveScene().buildIndex == 0)
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

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (playerctl.IsDash)
            {
                anim.SetTrigger("isDash");
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (playerctl.isGrounded && !playerctl.IsDash)
            {
                anim.SetBool("isAttacking", true);
            }
        }


        // Move animation
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }

        if (Input.GetButton("Horizontal"))
        {

            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
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
