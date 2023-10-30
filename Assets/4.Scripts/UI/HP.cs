using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    Image hpBar;
    // Start is called before the first frame update
    void Start()
    {
        hpBar = GameObject.FindGameObjectWithTag("HP_BAR")?.GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        hpBar.fillAmount = GameManager.Instance.player.GetComponent<PlayerCtl>().currHp / GameManager.Instance.player.GetComponent<PlayerCtl>().hp;
    }
}
