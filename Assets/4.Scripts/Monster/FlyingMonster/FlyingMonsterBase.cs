using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlyingMonsterBase : BaseMonster
{
    protected void FlyingInit()
    {
        audioSource = GetComponent<AudioSource>();
        state = State.IDLE;
        rigid = GetComponent<Rigidbody2D>();
        rigid.gravityScale = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
