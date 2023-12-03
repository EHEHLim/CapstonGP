using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    public float x, y;
    public int idx;
    [SerializeField] private RawImage panel;
    private bool isportalin = false;
    private bool portalClicked = false;

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().buildIndex == 11 || SceneManager.GetActiveScene().buildIndex == 10)
        {
            GameManager.Instance.RemainMonster = 0;
        }
        else
        {
            GameManager.Instance.RemainMonster = GameObject.Find("MG").GetComponent<MonsterGizmo>().MonsterSpawnPoints.Length;
        }

        GameManager.Instance.player.GetComponent<Transform>().position = new Vector3(x, y, 0);
        if (SceneManager.GetActiveScene().buildIndex != 2)
        {
            StartCoroutine(SceneStart());
        }
        while (true)
        {
            if(GameManager.Instance.mapCounting >= 7)
            {
                idx = 10;
                break;
            }

            if (GameManager.Instance.mapCounting == 3 || GameManager.Instance.mapCounting == 6)
            {
                idx = 11;
                break;
            }

            idx = (int)Random.Range(3, 10);

            if (SceneManager.GetActiveScene().buildIndex != idx)
            {
                break;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (isportalin && !portalClicked)
            {
                StartCoroutine(SceneChange());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isportalin = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isportalin = false;
        }
    }

    IEnumerator SceneStart()
    {
        panel.color = new Color(0, 0, 0, 1f);
        GameManager.Instance.isSceneChanging = true;
        while(panel.color.a > 0f)
        {
            panel.color -= new Color(0, 0, 0, 0.03f);
            yield return new WaitForSeconds(0.01f);
        }
        GameManager.Instance.isSceneChanging = false;
    }

    IEnumerator SceneChange()
    {
        portalClicked = true;
        if (SceneManager.GetActiveScene().buildIndex != 11)
        {
            GameManager.Instance.mapCounting++;
            GameManager.Instance.KillingPoint += GameManager.Instance.PassingMapPointAdd;
        }
        panel.color = new Color(0, 0, 0, 0f);
        GameManager.Instance.isSceneChanging = true;
        while(panel.color.a < 1f)
        {
            panel.color += new Color(0, 0, 0, 0.03f);
            yield return new WaitForSeconds(0.01f);
        }
        portalClicked = false;
        SceneManager.LoadScene(idx);
    }
}
