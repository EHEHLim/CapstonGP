using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Anim : MonoBehaviour
{
    Animator anim;
    SpriteRenderer spriteRenderer;
    PlayerCtl playerctl;
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
        if (Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetTrigger("Attack");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            anim.SetTrigger("Attack4");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playerctl.isGrounded == true || playerctl.canDoubleJump)
            {
                anim.SetTrigger("Jump");
            }
        }

        // Move animation
        if (Input.GetAxisRaw("Horizontal") != 0)
            anim.SetBool("isRun", true);
        else
            anim.SetBool("isRun", false);

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
