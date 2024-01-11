using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScCurrentWeapon : MonoBehaviour{
    [Header("~~~~Which Weapon~~~~")]
    public GameObject baguette;
    public GameObject graplingHook;
    public GameObject grenadeLauncher;
    public GameObject grenade;

    [Header("~~~~Weapon States~~~~")]
    bool canHold;
    private enum Weapons { _nothing ,_baguette, _graplingHook, _grenadeLauncher, _grenade };
    [SerializeField] private Weapons currentWeapon = Weapons._nothing;

    void Start() {
        
    }

    void Update(){
        
    }

    public GameObject ActualWeapon(){
        return baguette;
    }
}
