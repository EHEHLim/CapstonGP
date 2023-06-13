using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LastUi : MonoBehaviour
{

    private void Awake()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickYes()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnClickNo()
    {
        Application.Quit();
    }
}
