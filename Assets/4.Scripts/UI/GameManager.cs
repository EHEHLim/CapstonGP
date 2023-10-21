using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public GameObject damageEffect;
    public GameObject[] monsters;
    private static GameManager instance = null;
    public bool isSceneChanging = false;
    public GameObject player;
    public Canvas playerUi;
    public bool isStoryProcessing = false;
    private GameObject[] damageEffects;

    public int mapCounting = 0;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(this.gameObject);
        }

        damageEffects = new GameObject[10];
        for(int i = 0; i < damageEffects.Length; i++)
        {
            damageEffects[i] = Instantiate(damageEffect);
            damageEffects[i].SetActive(false);
            damageEffects[i].transform.parent = GameObject.Find("DamageEffectGroup").transform;
        }
    }

    public void Start()
    {
        SceneManager.sceneLoaded += LoadedSceneEvent;
    }

    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    void Update()
    {
        
    } 

    private void LoadedSceneEvent(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main" || scene.name == "Last")
        {
            player.SetActive(false);
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
        else
        {
            player.SetActive(true);
            player.GetComponent<Rigidbody2D>().gravityScale = 4;
        }

        if (scene.name == "Main" || scene.name == "Last")
        {
            playerUi.gameObject.SetActive(false);
        }
        else
        {
            playerUi.gameObject.SetActive(true);

        }
    }

    public void ShowDamageEffect(float dmg,Transform pos)
    {
        foreach(GameObject item in damageEffects)
        {
            if (item.activeSelf)
            {
                continue;
            }
            item.GetComponent<TextMeshPro>().text = dmg.ToString();
            item.transform.position = pos.position;
            item.SetActive(true);
        }
    }
}
