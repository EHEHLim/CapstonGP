using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class WalkingMonster4 : WalkingMonsterBase
{
    public int nextMove;
    private Animator anim;
    private bool isMovingIdle;
    private float distance;
    public Vector3 pos;
    private Vector3 originPos;
    public Vector2 cubeSize;
    private Transform target;
    private bool isAttcking;
    [SerializeField] private int attackDamage = 30;

    [SerializeField] private float traceSpeed;


    private void Awake()
    {
        WalkingInit();
        anim = GetComponent<Animator>();
        StartCoroutine("MonsterMove");
        StartCoroutine("CheckMonsterState");
        StartCoroutine("MonsterAction");
        target = GameManager.Instance.player.transform;

        originPos = pos;
    }


    private void FixedUpdate()
    {
        distance = rigid.Distance(GameManager.Instance.player.GetComponent<Collider2D>()).distance;

        if (spriteRenderer.flipX)
        {
            pos = new Vector3(-originPos.x, originPos.y);
        }
        else
        {
            pos = originPos;
        }

        if (isAttcking)
        {
            return;
        }

        if (state == State.IDLE)
        {
            rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
        }
        else if (state == State.TRACE)
        {
            TraceTarget(target.position);
            if (rigid.velocity.x > 0)
            {
                spriteRenderer.flipX = true;

            }
            else if (rigid.velocity.x < 0)
            {
                spriteRenderer.flipX = false;

            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + pos, cubeSize);
    }

    public override IEnumerator CheckMonsterState()
    {
        while (state != State.DIE)
        {
            yield return new WaitForSeconds(0.3f);

            if(distance > 7f)
            {
                state = State.IDLE;
            }
            else if(distance <= 7f && distance > 1.5f)
            {
                state = State.TRACE;
            }
            else if(distance <= 1.5f)
            {
                state = State.ATTACK;
            }


            if (hp <= 0)
            {
                state = State.DIE;
            }
        }
    }

    public override IEnumerator MonsterAction()
    {
        while (state != State.DIE)
        {
            yield return new WaitForSeconds(0.3f);

            switch (state)
            {
                case State.IDLE:

                    if (isMovingIdle == false)
                    {
                        StartCoroutine("MonsterMove");
                    }

                    if (rigid.velocity.x == 0)
                    {
                        anim.SetBool("Running", false);
                        anim.SetBool("Walking", false);
                    }
                    else
                    {
                        anim.SetBool("Running", false);
                        anim.SetBool("Walking", true);
                    }

                    break;
                case State.TRACE:

                    anim.SetBool("Running", true);
                    anim.SetBool("Walking", false);

                    break;
                case State.DIE:
                    anim.SetTrigger("DIE");
                    anim.SetBool("Running", false);
                    anim.SetBool("Walking", true);
                    break;
                case State.ATTACK:
                    anim.SetTrigger("ATTACK");
                    anim.SetBool("Running", false);
                    anim.SetBool("Walking", false);
                    break;
            }
        }
    }

    public IEnumerator MonsterMove()
    {
        isMovingIdle = true;
        nextMove = Random.Range(-1, 2);
        float waitingTime = 2f;
        switch (nextMove)
        {
            case -1:
                spriteRenderer.flipX = false;

                break;
            case 0:
                waitingTime = 0.5f;
                break;
            case 1:
                spriteRenderer.flipX = true;

                break;
        }

        if (state == State.IDLE)
        {
            yield return new WaitForSeconds(waitingTime);
            StartCoroutine("MonsterMove");
        }
        else
        {
            isMovingIdle = false;
            nextMove = 0;
        }
    }

    private void TraceTarget(Vector3 target)
    {
        if(target.x - transform.position.x < -1)
        {
            rigid.velocity = new Vector2(-1 * traceSpeed, rigid.velocity.y);
        }
        else if(target.x - transform.position.x > 1)
        {
            rigid.velocity = new Vector2(1 * traceSpeed, rigid.velocity.y);
        }
        else
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }
    }


    public void Monster4AttackStart()
    {
        isAttcking = true;
    }

    public void Monster4AttackEnd()
    {
        isAttcking = false;
    }

    public void Monster4Attack()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos + transform.position, cubeSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.gameObject.tag == "Player")
            {
                collider.GetComponent<PlayerCtl>().Hit(attackDamage);
            }
        }
    }
}
