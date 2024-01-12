using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScCurrentWeapon : MonoBehaviour{
    [Header("~~~~ Which Weapon ~~~~")]
    public Transform baguette;
    public Transform breadstick;
    public Transform twistedbread;
    public Transform rye;

    [Header("~~~~ Weapon States ~~~~")]
    bool canHold;
    private enum Weapons { _hand ,_baguette, _graplingHook, _grenadeLauncher, _grenade };
    [SerializeField] private Weapons currentWeapon = Weapons._hand;

    [Header("~~~~ Visual ~~~~")]
    public GameObject muzzleFlash;
    public TextMeshProUGUI munitionDisplay;
    [SerializeField] private int ballsLeft, magazine, ballsPerTaps; 

    void Start() {
        
    }

    void Update() {
        ActualWeapon();

        if (munitionDisplay != null) {
            munitionDisplay.SetText(ballsLeft / ballsPerTaps + " / " + magazine / ballsPerTaps);
        }
    }

    public ScWeapon ActualWeapon(){
        for (int u = 0; u< transform.childCount; u++) { 
            var weapon = transform.GetChild(u);
            if (weapon == baguette) { currentWeapon = Weapons._baguette; }
            if (weapon.gameObject.activeSelf && weapon.TryGetComponent<ScWeapon>(out ScWeapon weaponComponent)){
                ballsLeft = weaponComponent.bulletsLeft;
                ballsPerTaps = weaponComponent.bulletsShooting;
                magazine = weaponComponent.magazineSize;
                return weaponComponent;
            }
        }
        return null;
    }

    public void ActualMagazine(){
        
    }


}
