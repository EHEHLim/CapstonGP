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
    private void Awake()
    {
        GameManager.Instance.player.GetComponent<Transform>().position = new Vector3(x, y, 0);
        StartCoroutine(SceneStart());
        while(true)
        {
            idx = (int)Random.Range(2,9);
            
            if(SceneManager.GetActiveScene().buildIndex != idx)
            {
                break;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                StartCoroutine(SceneChange());
                Debug.Log("onPortal");
            }
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
        panel.color = new Color(0, 0, 0, 0f);
        GameManager.Instance.isSceneChanging = true;
        while(panel.color.a < 1f)
        {
            panel.color += new Color(0, 0, 0, 0.03f);
            yield return new WaitForSeconds(0.01f);
        }
        SceneManager.LoadScene(idx);
    }
}
