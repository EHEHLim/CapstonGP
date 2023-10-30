using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryPortal : MonoBehaviour
{
    [SerializeField] private RawImage panel;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                StartCoroutine(FadeOut(panel));
            }
        }
    }

    private IEnumerator FadeOut(RawImage pn)
    {
        while(pn.color.a > 0f)
        {
            pn.color -= new Color(0, 0, 0, 0.02f);  //panel의 투명도를 0.02씩 낮
            yield return new WaitForSeconds(0.01f); //0.01초 대기
        }
        SceneManager.LoadScene(2);
    }
}
