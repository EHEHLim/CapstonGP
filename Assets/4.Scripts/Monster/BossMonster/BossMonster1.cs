using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossMonster1 : BaseMonster
{
    private Animator anim;
    private Transform target;
    private State curState;
    private int nextMove;
    private bool isAttcking;
    private bool isRoar = false;

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
        curState = State.ATTACK;
    }

    private void FixedUpdate()
    {
        if (isAttcking)
        {
            return;
        }

        if (state == State.IDLE)
        {
            rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
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
                            anim.SetTrigger("ROLLATTACK");
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
        if (roar < 3)
            isRoar = true;
        else
            isRoar = false;
        nextMove = Random.Range(-1, 2);
        switch (nextMove)
        {
            case -1:
                spriteRenderer.flipX = false;

                break;
            case 0:
                break;
            case 1:
                spriteRenderer.flipX = true;

                break;
        }
        yield return new WaitForSeconds(1f);
        if(state == State.IDLE)
        {
            StartCoroutine(MonsterMove());
        }
    }

    public void RollAttack()
    {
        
    }

    public void TakeOff()
    {

    }

    public void SpikeAttack()
    {

    }
}
