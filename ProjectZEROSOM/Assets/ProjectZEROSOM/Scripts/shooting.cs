using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public int damage = 10; // ����ü ���ݷ�
    public Transform firePoint;
    public float projectileSpeed = 10f;

    private void OnTriggerEnter(Collider other)
    {
        Unit unit = other.GetComponent<Unit>();
        if (unit != null) // Unit Ŭ������ ��ӹ��� ������Ʈ���� Ȯ��
        {
            unit.TakeDamage(damage);
            Destroy(gameObject); // ����ü ����
        }
    }
}
