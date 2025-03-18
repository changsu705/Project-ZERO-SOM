using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public int damage = 10; // 투사체 공격력
    public Transform firePoint;
    public float projectileSpeed = 10f;

    private void OnTriggerEnter(Collider other)
    {
        Unit unit = other.GetComponent<Unit>();
        if (unit != null) // Unit 클래스를 상속받은 오브젝트인지 확인
        {
            unit.TakeDamage(damage);
            Destroy(gameObject); // 투사체 제거
        }
    }
}
