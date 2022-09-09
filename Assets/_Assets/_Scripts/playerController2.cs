using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController2 : MonoBehaviour
{
    PlayerControls controls;
    Vector2 move;
    public float speed = 10f;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Fire.performed += context => SendMessage();
        controls.Player.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => move = Vector2.zero;
    }
    private void OnEnable()
    {
        controls.Player.Enable();
    }
    private void OnDisable()
    {
        controls.Player.Disable();
    }

    void SendMessage()
    {
        Debug.Log("X is pressed");
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(move.x, move.y) * speed * Time.deltaTime;
        transform.Translate(movement, Space.World);
    }
}
