using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    public HealthBar healthBar;
    void Start()
    {
        health = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }
    public override void TakeDamage(int damage)
    {
        Debug.Log("�÷��̾ " + damage + "�� �������� �޾ҽ��ϴ�.");

        base.TakeDamage(damage); // �θ� Ŭ������ TakeDamage ����

        health = Mathf.Clamp(health, 0, maxHealth);

        if (healthBar != null)
        {
            healthBar.SetHealth(health);
            Debug.Log("ü�� �� ������Ʈ: " + health);
        }
    }



}
