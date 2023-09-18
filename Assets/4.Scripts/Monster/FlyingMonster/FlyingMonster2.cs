using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class FlyingMonster2 : FlyingMonsterBase
{
    [SerializeField] private GameObject projectile;
    public int nextMove;
    private Animator anim;
    private float distance;
    private bool isMovingIdle;
    private GameObject[] objectPool = new GameObject[10];
    private void Awake()
    {
        FlyingInit();
        anim = GetComponent<Animator>();
        StartCoroutine("MonsterMove");
        StartCoroutine("CheckMonsterState");
        StartCoroutine("MonsterAction");

        for(int i = 0; i < objectPool.Length; i++)
        {
            objectPool[i] = Instantiate(projectile);
            objectPool[i].SetActive(false);
        } 
    }

    private void FixedUpdate()
    {
        distance = rigid.Distance(GameManager.Instance.player.GetComponent<Collider2D>()).distance;
        
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
        
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

    public override IEnumerator CheckMonsterState()
    {
        while (state != State.DIE)
        {
            yield return new WaitForSeconds(0.3f);
            if(distance >= 7f)
            {
                state = State.IDLE;
            }

            if(distance <= 7f)
            {
                state = State.ATTACK;
                anim.SetTrigger("ATTACK");
            }

            if (hp <= 0)
            {
                state = State.DIE;
                nextMove = 0;
                anim.SetTrigger("DIE");
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
                    break;
                case State.ATTACK:
                    anim.SetTrigger("ATTACK");
                    break;
                case State.DIE:
                    anim.SetTrigger("DIE");
                    break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 7f);

    }

    public void ShootProjectile()
    {
        GameObject shootObject = null;
        for(int i = 0; i < objectPool.Length; i++)
        {
            if (objectPool[i].activeSelf)
            {
                continue;
            }
            shootObject = objectPool[i];
            break;
        }
        shootObject.transform.position = transform.position;
        shootObject.GetComponent<ShootingProjectile>().start = transform.position;
        shootObject.GetComponent<ShootingProjectile>().target = GameManager.Instance.player.transform.position;
        Vector3 dir = GameManager.Instance.player.transform.localPosition - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        shootObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        shootObject.SetActive(true);
        shootObject.GetComponent<ShootingProjectile>().ActiveFalseInvoke(3f);
    }

}
