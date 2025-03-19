using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingDumi : Monster
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public float shootingSpeed = 1f;
    public bool isShooting = true;

    public void Shooting(float rate)
    {
        if (!isShooting) return; // 쿨타임 중이면 실행 안 함

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null & isShooting == true)
        {
            rb.velocity = firePoint.forward * projectileSpeed;
            isShooting = false;
            Invoke(nameof(ResetShooting), rate);
        }
    }

    void Update()
    {
        Shooting(3f);
    }
    private void ResetShooting()
    {
        isShooting = true;
    }

    public override void Die()
    {
        health = maxHealth;

    }
}
