using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_MainUi : MonoBehaviour
{
    public RawImage panel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            StartCoroutine(MainScene());
        }
    }

    IEnumerator MainScene()
    {
        while(panel.color.a < 1f)
        {
            panel.color += new Color(0, 0, 0, 0.02f);
            yield return new WaitForSeconds(0.01f);
        }

        SceneManager.LoadScene(10);
    }
}
