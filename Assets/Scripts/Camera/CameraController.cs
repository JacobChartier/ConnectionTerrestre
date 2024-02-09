using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector2 mouseInput;

    [SerializeField] private float sensitivity = 10;
    [SerializeField] private float yClamp = 60;

    private float xRotation = 0;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        Vector2 input = InputManager.rotationInput;

        // Rotation autour de l'axe Y (gauche et droite)
        transform.Rotate(Vector3.up * (input.x * sensitivity));

        // Rotation autour de l'axe X (haut et bas)
        xRotation -= input.y;
        //xRotation = Mathf.Clamp(xRotation, -yClamp, yClamp); // Clamp pour éviter une rotation trop grande

        transform.localEulerAngles = new Vector3(xRotation, transform.localEulerAngles.y, 0);
    }
}
