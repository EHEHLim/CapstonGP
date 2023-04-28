using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BlinkAnim : MonoBehaviour
{
    private float time;

    public TextMeshProUGUI tmppro;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(time < 0.5f)
        {
            tmppro.color = new Color(1, 1, 1, 1 - time);
        }
        else
        {
            tmppro.color = new Color(1, 1, 1, time);
            if (time > 1f)
            {
                time = 0;
            }
        }

        time += Time.deltaTime;
    }
}
