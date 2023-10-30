using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selections : MonoBehaviour
{
    public int abilitySelection;
    
    public void OnClickSelection()
    {
        switch (abilitySelection)
        {
            case 0:
                PowerUp();
                break;
            case 1:
                SpeedUp();
                break;
            case 2:
                HpUp();
                break;
            case 3:
                KillingPointSave();
                break;
        }
    }

    private void PowerUp()
    {
        if (GameManager.Instance.KillingPoint >= 3)
        {
            GameManager.Instance.KillingPoint -= 3;
            GameManager.Instance.player.GetComponent<PlayerCtl>().arrowDamage += GameManager.Instance.player.GetComponent<PlayerCtl>().arrowDamage * 0.1f;
            GameManager.Instance.player.GetComponent<PlayerCtl>().attackDamage += GameManager.Instance.player.GetComponent<PlayerCtl>().attackDamage * 0.1f;
            this.gameObject.SetActive(false);
        }
    }

    private void SpeedUp()
    {
        if (GameManager.Instance.KillingPoint >= 2)
        {
            GameManager.Instance.KillingPoint -= 2;
            GameManager.Instance.player.GetComponent<PlayerCtl>().moveSpeed += GameManager.Instance.player.GetComponent<PlayerCtl>().moveSpeed * 0.1f;
            this.gameObject.SetActive(false);
        }
    }

    private void HpUp()
    {
        if (GameManager.Instance.KillingPoint >= 3)
        {
            GameManager.Instance.KillingPoint -= 3;
            GameManager.Instance.player.GetComponent<PlayerCtl>().hp += 10f;
            GameManager.Instance.player.GetComponent<PlayerCtl>().currHp += 10f;
            this.gameObject.SetActive(false);
        }
    }

    private void KillingPointSave()
    {
        if (GameManager.Instance.KillingPoint >= 4)
        {
            GameManager.Instance.KillingPoint -= 4;
            GameManager.Instance.PassingMapPointAdd++;
            this.gameObject.SetActive(false);
        }
    }
}
