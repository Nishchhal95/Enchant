using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool isCursorStateEnabled = false;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private GameControls gameControls;
    [SerializeField] private Vector2 inputValues;
    [SerializeField] private float moveSpeed = 10f;
    private void Start()
    {
        EnableCursorState(isCursorStateEnabled);
    }

    private void OnEnable()
    {
        gameControls = new GameControls();
        gameControls.GameActionMap.Enable();
    }

    private void OnDisable()
    {
        gameControls.GameActionMap.Disable();
    }

    private void Update()
    {
        HandlePlayerInput();
        HandlePlayerRotation();
    }

    private void HandlePlayerInput()
    {
        inputValues = gameControls.GameActionMap.Move.ReadValue<Vector2>();
        transform.Translate(new Vector3(inputValues.x, 0, inputValues.y) * moveSpeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isCursorStateEnabled = !isCursorStateEnabled;
            EnableCursorState(isCursorStateEnabled);
        }
    }

    private void HandlePlayerRotation()
    {
        transform.rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
    }

    private void EnableCursorState(bool enable)
    {
        if (enable)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
