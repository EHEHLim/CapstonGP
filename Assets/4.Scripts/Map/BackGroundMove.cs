using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxEffectMultiplier;
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

    }

    private void LateUpdate()
    {
        Vector3 deltaMove = cameraTransform.position - lastCameraPosition;
        
        transform.position += new Vector3(deltaMove.x * parallaxEffectMultiplier.x, deltaMove.y * parallaxEffectMultiplier.y) ;
        lastCameraPosition = cameraTransform.position;

    }
}
