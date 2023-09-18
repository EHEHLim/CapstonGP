using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingMonster1 : WalkingMonsterBase
{
    public int nextMove;
    private Animator anim;
    private void Awake()
    {
        WalkingInit();
        anim = GetComponent<Animator>();
        StartCoroutine("MonsterMove");
        StartCoroutine("CheckMonsterState");
        StartCoroutine("MonsterAction");
    }

    private void FixedUpdate()
    {
        if (state == State.DIE)
            return;
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
    }

    public override IEnumerator CheckMonsterState()
    {
        while(state != State.DIE)
        {
            yield return new WaitForSeconds(0.3f);

            if(hp <= 0)
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
                    break;
                case State.TRACE:
                    break;
                case State.DIE:
                    anim.SetTrigger("DIE");
                    break;
            }
        }
    }

    public IEnumerator MonsterMove()
    {
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
        yield return new WaitForSeconds(waitingTime);
        if (state == State.IDLE)
        {
            StartCoroutine("MonsterMove");
        }
    }

    
}
