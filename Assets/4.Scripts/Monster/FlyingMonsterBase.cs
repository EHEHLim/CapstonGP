using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlyingMonsterBase : BaseMonster
{
    private void Awake()
    {
        state = State.IDLE;
        rigid = GetComponent<Rigidbody2D>();
        rigid.gravityScale = 0;
    }
}
