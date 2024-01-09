using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScWeapon : MonoBehaviour{
    [Header("~~~~Bullets~~~~")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private float shootForce, upwardForce;
    [SerializeField] int bulletsLeft, bulletsShot;

    [Header("~~~~Guns Stats~~~~")]
    [SerializeField] private float timeBetweenShooting, spread, reload, timeBetweenShots;
    [SerializeField] private int magazineSize, bulletsPerTap;
    [SerializeField] private bool allowButtonHold;
    enum GunStatut 




    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
