using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string UnitName = "UnitName";
    public int health = 100; // 체력
    public int attackPower = 10; // 공격력

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(UnitName + "이(가) " + damage + "의 피해를 입었습니다. 남은 체력: " + health);
    }
}
