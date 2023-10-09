using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Spike : MonoBehaviour
{
    [SerializeField] private float spikeSpeed;
    private float spikeDamage;
    // Start is called before the first frame update
    void Start()
    {
        spikeDamage = GetComponentInParent<BossMonster1>().spikeDamage;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * spikeSpeed;
        Invoke("gameObject.SetActive(false)", 4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.player.GetComponent<PlayerCtl>().Hit(spikeDamage);
        }
        gameObject.SetActive(false);
    }
}
