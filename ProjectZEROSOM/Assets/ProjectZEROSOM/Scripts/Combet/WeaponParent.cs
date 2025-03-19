using System;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"�θ� ������Ʈ�� �浹 ����! �浹 ���: {collision.gameObject.name}");

        // �ڽ� ������Ʈ�� �浹 ó�� ����
        WeaponEdge weapon = GetComponentInChildren<WeaponEdge>();
        if (weapon != null)
        {
            weapon.HandleCollision(collision);
        }
    }
}
