using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScGetInput : MonoBehaviour{
    private ScView viewScript;
    private ScMovement moveScript;
    private ScCurrentWeapon currentWeaponScript;
    private PlayerInput playerInput;
    private Vector2 moveDirection;
    private GameObject weapon;


    private void Awake(){
        viewScript = GetComponent<ScView>();
        playerInput = GetComponent<PlayerInput>();
        moveScript = GetComponent<ScMovement>();
        currentWeaponScript = GetComponent<ScCurrentWeapon>();
    }
    private void FixedUpdate(){
        moveScript.Move(moveDirection);
    }

    public void GetMouseValue(InputAction.CallbackContext ctx){
        viewScript.LookAround(ctx.ReadValue<Vector2>());
    }

    public void GetDirInput(InputAction.CallbackContext ctx){
        if (ctx.performed) { moveDirection = ctx.ReadValue<Vector2>(); }
        if (ctx.canceled) { moveDirection = Vector2.zero; }
    }

    public void GetJumpInput(InputAction.CallbackContext ctx) {
        if (ctx.performed) { moveScript.Jump();  }
    }

    public void CurrentWeapon(){
        weapon = currentWeaponScript.ActualWeapon();
    }

/*    public void GetShootInput(InputAction.CallbackContext ctx) {  
        if (weapon != null){
            if (ctx.started) { weapon.GetComponent<>; }
            if (ctx.canceled) { weapon.StopShoot(); }
        } }*/

}