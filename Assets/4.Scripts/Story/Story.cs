using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Story : MonoBehaviour
{

    private bool isTyping = false;
    public TextMeshProUGUI txt;
    public TextMeshProUGUI mainText;
    public float typingSpeed = 0.05f;
    public int storyProcess;

    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private RawImage mainPanel;
    [SerializeField] private Image panel1;
    [SerializeField] private Image panel2;
    private bool isFading = false;

    private string[] mainPanelStory;
    private string[] ScientistStory;
    private string[] mainStory;
    private string[] mainStory2;
    private string[] pastThings;

    // Start is called before the first frame update
    void Awake()
    {
        mainPanelStory = new string[]
        {
            "20XX\n\n세상은 급속도로 발전하고 있었고\n인류 문명의 빠른 발전은 환경파괴를 야기하고 있었다."/*,
            "지구는 그에 대한 복수라도 하는 듯,\n인류 문명에는 정체불명의 전염병이 창궐하였다.",
            "그로인해 인류의 90%가 사망하였다.\n남은 과학자의 연구로 전염병은 한 바이러스로 인한 것임을 알게됐지만\n바이러스의 전염경로는 알 수 있는 방법이 없었고\n치료약 또한 만들 수 있는 방법이 없었다.",
            "절망 속에서 인류를 종속시키기 위해\n과학자들은 한가지 선택을 하게된다."*/
        };
        ScientistStory = new string[]
        {
            "바이러스가 원인인 걸 알았지만... 이건... 도저히 방법이 없군....",/*
            "바이라서의 잠복기가 10년인 탓에 아직 남아있는 인류도 바이러스에 감염 되어 있을거야...",
            "지직... 지....지지직...",
            "!?",
            "아직 남아있는 연구소가 있었나...?",
            "???:한국 연구소 있습니까???",
            "누구십니까...?",
            "??? : 아...! 신이시어...!",
            "??? : 저는 미국의 연구소장 입니다!",
            "미국연구소장 : 저희는 지금 세계 각국에 남아있는 연구소를 모으고 있습니다",
            "이미 멸망한 세상에 무슨 희망이 있어서 모으고 계십니까...",
            "미국연구소장 : ....",
            "미국연구소장 : .......",
            "미국연구소장 : .......아직 희망이 있을지도 모릅니다.",
            "예...?",
            "미국연구소장 : 저희는 이미 늦었지만, 클론을 이용하면 인류는 아직 끝나지 않았을지도 모릅니다.",
            "클론이라니... 그건 윤리적으로 금지된 기술이지 않습니까..",
            "미국연구소장 : 이미 인류가 멸망한 마당에 그런게 중요하겠습니까??",
            "ㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋ",
            "좋습니다! 계획이 뭔지 들어나 보죠!",
            "미국연구소장 : 저희가 최근에 연구한 결과에 따르면 인류를 멸망시킨 바이러스는...",
            "바이러스는?",
            "미국연구소장 : 현재까지 인류말고는 감염된 동물종이 없다는 겁니다.",
            "!?",
            "미국연구소장 : 그점을 이용해서 클론을 만들어두고, 인류가 멸망한 후 훗날 바이러스가 사라지면",
            "미국연구소장 : 클론들을 일제히 깨우는 겁니다",
            "깨울땐 어떻게 하죠? 몇년, 아니 몇천년이 지나야 할지도 모르는데 관리가 되지 않으면 다 의미 없습니다..",
            "미국연구소장 : 자가보완 안드로이드를 이용하면 됩니다.",
            "그런 방법이...!",
            "미국연구소장 : 안드로이드가 지속적으로 클론들을 관리하고, 바이러스가 사라지게 된다면 클론들을 일제히 깨우는 겁니다.",
            "알겠습니다.. 그럼 저희도 바로 작업에 착수하겠습니다..",
            "미국연구소장 : 인류를 위해 잘 부탁 드립니다!",
            "(무전이 끊어졌다)",
            "(지하실로 이동하자)",
            "지직....툭",*/
            "(계단앞 문에서 위화살표키를 누르면 이동합니다)"
        };
        mainStory = new string[]
        {
            "지직... 지지직....",/*
            "나는....누구....?",
            "!!!!!",
            "으윽 머리가....!",
            "그래...나는 XXX박사님이 만드신 클론관리 안드로이드...",
            "근데 내가 왜 여기에...?",
            "!!!",
            "내 관리하의 클론들은 어떻게 된거지!?!?",
            "지직....툭",*/
            "크으윽.. 다시 머리가...!"
        };
        pastThings = new string[]
        {
            "나는 XXX박사님의 임무를 받들어 클론들을 관리하고 있었다\n수백년간....\n바이러스가 사라지기만을 기다리며...",
            "박사님의 유언에 따르면 클론들을 관리하는 안드로이드는\n세계 각국에 있는 듯 하였다.",
            "나는 클론들을 관리하며 평화로운 나날들을 보내고 있었다...\n그날이 있기 전까지는...",
            "지지직....\n 코드 : MEXRROR_2030-6\n해당 기체의 기억에 결함 발생\n가까운 연구소로 가서 수리요구"
        };
        mainStory2 = new string[]
        {
            "(기억들이 혼탁하다... 한시 빨리 연구소로 가야한다)",
            "안드로이드는 유사시를 대비해 전투능력을 갖고 있습니다.",
            "이동-방향키, 점프-스페이스바(더블점프 가능), 공격 z, 대쉬 Lshift, 무기교체 Tab"
        };
        mainPanel.color = new Color(0, 0, 0, 1);
        mainText.gameObject.SetActive(false);
        txt.gameObject.SetActive(false);
    }

    private void Start()
    {
        if(storyProcess == 1)
        {
            StartCoroutine(StoryProcessMap1());
        }
        else if(storyProcess == 2)
        {
            StartCoroutine(storyProcessMap2());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator storyProcessMap2()
    {
        GameManager.Instance.isStoryProcessing = true;

        mainText.gameObject.SetActive(true);
        StartCoroutine(Typing("3xxx년 한국 숲속 어딘가",mainText));
        yield return new WaitUntil(() => isTyping == false);
        yield return new WaitUntil(() => Input.anyKeyDown);
        mainText.text = "";
        mainText.gameObject.SetActive(false);
        StartCoroutine(FadeIn(mainPanel));
        yield return new WaitUntil(() => isFading == false);

        StartCoroutine(StorySlideIn(panel1));
        StartCoroutine(StorySlideIn(panel2));
        txt.gameObject.SetActive(true);
        yield return new WaitUntil(() => isFading == false);
        foreach(string str in mainStory)
        {
            txt.text = "";
            StartCoroutine(Typing(str, txt));
            yield return new WaitUntil(() => isTyping == false);
            yield return new WaitUntil(() => Input.anyKeyDown);
        }
        txt.gameObject.SetActive(false);

        StartCoroutine(FadeOut(mainPanel));
        mainText.gameObject.SetActive(true);
        foreach(string str in pastThings)
        {
            mainText.text = "";
            StartCoroutine(Typing(str, mainText));
            yield return new WaitUntil(() => isTyping == false);
            yield return new WaitUntil(() => Input.anyKeyDown);
        }
        mainText.gameObject.SetActive(false);
        StartCoroutine(FadeIn(mainPanel));

        txt.gameObject.SetActive(true);
        foreach (string str in mainStory2)
        {
            txt.text = "";
            StartCoroutine(Typing(str, txt));
            yield return new WaitUntil(() => isTyping == false);
            yield return new WaitUntil(() => Input.anyKeyDown);
        }
        txt.gameObject.SetActive(false);
        StartCoroutine(StroySlideOut(panel1));
        StartCoroutine(StroySlideOut(panel2));
        yield return new WaitUntil(() => isFading == false);
        GameManager.Instance.isStoryProcessing = false;

    }

    private IEnumerator StoryProcessMap1()
    {
        GameManager.Instance.isStoryProcessing = true;

        mainText.gameObject.SetActive(true);
        foreach(string str in mainPanelStory)
        {
            mainText.text = "";
            mainText.color = new Color(1f, 1f, 1f, 1f);
            StartCoroutine(Typing(str,mainText));
            yield return new WaitUntil(() => isTyping == false);
            yield return new WaitUntil(() => Input.anyKeyDown);
            while(mainText.color.a > 0f)
            {
                mainText.color -= new Color(0, 0, 0, 0.03f);
                yield return new WaitForSeconds(0.01f);
            }
        }
        mainText.gameObject.SetActive(false);

        GameManager.Instance.player.transform.position = playerSpawnPoint.position;

        StartCoroutine(FadeIn(mainPanel));
        yield return new WaitUntil(() => isFading == false);

        StartCoroutine(StorySlideIn(panel1));
        StartCoroutine(StorySlideIn(panel2));
        yield return new WaitUntil(() => isFading == false);
        txt.gameObject.SetActive(true);
        foreach (string str in ScientistStory)
        {
            txt.text = "";
            StartCoroutine(Typing(str, txt));
            yield return new WaitUntil(() => isTyping == false);
            yield return new WaitUntil(() => Input.anyKeyDown);
        }
        txt.gameObject.SetActive(false);
        StartCoroutine(StroySlideOut(panel1));
        StartCoroutine(StroySlideOut(panel2));
        yield return new WaitUntil(() => isFading == false);
        GameManager.Instance.isStoryProcessing = false;
    }

    private IEnumerator StorySlideIn(Image image)
    {
        isFading = true;
        while(image.fillAmount < 1f)
        {
            image.fillAmount += 0.03f;
            yield return new WaitForSeconds(0.01f);
        }
        isFading = false;
    }

    private IEnumerator StroySlideOut(Image image)
    {
        isFading = true;
        while (image.fillAmount > 0f)
        {
            image.fillAmount -= 0.03f;
            yield return new WaitForSeconds(0.01f);
        }
        isFading = false;
    }

    //Typing 효과 Coroutine : 한글자씩 추가로 출력해가는 코드
    IEnumerator Typing(string str,TextMeshProUGUI txt)
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
        isFading = true;
        pn.color = new Color(0, 0, 0, 1f);
        while (pn.color.a > 0f)
        {
            pn.color -= new Color(0, 0, 0, 0.03f);
            yield return new WaitForSeconds(0.01f);
        }
        isFading = false;
    }

    //Fade Out 효과 Coroutine
    IEnumerator FadeOut(RawImage pn)
    {
        isFading = true;
        pn.color = new Color(0, 0, 0, 0f);
        while (pn.color.a < 1f)
        {
            pn.color += new Color(0, 0, 0, 0.03f);
            yield return new WaitForSeconds(0.01f);
        }
        isFading = false;
    }
}

