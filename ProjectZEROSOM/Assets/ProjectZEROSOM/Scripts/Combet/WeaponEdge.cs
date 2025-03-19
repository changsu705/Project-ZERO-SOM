using UnityEngine;

public class WeaponEdge : MonoBehaviour
{
    public float damageMultiplier = 10f;
    public float minDamageSpeed = 2f;

    public void HandleCollision(Collision collision)
    {
        Monster monster = collision.gameObject.GetComponent<Monster>();
        if (monster != null)
        {
            float impactSpeed = collision.relativeVelocity.magnitude;
            if (impactSpeed >= minDamageSpeed)
            {
                int damage = Mathf.RoundToInt(impactSpeed * damageMultiplier);
                monster.TakeDamage(damage);
                Debug.Log($"[DamageObject] {monster.UnitName}에게 {damage} 데미지를 입혔습니다! (속도: {impactSpeed})");
            }
        }
    }
}
