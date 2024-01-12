using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScGetInput : MonoBehaviour{

    private ScView viewScript;
    private ScMovement moveScript;
    [SerializeField] private GameObject weaponHold;
    private ScCurrentWeapon currentWeaponScript;
    private PlayerInput playerInput;
    private Vector2 moveDirection;
    ScWeapon weapon;




    private void Awake(){
        viewScript = GetComponent<ScView>();
        playerInput = GetComponent<PlayerInput>();
        moveScript = GetComponent<ScMovement>();
        currentWeaponScript = weaponHold.GetComponent<ScCurrentWeapon>();
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


    public void GetShootInput(InputAction.CallbackContext ctx){
        if (ctx.started){
            ScWeapon weapon = currentWeaponScript.ActualWeapon();
            if (weapon){
                weapon.Statut();
            }
            else{
                Debug.Log("No weapon found.");
            }
        }
    }

    public void GetSecondaryShootInput(InputAction.CallbackContext ctx){
        if (ctx.performed) { Debug.Log("second shoot"); }
    }

    public void GetReloadInput(InputAction.CallbackContext ctx){
        if (ctx.started){
            ScWeapon weapon = currentWeaponScript.ActualWeapon();
            if (weapon){
                weapon.Reload();
            }
        }
    }
}