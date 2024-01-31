using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScGetInput : MonoBehaviour{

    private ScView viewScript;
    private ScMovement moveScript;
    [SerializeField] private GameObject weaponHold;
    private ScCurrentWeapon currentWeaponScript;
    [SerializeField] private GameObject menuCanva;
    [SerializeField] private bool menuOpenned;
    private ScMenu menuScript;
    private PlayerInput playerInput;
    private Vector2 moveDirection;
    ScWeapon weapon;

    private void Awake(){
        viewScript = GetComponent<ScView>();
        playerInput = GetComponent<PlayerInput>();
        moveScript = GetComponent<ScMovement>();
        currentWeaponScript = weaponHold.GetComponent<ScCurrentWeapon>();
        menuScript = menuCanva.GetComponent<ScMenu>();
    }

    private void FixedUpdate(){
        moveScript.Move(moveDirection);
    }

    public void GetMouseValue(InputAction.CallbackContext ctx){
        if (!menuOpenned){
            viewScript.LookAround(ctx.ReadValue<Vector2>());
        } 
    }

    public void GetDirInput(InputAction.CallbackContext ctx){
        if (!menuOpenned){
            if (ctx.performed) { moveDirection = ctx.ReadValue<Vector2>(); }
            if (ctx.canceled) { moveDirection = Vector2.zero; }
        }
    }

    public void GetJumpInput(InputAction.CallbackContext ctx) {
        if (!menuOpenned) { 
            if (ctx.performed) { moveScript.Jump();  }
        }
    }


    public void GetShootInput(InputAction.CallbackContext ctx){
        if (!menuOpenned) { 
            if (ctx.started){
                ScWeapon weapon = currentWeaponScript.ActualWeapon();
                if (weapon){
                    weapon.Statut();
                }
                else{
                    Debug.Log("No weapon found.");
                }
            }
            else if (ctx.canceled){
                ScWeapon weapon = currentWeaponScript.ActualWeapon();
                if (weapon){
                    weapon.CancelAutoShoot();
                }
            }
        }
    }

    public void GetSecondaryShootInput(InputAction.CallbackContext ctx){
        if (!menuOpenned){
            if (ctx.performed) { Debug.Log("second shoot"); }
        }
    }

    public void GetReloadInput(InputAction.CallbackContext ctx){
        if (!menuOpenned){
            if (ctx.started){
                ScWeapon weapon = currentWeaponScript.ActualWeapon();
                if (weapon){
                    weapon.Reload();
                }
            }
        }
    }
    
    public void GetSwitchInput(InputAction.CallbackContext ctx){
        if (!menuOpenned){
            float scroll_amount = ctx.ReadValue<float>();
            int scrolling = 0;
            if (scroll_amount > 0) {
                scrolling = 1;
                currentWeaponScript.ScrollWeapon(scrolling);
            } else if (scroll_amount < 0) {
                scrolling = -1;
                currentWeaponScript.ScrollWeapon(scrolling);
            }
        }
    }


    public void GetMenuInput(InputAction.CallbackContext ctx) {
        if (!menuOpenned) { }
        if (menuOpenned) { }
    }
}