using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string UnitName = "UnitName";
    public int maxHealth = 100; // ü��
    public int health;
    public int attackPower = 10; // ���ݷ�

    public virtual  void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }
    public virtual void Die()
    {
        Debug.Log(UnitName + "��(��) ���������ϴ�.");
        Destroy(gameObject);
    }

}
