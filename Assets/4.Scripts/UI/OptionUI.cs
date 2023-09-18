using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionUI : MonoBehaviour
{
    public AudioSource btnsource;
    [SerializeField] private GameObject optionPanel;
    // Start is called before the first frame update
    private void Awake()
    {
        optionPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            return;
        }

        if (GameManager.Instance.isSceneChanging)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionPanel.activeSelf)
            {
                optionPanel.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                optionPanel.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void OnClickBack()
    {
        optionPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnClickMute()
    {
        Debug.Log("onMute");
    }

    public void OnValueChangedSound()
    {

    }
}
