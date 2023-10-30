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
    private SfxSoundController sound;

    public int mapCounting = 0;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);

            sound = GetComponent<SfxSoundController>();
            damageEffects = new GameObject[10];
            for (int i = 0; i < damageEffects.Length; i++)
            {
                damageEffects[i] = Instantiate(damageEffect);
                damageEffects[i].SetActive(false);
                damageEffects[i].transform.SetParent(GameObject.Find("DamageEffectGroup").transform);
            }

        }
        else
        {
            Destroy(this.gameObject);
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

    public SfxSoundController Sound
    {
        get
        {
            if(sound == null)
            {
                return null;
            }
            return sound;
        }
    }

    void Update()
    {
        
    } 

    private void LoadedSceneEvent(Scene scene, LoadSceneMode mode)
    {
        foreach (var item in player.GetComponent<PlayerCtl>().ArrowPool)
        {
            item.SetActive(false);
        }
        foreach(var item in damageEffects)
        {
            item.SetActive(false);
        }
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

    public IEnumerator ShowDamageEffect(int dmg,Transform pos)
    {
        for(int i = 0; i < damageEffects.Length; i++)
        {
            if (damageEffects[i].activeSelf == false)
            {
                damageEffects[i].GetComponent<TextMeshPro>().text = dmg.ToString();
                damageEffects[i].transform.position = pos.position;
                damageEffects[i].GetComponent<damageEffect>().alpha = new Color(1,1,1,1);
                
                damageEffects[i].SetActive(true);
                yield return new WaitForSeconds(2f);
                damageEffects[i].SetActive(false);
                break;
            }
        }
    }
}
