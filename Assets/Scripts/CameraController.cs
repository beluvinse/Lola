using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject cameraPos;
    Vector3 moveInput;
    bool canMove = true;

    public Transform target;


    private void FixedUpdate()
    {
        transform.position = new Vector3(target.position.x, 22f, target.position.z -12f);
    }

    private void Move(float xAxis, float zAxis)
    {
        //var dir = (transform.right * xAxis + transform.forward * zAxis).normalized;
         moveInput = new Vector3(xAxis, 0f, zAxis).normalized;
        //_myRB.MovePosition(transform.position + dir * _movSpeed * Time.fixedDeltaTime);
        transform.position += moveInput * Time.fixedDeltaTime * 15f;

    }

   
}
