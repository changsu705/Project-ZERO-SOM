using UnityEngine;

public class WeaponEdge : MonoBehaviour
{
    public float damageMultiplier = 10f;
    public float minDamageSpeed = 2f;

    public void HandleCollision(Collision collision)
    {
        GameObject hitObject = collision.collider.gameObject;

        if (hitObject.CompareTag("MonsterWeapon"))
        {
            Monster monsterWeapon = hitObject.GetComponentInParent<Monster>();
            if (monsterWeapon != null)
            {
                if (monsterWeapon.isparred == true)
                {
                    monsterWeapon.Parried();
                    return;
                }
            }
        }
        Monster bodyMonster = collision.collider.GetComponentInParent<Monster>();
        if (bodyMonster != null)
        {
            float impactSpeed = collision.relativeVelocity.magnitude;
            if (impactSpeed >= minDamageSpeed)
            {
                int damage = Mathf.RoundToInt(impactSpeed * damageMultiplier);
                bodyMonster.TakeDamage(damage);
                Debug.Log($"[DamageObject] {bodyMonster.UnitName}���� {damage} �������� �������ϴ�! (�ӵ�: {impactSpeed})");
            }
        }
    }
}
