using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCol : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TGROUND"))
            GetComponent<CapsuleCollider2D>().isTrigger = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("TGROUND"))
            GetComponent<CapsuleCollider2D>().isTrigger = false;
    }
}
