using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossMonster1 : BaseMonster
{
    private Animator anim;
    private Transform target;

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

                    break;
                case State.TRACE:

                    break;
                case State.DIE:

                    break;
                case State.ATTACK:

                    break;
            }
        }
    }
}
