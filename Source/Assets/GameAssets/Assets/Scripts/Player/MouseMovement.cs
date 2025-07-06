using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    // Player objesini Inspector'dan atayacaðýz
    public Transform playerBody;

    public float mouseSensitivity = 100f;
    float xRotation = 0f;
    // float YRotation = 0f;
    public static MouseMovement Instance { get; set; }
    public bool isMouseLocked { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        SetMouseLock(true);
    }

    public void SetMouseLock(bool locked)
    {
        isMouseLocked = locked;
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
    }

    void Update()
    {
        if (isMouseLocked) 
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            //control rotation around x axis (Look up and down)
            xRotation -= mouseY;

            //we clamp the rotation so we cant Over-rotate (like in real life)
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            //control rotation around y axis (Look up and down)
            //YRotation += mouseX;

            //applying both rotations
            // transform.localRotation = Quaternion.Euler(xRotation, YRotation, 0f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // Player gövdesi saða-sola dönme (Y ekseni)
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }

    // ESC için
    private void OnEnable()  
    {
        // ESC menüsü açýldýðýnda mouse kilidini sýfýrla
        PauseMenuManager.OnPauseStateChanged += HandlePauseState;
    }
    private void OnDisable()
    {
        PauseMenuManager.OnPauseStateChanged -= HandlePauseState;
    }
    private void HandlePauseState(bool isPaused)
    {
        SetMouseLock(!isPaused);
    }




}