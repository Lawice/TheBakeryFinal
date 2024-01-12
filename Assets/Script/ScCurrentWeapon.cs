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
    public TextMeshProUGUI munitionDisplay;
    public TextMeshProUGUI weaponDisplay;
    public TextMeshProUGUI scoreDisplay;
    public int score;
    [SerializeField] private int ballsLeft, magazine, ballsPerTaps; 

    void Start() {
        
    }

    void Update() {
        ActualWeapon();

        if (munitionDisplay != null) {
            munitionDisplay.SetText(ballsLeft / ballsPerTaps + " / " + magazine / ballsPerTaps);
        }

        if (scoreDisplay != null) {
            scoreDisplay.SetText("Score :" + score);
        }
    }

    public ScWeapon ActualWeapon(){
        for (int u = 0; u< transform.childCount; u++) { 
            var weapon = transform.GetChild(u);
            if (weapon == baguette) { weaponDisplay.SetText("La Baguette"); }
            if (weapon == breadstick) { weaponDisplay.SetText("Le Pain"); }
            if (weapon == twistedbread) { weaponDisplay.SetText("Le Pain Sprirale"); }
            if (weapon == rye)  { weaponDisplay.SetText("Le Crouton"); }
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
