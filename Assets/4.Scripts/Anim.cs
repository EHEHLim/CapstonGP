using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        // Attack animation
        if (Input.GetKey(KeyCode.Z))
        {
            anim.SetTrigger("Attack");
        }

        // Move animation
        if (Input.GetAxisRaw("Horizontal") != 0)
            anim.SetBool("isRun", true);
        else
            anim.SetBool("isRun", false);
    }
}
