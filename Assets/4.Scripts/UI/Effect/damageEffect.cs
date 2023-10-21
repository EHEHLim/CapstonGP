using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class damageEffect : MonoBehaviour
{
    private float moveSpeed;
    private float alphaSpeed;
    TextMeshPro text;
    public Color alpha;

    void Start()
    {
        moveSpeed = 2.0f;
        alphaSpeed = 2.0f;

        text = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); // 텍스트 위치

        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // 텍스트 알파값
        text.color = alpha;
    }
}
