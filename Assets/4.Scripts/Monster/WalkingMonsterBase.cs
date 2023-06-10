using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WalkingMonsterBase : BaseMonster
{
    protected void WalkingInit()
    {
        state = State.IDLE;
        rigid = GetComponent<Rigidbody2D>();
        rigid.gravityScale = 4;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
