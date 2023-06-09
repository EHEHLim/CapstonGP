using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public bool isSceneChanging = false;
    public GameObject player;
    public bool isStoryProcessing = false;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadedSceneEvent(Scene scene, LoadSceneMode mode)
    {
        if(player.activeSelf == true)
        {
            if(scene.name == "Main")
            {
                player.SetActive(false);
                player.GetComponent<Rigidbody2D>().gravityScale = 0;
            }
        }
        else
        {
            player.SetActive(true);
            player.GetComponent<Rigidbody2D>().gravityScale = 4;
        }
    }
}
