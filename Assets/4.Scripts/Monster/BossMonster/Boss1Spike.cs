using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Spike : MonoBehaviour
{
    [SerializeField] private float spikeSpeed;
    [SerializeField] private GameObject Boss;
    private float spikeDamage;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * spikeSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.player.GetComponent<PlayerCtl>().Hit(spikeDamage);
        }
        gameObject.SetActive(false);
    }

    private void hide()
    {
        gameObject.SetActive(false);
    }

    public void initSpike()
    {
        gameObject.SetActive(false);
        spikeDamage = Boss.GetComponent<BossMonster1>().spikeDamage;
    }
}
