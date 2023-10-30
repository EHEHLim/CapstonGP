using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class StoreManager : MonoBehaviour
{
    public GameObject ZkeyImage;
    public RawImage storePanel;
    public GameObject[] storeSelections;
    private string[] abilities;
    private Selections[] selectionButtons;

    // Start is called before the first frame update

    private void Start()
    {
        abilities = new string[]
        {
            "공격력 증가\n\n\n현재 플레이어의 공격력이 10% 증가합니다.\n\n공격력 증가 +10%\n\n\n\nkilling point : 3p",
            "이동속도 증가\n\n\n현재 플레이어의 이동속도가 10% 증가합니다.\n\n이동속도 증가 +10%\n\n\n\nkilling point : 2p",
            "체력 증가\n\n\n현재 플레이어의 체력이 10 증가합니다.\n\n체력 증가 +10\n\n\n\nkilling point : 3p",
            "killing point 저축\n\n\n플레이어가 맵을 통과할 때 마다\nkilling point가 1포인트씩 증가합니다\n\n\n\nkilling point : 4p"
        };

        selectionButtons = new Selections[3];

        for(int i = 0; i < selectionButtons.Length; i++)
        {
            selectionButtons[i] = storeSelections[i].GetComponent<Selections>();
        }
    }

    

    void OnEnable()
    {
        storePanel.gameObject.SetActive(false);
        ZkeyImage.SetActive(false);
        foreach(var item in storeSelections)
        {
            item.GetComponent<TextMeshProUGUI>().text = "";
        }
        for (int i = 0; i < storeSelections.Length; i++)
        {
            int idx = Random.Range(0, 4);
            storeSelections[i].GetComponent<TextMeshProUGUI>().text = abilities[idx];
            selectionButtons[i].abilitySelection = idx;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (ZkeyImage.activeSelf)
            {
                storePanel.gameObject.SetActive(!storePanel.gameObject.activeSelf);
                GameManager.Instance.IsStoreOpen = !GameManager.Instance.IsStoreOpen;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ZkeyImage.SetActive(true);
            GameManager.Instance.IsStoreIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ZkeyImage.SetActive(false);
            storePanel.gameObject.SetActive(false);
            GameManager.Instance.IsStoreIn = false;
            GameManager.Instance.IsStoreOpen = false;
        }
    }
}

