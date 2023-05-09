using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialStory : MonoBehaviour
{
    private string[] m_text_1;
    private bool isTyping = false;
    public TextMeshProUGUI txt;
    public float typingSpeed = 0.05f;
    public GameObject arrow;
    public RawImage panel;

    private void Awake()
    {
        m_text_1 = new string[]{
            "",
            "",
            ""
        };

        StartCoroutine(Tutorial_1());
    }

    IEnumerator Tutorial_1()
    {
        StartCoroutine(FadeIn(panel));
        GameManager.Instance.isStoryProcessing = true;
        for (int i = 0; i < m_text_1.Length; i++)
        {
            arrow.SetActive(false);
            StartCoroutine(Typing(m_text_1[i]));
            yield return new WaitUntil(() => !isTyping);
            arrow.SetActive(true);
            yield return new WaitUntil(() => Input.anyKeyDown);
        }
        StartCoroutine(FadeOut(panel));
    }

    IEnumerator Typing(string str)
    {
        isTyping = true;
        txt.text = "";
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < str.Length; i++)
        {
            txt.text = str.Substring(0, i);
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    IEnumerator FadeIn(RawImage pn)
    {
        while(pn.color.a < 0.6)
        {
            pn.color += new Color(0, 0, 0, 0.02f);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator FadeOut(RawImage pn)
    {
        while(pn.color.a > 0f)
        {
            pn.color -= new Color(0, 0, 0, 0.02f);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
