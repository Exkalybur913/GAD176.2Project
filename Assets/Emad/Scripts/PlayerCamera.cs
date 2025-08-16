using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour

{
    public float sensY;
    public float sensX;
    public float rotX;
    public float rotY;
    public Transform look;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Mouse input
    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        rotY += mouseX;

        rotX -= mouseY;
        rotX = Mathf.Clamp(rotX, -90f, 90f);

        // Camera Rotation and Oriantation
        transform.rotation = Quaternion.Euler(rotX, rotY, 0);
        look.rotation = Quaternion.Euler(0, rotY, 0);
    }
}
        


