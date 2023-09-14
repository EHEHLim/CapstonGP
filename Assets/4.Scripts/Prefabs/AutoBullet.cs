using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBullet : MonoBehaviour
{
    public Transform pos;
    public GameObject bullet;

    void Start()
    {
    }

    void Update()
    {
        Instantiate(bullet, pos.position, transform.rotation);
    }

}
