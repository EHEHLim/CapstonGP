using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BossMonster1 : BaseMonster
{
    private Animator anim;
    private Transform target;
    private State curState;
    private int nextMove;
    private bool isAttcking;
    private bool isRoar = false;
    private bool rollingStart = false;

    [SerializeField] private float rollingSpeed;
    [SerializeField] private float rollingDamage;
    [SerializeField] private float jumpDamage;
    [SerializeField] private float spikeDamage;
    [SerializeField] private GameObject spike;

    private void Awake()
    {
        state = State.IDLE;
        rigid = GetComponent<Rigidbody2D>();
        rigid.gravityScale = 4;
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        StartCoroutine("MonsterMove");
        StartCoroutine("CheckMonsterState");
        StartCoroutine("MonsterAction");
        target = GameManager.Instance.player.transform;
    }

    private void FixedUpdate()
    {
        if (isAttcking)
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(transform.position + new Vector3(0.5f * nextMove, -0.5f), new Vector2(6, 6), 0);
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider.gameObject.tag == "Player")
                {
                    collider.gameObject.GetComponent<PlayerCtl>().Hit((int)rollingDamage);
                }
            }
        }
        if (state == State.IDLE)
        {
            rigid.velocity = new Vector2(nextMove * moveSpeed, rigid.velocity.y);
        }
    }
    public override IEnumerator CheckMonsterState()
    {
        while (state != State.DIE)
        {
            yield return new WaitForSeconds(0.3f);

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
                    if (rigid.velocity.x == 0)
                    {
                        anim.SetBool("isWalking", false);
                        if (isRoar)
                            anim.SetTrigger("ROAR");
                    }
                    else
                    {
                        anim.SetBool("isWalking", true);
                    }
                    int rdState = Random.Range(0, 2);
                    if (rdState == 0)
                    {
                        state = State.IDLE;
                    }
                    else
                    {
                        state = State.ATTACK;
                    }
                    break;
                case State.TRACE:

                    break;
                case State.DIE:
                    anim.SetBool("isDead", true);
                    break;
                case State.ATTACK:
                    int randnum = (int)Random.Range(0, 3);
                    switch (randnum)
                    {
                        case 0:
                            StartCoroutine("RollAttack");
                            break;
                        case 1:
                            anim.SetTrigger("TAKEOFF");
                            break;
                        case 2:
                            anim.SetTrigger("SPIKEATTACK");
                            break;
                        default:
                            break;
                    }
                    state = State.IDLE;
                    break;
            }
        }
    }

    public IEnumerator MonsterMove()
    {
        int roar = Random.Range(0, 10);
        nextMove = Random.Range(-1, 2);
        if (roar < 3)
        {
            isRoar = true;
            nextMove = 0;
        }
        else
            isRoar = false;
        switch (nextMove)
        {
            case -1:
                spriteRenderer.flipX = false;

                break;
            case 0:
                nextMove = 0;
                break;
            case 1:
                spriteRenderer.flipX = true;

                break;
        }
        yield return new WaitForSeconds(1f);
        if (state == State.IDLE)
        {
            StartCoroutine(MonsterMove());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(0.5f,-0.5f), new Vector2(6, 6));
    }

    public IEnumerator RollAttack()
    {
        anim.SetTrigger("ROLLATTACK");
        anim.SetBool("isRollAttacking", true);
        yield return new WaitUntil(() => rollingStart == true);
        isAttcking = true;
        nextMove = GameManager.Instance.player.transform.position.x > transform.position.x ? 1 : -1;
        float originSpeed = moveSpeed;
        moveSpeed = rollingSpeed;
        yield return new WaitForSeconds(1f);
        isAttcking = false;
        anim.SetBool("isRollAttacking", false);
        moveSpeed = originSpeed;
        nextMove = 0;
    }

    public IEnumerator TakeOff()
    {
        yield return null;
    }

    public IEnumerator SpikeAttack()
    {
        yield return null;
    }
}
