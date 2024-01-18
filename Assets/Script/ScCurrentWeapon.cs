using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScCurrentWeapon : MonoBehaviour {
    [Header("~~~~ Which Weapon ~~~~")]
    public Transform transBaguette;
    public GameObject baguette;
    public Transform transBreadstick;
    public GameObject breadstick;
    public Transform transTwistedbread;
    public GameObject twistedbread;
    public Transform transRye;
    public GameObject rye;

    [Header("~~~~ Weapon States ~~~~")]
    bool canHold;
    private enum Weapons { _baguette, _breadstick, _twistedbread, _rye };
    [SerializeField] private Weapons currentWeapon = Weapons._baguette;

    [Header("~~~~ Visual ~~~~")]
    public TextMeshProUGUI munitionDisplay;
    public TextMeshProUGUI weaponDisplay;
    public TextMeshProUGUI scoreDisplay;
    public int score;
    [SerializeField] private int ballsLeft, magazine, ballsPerTaps;

    void Start() {
        currentWeapon = Weapons._baguette;
        baguette.SetActive(true); breadstick.SetActive(false); twistedbread.SetActive(false); rye.SetActive(false);
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

    public ScWeapon ActualWeapon() {
        for (int u = 0; u < transform.childCount; u++) {
            var weapon = transform.GetChild(u);
            if (weapon == transBaguette) { weaponDisplay.SetText("La Baguette"); }
            if (weapon == transBreadstick) { weaponDisplay.SetText("Le Pain"); }
            if (weapon == transTwistedbread) { weaponDisplay.SetText("Le Pain Sprirale"); }
            if (weapon == transRye) { weaponDisplay.SetText("Le Crouton"); }
            if (weapon.gameObject.activeSelf && weapon.TryGetComponent<ScWeapon>(out ScWeapon weaponComponent)) {
                ballsLeft = weaponComponent.bulletsLeft;
                ballsPerTaps = weaponComponent.bulletsShooting;
                magazine = weaponComponent.magazineSize;
                return weaponComponent;
            }
        }
        return null;
    }

    public void ScrollWeapon(int scroll) {
        if (scroll == 1) {
            switch (currentWeapon) {
                case Weapons._baguette:
                    currentWeapon = Weapons._breadstick;
                    baguette.SetActive(false); breadstick.SetActive(true); twistedbread.SetActive(false); rye.SetActive(false);
                    break;
                case Weapons._breadstick:
                    currentWeapon = Weapons._twistedbread;
                    baguette.SetActive(false); breadstick.SetActive(false); twistedbread.SetActive(true); rye.SetActive(false);
                    break;
                case Weapons._twistedbread:
                    currentWeapon = Weapons._rye;
                    baguette.SetActive(false); breadstick.SetActive(false); twistedbread.SetActive(false); rye.SetActive(true);
                    break;
                case Weapons._rye:
                    currentWeapon = Weapons._baguette;
                    baguette.SetActive(true); breadstick.SetActive(false); twistedbread.SetActive(false); rye.SetActive(false);
                    break;
            }
        }

        if (scroll == -1) {
            switch (currentWeapon) {
                case Weapons._baguette:
                    currentWeapon = Weapons._rye;
                    baguette.SetActive(false); breadstick.SetActive(false); twistedbread.SetActive(false); rye.SetActive(true);
                    break;
                case Weapons._breadstick:
                    currentWeapon = Weapons._baguette;
                    baguette.SetActive(true);  breadstick.SetActive(false); twistedbread.SetActive(false); rye.SetActive(false);
                    break;
                case Weapons._twistedbread:
                    currentWeapon = Weapons._breadstick;
                    baguette.SetActive(false);  breadstick.SetActive(true); twistedbread.SetActive(false); rye.SetActive(false);
                    break;
                case Weapons._rye:
                    currentWeapon = Weapons._twistedbread;
                    baguette.SetActive(false); breadstick.SetActive(false); twistedbread.SetActive(true); rye.SetActive(false);
                    break;
            }
        }
    }

    public void AddScore(int u) {
        score += u;
    }

}
