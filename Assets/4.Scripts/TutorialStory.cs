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
    private bool isFading = false;

    private void Awake()
    {
        m_text_1 = new string[]{
            "대화창 1",
            "대화창 2",
            "대화창 3",
            "대화창 4",
            "대화창 5"
        };

        //초기화
        txt.gameObject.SetActive(false);
        arrow.gameObject.SetActive(false);
        //시
        StartCoroutine(Tutorial_1());
    }

    IEnumerator Tutorial_1() //전체 스토리 첫번째 진행
    {
        //스토리 진행동안 Player조작을 할 수 없도록 여러곳에서 접근할 수 있는
        //GameManager에 isStoryProcessing변수를 생성후, true값을 줌
        GameManager.Instance.isStoryProcessing = true;

        StartCoroutine(FadeIn(panel)); //Panel Fade in

        //Fading이 끝나고 text창 (대화창)을 열어야 하므로,
        //isFading이 false로 변할때까지 대기하다가 변하면 다음 문장 실행
        //(() => bool값 or bool값 반환 함수 or 조건문) WaitUntil함수는 매개변수로 함수가 들어가야함
        //isFading이라는 bool값 변수를 함수처럼 사용하기 위하
        yield return new WaitUntil(() => isFading == false);

        //text창을 보이게 함
        txt.gameObject.SetActive(true);
        for (int i = 0; i < m_text_1.Length; i++)
        {
            arrow.SetActive(false);                             //텍스트 출력 중에는 화살표를 보이게 하지 않게함
            StartCoroutine(Typing(m_text_1[i]));                //한글자씩 출력
            yield return new WaitUntil(() => !isTyping);        //Typing이 끝날때까지 대기 isFading과 같은 원리
            arrow.SetActive(true);                              //텍스트 출력이 끝나면 화살표를 보이게 함
            yield return new WaitUntil(() => Input.anyKeyDown); //사용자 입력시까지 대기
        }
        //text창을 가림
        txt.gameObject.SetActive(false);
        //스토리 종료
        StartCoroutine(FadeOut(panel));

        yield return new WaitUntil(() => isFading == false);
        GameManager.Instance.isStoryProcessing = false;
    }


    //Typing 효과 Coroutine : 한글자씩 추가로 출력해가는 코드
    IEnumerator Typing(string str)
    {
        isTyping = true; //typing 시작
        txt.text = ""; // text 대사창 초기화
        yield return new WaitForSeconds(1f);
        //1초 대기

        //SubString으로 문자열 0번째 부터 i번째까지 반복 typingSpeed로 속도 조절
        for (int i = 0; i < str.Length; i++)
        {
            txt.text = str.Substring(0, i);                 // str문자열의 0번쨰 부터 i번째까지의 문자열을 txt.text에 반영 (대사창)
            yield return new WaitForSeconds(typingSpeed);   // 글자 출력 후 0.05초 기다
        }
        isTyping = false; //typing 끝
    }

    
    //Fade In 효과 Coroutine
    IEnumerator FadeIn(RawImage pn)
    {
        //Fading 효과 시작 (Fade In과 Fade Out이 동시에 일어날순 없으므로 동일한 변수 취급
        isFading = true;
        while(pn.color.a < 0.6) //pnael의 투명도가 0.6을 넘지 않도록 조건문 설정 (0~1 백분율)
        {
            pn.color += new Color(0, 0, 0, 0.02f);  //투명도 비율을 0.02씩 증가시
            yield return new WaitForSeconds(0.01f); //0.01초 대
        }
        //Fading 끝
        isFading = false;
    }

    //Fade Out 효과 Coroutine
    IEnumerator FadeOut(RawImage pn)
    {
        isFading = true; //Fading 시작
        while(pn.color.a > 0f)
        {
            pn.color -= new Color(0, 0, 0, 0.02f);  //panel의 투명도를 0.02씩 낮
            yield return new WaitForSeconds(0.01f); //0.01초 대기
        }
        isFading = false; //Fading 끝
    }
}
