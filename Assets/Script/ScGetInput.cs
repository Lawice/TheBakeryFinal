using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScGetInput : MonoBehaviour{
    private ScView viewScript;
    private PlayerInput playerInput;

    private void Start(){
        viewScript = GetComponent<ScView>();
        playerInput = GetComponent<PlayerInput>();
    }

    public void GetMouseValue(InputAction.CallbackContext ctx){
        viewScript.LookAround(ctx.ReadValue<Vector2>());
    }
}