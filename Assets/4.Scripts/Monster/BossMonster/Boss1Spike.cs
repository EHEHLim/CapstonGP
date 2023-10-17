using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Spike : MonoBehaviour
{
    [SerializeField] private float spikeSpeed;
    [SerializeField] private GameObject Boss;
    private float spikeDamage;
    void Update()
    {
        transform.position -= transform.right * spikeSpeed * Time.deltaTime;
        Invoke("hide", 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.player.GetComponent<PlayerCtl>().Hit(spikeDamage);

            gameObject.SetActive(false);
        }
    }

    private void hide()
    {
        gameObject.SetActive(false);
    }

    public void initSpike()
    {
        spikeDamage = Boss.GetComponent<BossMonster1>().spikeDamage;
        gameObject.SetActive(false);
    }
}
