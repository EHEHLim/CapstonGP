using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Tile : MonoBehaviour
{
    public Transform desPos;
    public float speed;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, desPos.position, Time.deltaTime * speed);
    }
}
