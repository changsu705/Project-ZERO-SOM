using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public int damage = 10; // ����ü ���ݷ�
    void Start()
    {
        Destroy(gameObject, 6f); // 3�� �Ŀ� ������Ʈ ����
    }

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
