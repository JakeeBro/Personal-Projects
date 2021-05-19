using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Variables")]
    [Range(.1f, 1000f)] public float sensitivity = 500f;
    [Range(0, 90)] public int vRotation = 90;

    private float xRotation = 0f;

    [Header("Remote References")]
    public Transform player;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -vRotation, vRotation);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        player.Rotate(Vector3.up * mouseX);
    }
}
