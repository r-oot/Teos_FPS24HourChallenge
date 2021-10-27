using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera Cam;

    private float MoveSpeed = 10;
    float xRotation = 0;
    private void Awake()
    {
        Cam = Camera.main;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void ChangeRotationLook(float x, float y)
    {
        xRotation -= y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        Cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(x * Vector3.up);
    }
    public void MoveCharacter(Vector3 dir)
    {
        transform.position += dir * MoveSpeed * Time.deltaTime;
    }
}
