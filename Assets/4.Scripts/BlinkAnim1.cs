using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BlinkAnim1 : MonoBehaviour
{
    private float time;

    private RawImage rm;
    // Start is called before the first frame update
    void Start()
    {
        rm = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if(time < 0.5f)
        {
            rm.color = new Color(1, 1, 1, 1 - time);
        }
        else
        {
            rm.color = new Color(1, 1, 1, time);
            if (time > 1f)
            {
                time = 0;
            }
        }

        time += Time.deltaTime;
    }
}
