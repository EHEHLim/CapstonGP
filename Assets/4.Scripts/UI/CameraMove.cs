using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float maxX;
    private Transform playerTr;

    private void Awake()
    {
        player = GameManager.Instance.player;
        playerTr = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        if(playerTr.position.x > 0 && playerTr.position.x < maxX)
        {
            transform.position = new Vector3(playerTr.position.x, transform.position.y, -10f);
        }
        else if(playerTr.position.x < 0)
        {
            transform.position = new Vector3(0f, transform.position.y, -10f);
        }
        else if(playerTr.position.x > maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, -10f);
        }

        if (playerTr.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, playerTr.position.y, -10f);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, 0, -10f);
        }
    }
}
