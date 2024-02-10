using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //    [SerializeField] private float sensitivity = 1;
    //    [SerializeField] private float yClamp = 10; 

    //    private float xRotation = 0;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //void Update()
    //{
    //    RotateCamera();
    //}

    //private void RotateCamera()
    //{
    //    Vector2 input = InputManager.rotationInput;

    //    // Y rotation (left/right)
    //    transform.Rotate(Vector3.up * (input.x * sensitivity));

    //    // X rotation (up/down)
    //    xRotation -= input.y;
    //    xRotation = Mathf.Clamp(xRotation, -yClamp, yClamp);

    //    transform.localEulerAngles = new Vector3(xRotation, transform.localEulerAngles.y, 0);
    //}
}
