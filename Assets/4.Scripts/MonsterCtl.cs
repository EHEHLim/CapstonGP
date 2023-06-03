using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCtl : MonoBehaviour
{
    [SerializeField] private int hp;
    [SerializeField] private float moveSpeed;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void Damage(int damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            Invoke("Destroy", 3f);
        }
    }
}
