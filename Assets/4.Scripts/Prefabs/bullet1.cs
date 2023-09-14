using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet1 : MonoBehaviour
{
    public float speed;
    void Start()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
        Destroy(gameObject, 3.0f);
    }

    void Update()
    {
        
    }
    
}
