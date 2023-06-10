using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMonster : MonoBehaviour
{
    protected Rigidbody2D rigid;
    protected SpriteRenderer spriteRenderer;
    [SerializeField] protected int hp;
    [SerializeField] protected float moveSpeed;
    public State state = State.IDLE;

    public enum State{
        IDLE,
        TRACE,
        DIE
    }

    public void hit(int damage)
    {
        hp -= damage;
        if(hp <= damage)
        {
            state = State.DIE;
        }
        else
        {
            GetComponent<Animator>().SetTrigger("HIT");
        }
    }

    abstract public IEnumerator CheckMonsterState();

    abstract public IEnumerator MonsterAction();
}
