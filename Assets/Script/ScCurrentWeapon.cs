using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScCurrentWeapon : MonoBehaviour{
    [Header("~~~~ Which Weapon ~~~~")]
    public Transform transBaguette;
    public Transform transBreadstick;
    public Transform transTwistedbread;
    public Transform transRye;

    [Header("~~~~ Weapon States ~~~~")]
    bool canHold;
    private enum Weapons { _hand ,_baguette, _breadstick, _twistedbread, _rye };
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
            if (weapon == transBaguette) { weaponDisplay.SetText("La Baguette"); }
            if (weapon == transBreadstick) { weaponDisplay.SetText("Le Pain"); }
            if (weapon == transTwistedbread) { weaponDisplay.SetText("Le Pain Sprirale"); }
            if (weapon == transRye)  { weaponDisplay.SetText("Le Crouton"); }
            if (weapon.gameObject.activeSelf && weapon.TryGetComponent<ScWeapon>(out ScWeapon weaponComponent)){
                ballsLeft = weaponComponent.bulletsLeft;
                ballsPerTaps = weaponComponent.bulletsShooting;
                magazine = weaponComponent.magazineSize;
                return weaponComponent;
            }
        }
        return null;
    }

    public void ScrollWeapon(int scroll){
        if (scroll > 1){
            switch (currentWeapon){
                case Weapons._hand:
                    currentWeapon = Weapons._baguette;
                    Debug.Log("A");
                    break;
                case Weapons._baguette:
                    currentWeapon = Weapons._breadstick;
                    Debug.Log("B");
                    break;
                case Weapons._breadstick:
                    currentWeapon = Weapons._twistedbread;
                    Debug.Log("C");
                    break;
                case Weapons._twistedbread:
                    currentWeapon = Weapons._rye;
                    Debug.Log("D");
                    break;
                case Weapons._rye:
                    currentWeapon = Weapons._hand;
                    Debug.Log("E");
                break;
            }
        }

        if (scroll < -1){
            switch (currentWeapon){
                case Weapons._hand:
                    currentWeapon = Weapons._rye;
                    Debug.Log("E");
                    break;
                case Weapons._baguette:
                    currentWeapon = Weapons._hand;
                    Debug.Log("A");
                    break;
                case Weapons._breadstick:
                    currentWeapon = Weapons._baguette;
                    Debug.Log("B");
                    break;
                case Weapons._twistedbread:
                    currentWeapon = Weapons._breadstick;
                    Debug.Log("C");
                    break;
                case Weapons._rye:
                    currentWeapon = Weapons._twistedbread;
                    Debug.Log("D");
                break;
            }
        }
        
    }
}
