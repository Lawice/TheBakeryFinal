using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScWeapon : MonoBehaviour {
    [Header("~~~~ Bullets ~~~~")]
    [SerializeField] protected private GameObject bullet;
    [SerializeField] protected private float shootForce, upwardForce;
    [SerializeField] public int bulletsLeft, bulletsShot;

    [Header("~~~~ Guns Stats ~~~~")]
    [SerializeField] public string gunName;
    [SerializeField] protected private float shootingTime, spread, reloadTime, shotsTime;
    [SerializeField] public int magazineSize, bulletsShooting;
    [SerializeField] protected private bool canHold;
    private enum GunStatut {shooting, canShoot, relaoding};
    [SerializeField] private GunStatut gunStatut = GunStatut.canShoot;

    [Header("~~~~ Reference ~~~~")]
    [SerializeField] protected private Camera FPSCamera;
    [SerializeField] protected private Transform attackPoint;

    [Header("~~~~ Fix ~~~~")]
    public bool allowInvoke = true;

    [Header("~~~~ Audio & Animation ~~~~")]
    ScAudioManager audioManager;
    private Animator animator;


    void Awake() {
        bulletsLeft = magazineSize;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<ScAudioManager>();
        animator = GetComponentInParent<Animator>();
    }

    public virtual void Statut() {
        switch (gunStatut) {
            case GunStatut.canShoot:
                if (bulletsLeft > 0) { 
                    bulletsShot = 0;
                    if (canHold) { InvokeRepeating("AutoShoot", 0f, shootingTime); }
                    else { Shoot(); }
                }
                else { Reload(); }
                break;
            case GunStatut.shooting:
                if (bulletsShot > 0) {
                    bulletsShot = 0;
                    if (canHold) { InvokeRepeating("AutoShoot", 0f, shootingTime); }
                    else { Shoot(); }
                }
                else {Reload(); }
                break;
        }
    }

    public virtual void Shoot(){
        audioManager.PlayRandomSFX(audioManager.baguetteShoot);
        gunStatut = GunStatut.shooting;

        Ray shootRay = FPSCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
        RaycastHit shoothit;

        Vector3 targetPoint;
        if (Physics.Raycast(shootRay, out shoothit)) { targetPoint = shoothit.point; }
        else{ targetPoint = shootRay.GetPoint(75); }
         
        Vector3 direction = targetPoint - attackPoint.position;
        float xSpread = Random.Range(-spread, spread);
        float ySpread = Random.Range(-spread, spread);
        Vector3 directionSpread = direction + new Vector3(xSpread, ySpread, 0);
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        currentBullet.transform.forward = directionSpread.normalized;
        currentBullet.GetComponent<Rigidbody>().AddForce(directionSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce( FPSCamera.transform.up* upwardForce, ForceMode.Impulse);

        bulletsLeft--;
        bulletsShot++;

        if (allowInvoke && bulletsLeft > 0) {
            Invoke("ResetShot", shootingTime);
            allowInvoke = false;
        }

        if (bulletsShot <bulletsShooting && bulletsLeft > 0) {Invoke("Shoot", shotsTime);}
        
        if (bulletsLeft <= 0) { Reload(); }
    }

    private void ResetShot() {
        gunStatut = GunStatut.canShoot;
        allowInvoke = true;
    }

    public void Reload() {
        gunStatut = GunStatut.relaoding;
        animator.SetBool("Reloading", true);
        Invoke("Reloaded",reloadTime);
    }

    private void Reloaded() {
        bulletsLeft = magazineSize;
        gunStatut = GunStatut.canShoot;
        animator.SetBool("Reloading", false);
    }

    private void AutoShoot(){
        if (gunStatut == GunStatut.canShoot || gunStatut == GunStatut.shooting){Shoot(); }
        else{CancelInvoke("AutoShoot");}
    }

  public void CancelAutoShoot(){CancelInvoke("AutoShoot"); }

}
