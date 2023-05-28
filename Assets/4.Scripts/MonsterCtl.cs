using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCtl : MonoBehaviour
{
    public int Hp = 3;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void Damage(int damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            Destroy(gameObject);
        }
        Debug.Log("hit");
    }
}
