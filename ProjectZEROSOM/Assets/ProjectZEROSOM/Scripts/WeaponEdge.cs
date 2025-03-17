using UnityEngine;

public class WeaponEdge : MonoBehaviour
{
    public float damageMultiplier = 10f; // 속도에 따른 데미지 배율
    public float minDamageSpeed = 2f; // 최소 데미지를 주기 위한 속도 임계값

    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 대상이 Monster 클래스를 가진 경우
        Monster monster = collision.gameObject.GetComponent<Monster>();
        if (monster != null)
        {
            // 상대 속도를 계산하여 데미지 결정
            float impactSpeed = collision.relativeVelocity.magnitude;

            // 일정 속도 이상일 때만 데미지 적용
            if (impactSpeed >= minDamageSpeed)
            {
                int damage = Mathf.RoundToInt(impactSpeed * damageMultiplier);
                monster.TakeDamage(damage);

                Debug.Log($"[DamageObject] {monster.UnitName}에게 {damage} 데미지를 입혔습니다! (속도: {impactSpeed})");
            }
        }
    }
}
