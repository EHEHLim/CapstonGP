using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    private int killingPoint = 0;
    private int remainMonster = 0;
    private int passingMapPointAdd = 0;
    public GameObject damageEffect;
    public GameObject[] monsters;
    private static GameManager instance = null;
    public bool isSceneChanging = false;
    public GameObject player;
    public Canvas playerUi;
    public bool isStoryProcessing = false;
    private GameObject[] damageEffects;
    private SfxSoundController sound;
    [SerializeField] TextMeshProUGUI killingPointText;
    [SerializeField] TextMeshProUGUI remainMonsterText;
    private bool isStoreOpen = false;
    private bool isStoreIn = false;
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

    private void Update()
    {
        killingPointText.text = "killing point : " + killingPoint.ToString();
        remainMonsterText.text = "Remain Monster : " + remainMonster.ToString();
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

    public bool IsStoreIn
    {
        get
        {
            return isStoreIn;
        }
        set
        {
            isStoreIn = value;
        }
    }

    public bool IsStoreOpen
    {
        get
        {
            return isStoreOpen;
        }
        set
        {
            isStoreOpen = value;
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

    public int KillingPoint
    {
        get
        {
            return killingPoint;
        }
        set
        {
            killingPoint = value;
        }
    }

    public int RemainMonster
    {
        get
        {
            return remainMonster;
        }
        set
        {
            remainMonster = value;
        }
    }

    public int PassingMapPointAdd
    {
        get
        {
            return passingMapPointAdd;
        }
        set
        {
            passingMapPointAdd = value;
        }
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
            killingPointText.gameObject.SetActive(false);
            remainMonsterText.gameObject.SetActive(false);
        }
        else
        {
            playerUi.gameObject.SetActive(true);

            killingPointText.gameObject.SetActive(true);
            remainMonsterText.gameObject.SetActive(true);
        }

    }

    private IEnumerator ShowDamageEffect(float dmg,Transform pos)
    {
        for(int i = 0; i < damageEffects.Length; i++)
        {
            if (damageEffects[i].activeSelf == false)
            {
                damageEffects[i].GetComponent<TextMeshPro>().text = ((int)dmg).ToString();
                damageEffects[i].transform.position = pos.position;
                damageEffects[i].GetComponent<damageEffect>().alpha = new Color(1,1,1,1);
                
                damageEffects[i].SetActive(true);
                yield return new WaitForSeconds(2f);
                damageEffects[i].SetActive(false);
                break;
            }
        }
    }

    public void ShowDamage(float dmg,Transform pos)
    {
        StartCoroutine(ShowDamageEffect(dmg, pos));
    }
}
