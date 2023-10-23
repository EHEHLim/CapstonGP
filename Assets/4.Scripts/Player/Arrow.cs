using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int arrowDamage;
    // Start is called before the first frame update
    void Start()
    {
        arrowDamage = GameManager.Instance.player.GetComponent<PlayerCtl>().arrowDamage;
    }

    // Update is called once per frame
    private void OnEnable()
    {
        Invoke("Destroy", 3f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Monster"))
        {
            collision.gameObject.GetComponent<BaseMonster>().hit(arrowDamage);
        }
        Destroy();
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
        transform.parent = GameManager.Instance.player.transform;
    }

}
