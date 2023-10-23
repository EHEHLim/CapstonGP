using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallenZone : MonoBehaviour
{
    private GameObject portal;
    private Vector3 respon;
    // Start is called before the first frame update
    void Awake()
    {
        portal = GameObject.Find("Portal");
        respon = new Vector3(portal.GetComponent<Portal>().x, portal.GetComponent<Portal>().y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = respon;
        }
          
    }
}
