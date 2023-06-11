using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class FlyingMonster3 : FlyingMonsterBase
{
    private Animator anim;
    private float traceDistance;
    private float attackDistance;
    public int nextMove;
    private bool isMovingIdle;
    private float distance;
    public Vector3 pos;
    public Vector2 cubeSize;
    private Transform target;
    public int attackDamage;
    private Vector3 originPos;
    private float deadHit = 10;

    private void Awake()
    {
        FlyingInit();
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

        
        if (state == State.IDLE)
        {
            rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
        }
        else if (state == State.TRACE)
        {
            TraceTarget(target.position);
            if(rigid.velocity.x > 0)
            {
                spriteRenderer.flipX = true;
                
            }
            else if(rigid.velocity.x < 0)
            {
                spriteRenderer.flipX = false;
                
            }
        }

    }

    public override IEnumerator CheckMonsterState()
    {
        while (state != State.DIE)
        {
            yield return new WaitForSeconds(0.3f);
            if (distance >= 7f)
            {
                state = State.IDLE;
            }

            if (distance <= 7f && distance > 0.5f)
            {
                state = State.TRACE;
            }

            if (distance <= 0.5f)
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
                    rigid.velocity = new Vector2(rigid.velocity.x, 0);
                    if (isMovingIdle == false)
                    {
                        StartCoroutine("MonsterMove");
                    }
                    break;
                case State.TRACE:
                    break;
                case State.ATTACK:
                    anim.SetTrigger("ATTACK");
                    rigid.velocity = new Vector2(0, 0);
                    break;
                case State.DIE:
                    rigid.gravityScale = 1f;
                    int direction = 0;
                    if (spriteRenderer.flipX)
                    {
                        direction = -1;
                    }
                    else
                    {
                        direction = 1;
                    }
                    rigid.AddForce(new Vector2(1 * direction * deadHit, 1f), ForceMode2D.Impulse);
                    anim.SetTrigger("DIE");
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + pos, cubeSize);
    }

    private void TraceTarget(Vector3 target)
    {
        rigid.velocity = new Vector2(target.x + 1 - transform.position.x, target.y - transform.position.y);
    }

    public void Attack()
    {
        
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos + transform.position, cubeSize, 0);
        Debug.Log(collider2Ds.Length.ToString());
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.gameObject.tag == "Player")
            {
                collider.GetComponent<PlayerCtl>().Hit(attackDamage);
            }
        }
    }
}