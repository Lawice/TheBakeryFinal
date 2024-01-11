using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScWeapon : MonoBehaviour {
    [Header("~~~~Bullets~~~~")]
    [SerializeField] protected private GameObject bullet;
    [SerializeField] private float shootForce, upwardForce;
    [SerializeField] int bulletsLeft, bulletsShot;

    [Header("~~~~Guns Stats~~~~")]
    [SerializeField] private float shootingTime, spread, reloadTime, shotsTime;
    [SerializeField] private int magazineSize, bulletsShooting;
    [SerializeField] private bool canHold;
    private enum GunStatut {shooting, canShoot, relaoding};
    [SerializeField] private GunStatut gunStatut = GunStatut.canShoot;

    [Header("~~~~Reference~~~~")]
    [SerializeField] private Camera FPSCamera;
    [SerializeField] private Transform attackPoint;

    [Header("~~~~Fix~~~~")]
    public bool allowInvoke = true;

    void Awake() {
        bulletsLeft = magazineSize;
    }

    public virtual void Statut() {
        switch (gunStatut) {
            case GunStatut.canShoot:
                if (bulletsLeft > 0) {
                    bulletsShot = 0;
                    Shoot();
                }
                else { Reload(); }
                break;
            case GunStatut.shooting:
                if (bulletsShot > 0)
                {
                    bulletsShot = 0;
                    Shoot();
                }
                else {Reload(); }
                break;
        }
    }

    public virtual void Shoot(){
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

        if (allowInvoke) {
            Invoke("ResetShot", shootingTime);
            allowInvoke = false;
        }

        if (bulletsShot <bulletsShooting && bulletsLeft > 0) {
            Invoke("Shoot", shotsTime);
        } 
    }

    private void ResetShot() {
        gunStatut = GunStatut.canShoot;
        allowInvoke = true;
        
    }

    private void Reload() {
        gunStatut = GunStatut.relaoding;
        Invoke("Reloaded",reloadTime);
    }

    private void Reloaded() {
        bulletsLeft = magazineSize;
        gunStatut = GunStatut.canShoot;
    }


}
