using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOLD : MonoBehaviour
{
    public float rotationX;
    public float rotationY;
    public float sensitivity = 5f;

    public Transform pivot;
    Quaternion rotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            rotationX -= Input.GetAxisRaw("Mouse Y") * sensitivity;
            rotationY += Input.GetAxisRaw("Mouse X") * sensitivity;
            rotation =  Quaternion.Euler(rotationX, rotationY, 0);
            //rotation.Normalize();
            pivot.rotation = rotation;   
    }
}
