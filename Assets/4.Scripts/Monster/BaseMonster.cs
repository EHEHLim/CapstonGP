using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMonster : MonoBehaviour
{
    protected Rigidbody2D rigid;
    protected SpriteRenderer spriteRenderer;
    [SerializeField] public float hp;
    [SerializeField] protected float moveSpeed;
    protected AudioSource audioSource;
    private bool isAlreadyDead = false;

    public State state = State.IDLE;

    public enum State{
        IDLE,
        TRACE,
        DIE,
        ATTACK
    }
    public void hit(float damage)
    {
        hp -= damage;

        StartCoroutine(GameManager.Instance.ShowDamageEffect(damage, this.transform));
        if (hp > 0)
        {
            GetComponent<Animator>().SetTrigger("HIT");
        }
        else
        {
            if (!isAlreadyDead)
            {
                audioSource.PlayOneShot(GameManager.Instance.Sound.monsterDie);
                GameManager.Instance.KillingPoint++;
                GameManager.Instance.RemainMonster--;
                isAlreadyDead = true;
            }
        }
    }


    abstract public IEnumerator CheckMonsterState();

    abstract public IEnumerator MonsterAction();

    public void DieAnimEvent()
    {
        Destroy(gameObject);
    }
}
