using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapons arguments")]
    [SerializeField] float fireRate;

    public Transform firePoint;
    public GameObject bulletPref;

    private float timeToFire = 0;

    public void Fire()
    {
        if (fireRate == 0)
        {
            return;
        }
        if (Time.time >= timeToFire)
        {
            Instantiate(bulletPref, firePoint.position, firePoint.rotation);
            timeToFire = Time.time + 1 / fireRate;
        }
    }
}
