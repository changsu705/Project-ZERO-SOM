using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string UnitName = "UnitName";
    public int health = 100; // ü��
    public int attackPower = 10; // ���ݷ�

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(UnitName + "��(��) " + damage + "�� ���ظ� �Ծ����ϴ�. ���� ü��: " + health);
    }
}
