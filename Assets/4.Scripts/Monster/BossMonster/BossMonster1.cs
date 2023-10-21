using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossMonster1 : BaseMonster
{
    private Animator anim;
    private Transform target;
    private int nextMove;
    private bool isAttcking;
    private bool isRoar = false;

    private bool rollingStart = false;
    private bool jumpingStart = false;
    private bool spikeStart = false;
    private bool isStateEnd = true;
    private bool isStateChecked = true;

    [SerializeField] private float rollingSpeed;
    [SerializeField] private float rollingDamage;
    [SerializeField] private float jumpDamage;
    [SerializeField] public float spikeDamage;
    [SerializeField] private GameObject spike;
    [SerializeField] public Vector3[] spikePositions;
    private GameObject[] spikes;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        state = State.IDLE;
        rigid = GetComponent<Rigidbody2D>();
        rigid.gravityScale = 4;
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        StartCoroutine("CheckMonsterState");
        StartCoroutine("MonsterAction");
        target = GameManager.Instance.player.transform;
        spikes = new GameObject[7];
        for (int i = 0; i < spikes.Length; i++)
        {
            spikes[i] = Instantiate(spike);
            spikes[i].transform.parent = this.transform;
            spikes[i].GetComponent<Boss1Spike>().initSpike();
            spikes[i].transform.position = spikePositions[i];
            spikes[i].transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(spikePositions[i].y, spikePositions[i].x)*Mathf.Rad2Deg +180);
        }
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
        if (state == State.IDLE || (state == State.ATTACK && rollingStart))
        {
            rigid.velocity = new Vector2(nextMove * moveSpeed, rigid.velocity.y);
        }

        if (nextMove > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if(nextMove < 0)
        {
            spriteRenderer.flipX = false;
        }

        if (nextMove != 0)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {

            anim.SetBool("isWalking", false);
        }
    }
    public override IEnumerator CheckMonsterState()
    {
        while (state != State.DIE)
        {
            yield return new WaitUntil(() => isStateEnd == true);
            isStateEnd = false;
            if (hp <= 0)
            {
                state = State.DIE;
            }

            if(state == State.IDLE)
            {
                state = State.ATTACK;
            }
            else if(state == State.ATTACK)
            {
                state = State.IDLE;
            }
            isStateChecked = true;
        }
    }

    public override IEnumerator MonsterAction()
    {
        while (state != State.DIE)
        {
            yield return new WaitUntil(() => isStateChecked == true);
            isStateChecked = false;
            yield return new WaitForSeconds(0.3f);

            switch (state)
            {
                case State.IDLE:
                    StartCoroutine("MonsterMove");
                    if (rigid.velocity.x == 0)
                    {
                        if (isRoar)
                        {
                            anim.SetTrigger("ROAR");
                            audioSource.PlayOneShot(GameManager.Instance.Sound.bossRoar);
                        }
                    }
                    else
                    {
                        anim.SetBool("isWalking", true);
                    }
                    break;
                case State.DIE:
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isDead", true);
                    break;
                case State.ATTACK:
                    anim.SetBool("isWalking", false);
                    int randnum = (int)Random.Range(0, 3);
                    switch (randnum)
                    {
                        case 0:
                            StartCoroutine(RollAttack());
                            break;
                        case 1:
                            StartCoroutine(TakeOff());
                            break;
                        case 2:
                            StartCoroutine(SpikeAttack());
                            break;
                        default:
                            break;
                    }
                    break;
            }
        }
    }

    public IEnumerator MonsterMove()
    {
        int roar = Random.Range(0, 10);
        nextMove = 0;
        yield return new WaitForSeconds(1f);
        nextMove = Random.Range(-1, 2);
        if (roar < 3)
        {
            nextMove = 0;
            isRoar = true;
        }
        else
            isRoar = false;
        yield return new WaitForSeconds(3f);
        nextMove = 0;
        yield return new WaitForSeconds(0.7f);
        isStateEnd = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(0.5f,-0.5f), new Vector2(6, 6));
        foreach(Vector2 pos in spikePositions)
        {
            Gizmos.DrawWireSphere(transform.position + new Vector3(pos.x,pos.y), 0.3f);
        }
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
        yield return new WaitForSeconds(3f);
        isAttcking = false;
        anim.SetBool("isRollAttacking", false);
        moveSpeed = originSpeed;
        nextMove = 0;
    }

    public IEnumerator TakeOff()
    {
        anim.SetTrigger("TAKEOFF");
        yield return new WaitUntil(() => jumpingStart == true);
        rigid.velocity = new Vector2(0, 10);
        float originGravity = rigid.gravityScale;
        rigid.gravityScale = 0;
        yield return new WaitUntil(() => transform.position.y > 30);
        transform.position = new Vector3(GameManager.Instance.player.transform.position.x, transform.position.y);
        rigid.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(1f);
        isAttcking = true;
        rigid.gravityScale = 20f;
        yield return new WaitUntil(() => jumpingStart == false);
        isAttcking = false;
        rigid.gravityScale = originGravity;
    }

    public IEnumerator SpikeAttack()
    {
        anim.SetTrigger("SPIKEATTACK");
        yield return new WaitUntil(() => spikeStart == true);
        anim.SetBool("isTired", true);
        for(int i = 0; i < spikes.Length; i++)
        {
            spikes[i].transform.position = spikePositions[i]+transform.position;
            spikes[i].SetActive(true);
        }
        yield return new WaitForSeconds(3f);
        spikeStart = false;
        anim.SetBool("isTired", false);
    }

    public void JumpingStart()
    {
        jumpingStart = true;
    }

    public void RollingStart()
    {
        rollingStart = true;
    }

    public void RollingEnd()
    {
        rollingStart = false;
    }

    public void AttackStateEnd()
    {
        isStateEnd = true;
    }

    public void SpikeStart()
    {
        spikeStart = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "GROUND")
        {
            jumpingStart = false;
            anim.SetBool("isGround", true);
        }

        if(collision.gameObject.layer == 10)
        {
            nextMove *= -1;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "GROUND")
        {
            jumpingStart = true;
            anim.SetBool("isGround", false);
        }
    }
}
