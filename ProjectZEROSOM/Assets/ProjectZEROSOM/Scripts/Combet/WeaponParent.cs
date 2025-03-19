using System;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"부모 오브젝트가 충돌 감지! 충돌 대상: {collision.gameObject.name}");

        // 자식 오브젝트의 충돌 처리 실행
        WeaponEdge weapon = GetComponentInChildren<WeaponEdge>();
        if (weapon != null)
        {
            weapon.HandleCollision(collision);
        }
    }
}
