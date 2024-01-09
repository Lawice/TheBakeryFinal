using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScGetInput : MonoBehaviour{
    private ScView viewScript;
    private ScMovement moveScript;
    private PlayerInput playerInput;
    private Vector2 moveDirection;

    private void Awake(){
        viewScript = GetComponent<ScView>();
        playerInput = GetComponent<PlayerInput>();
        moveScript = GetComponent<ScMovement>();
    }

    public void GetMouseValue(InputAction.CallbackContext ctx){
        viewScript.LookAround(ctx.ReadValue<Vector2>());
    }

    private void FixedUpdate(){
        moveScript.Move(moveDirection);
    }

    public void GetDirInput(InputAction.CallbackContext ctx){
        if (ctx.performed) { moveDirection = ctx.ReadValue<Vector2>(); }
        if (ctx.canceled) { moveDirection = Vector2.zero; }
    }

}