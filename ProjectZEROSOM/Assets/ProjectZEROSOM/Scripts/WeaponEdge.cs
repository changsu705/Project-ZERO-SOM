using UnityEngine;

public class WeaponEdge : MonoBehaviour
{
    public float damageMultiplier = 10f; // �ӵ��� ���� ������ ����
    public float minDamageSpeed = 2f; // �ּ� �������� �ֱ� ���� �ӵ� �Ӱ谪

    private void OnCollisionEnter(Collision collision)
    {
        // �浹�� ����� Monster Ŭ������ ���� ���
        Monster monster = collision.gameObject.GetComponent<Monster>();
        if (monster != null)
        {
            // ��� �ӵ��� ����Ͽ� ������ ����
            float impactSpeed = collision.relativeVelocity.magnitude;

            // ���� �ӵ� �̻��� ���� ������ ����
            if (impactSpeed >= minDamageSpeed)
            {
                int damage = Mathf.RoundToInt(impactSpeed * damageMultiplier);
                monster.TakeDamage(damage);

                Debug.Log($"[DamageObject] {monster.UnitName}���� {damage} �������� �������ϴ�! (�ӵ�: {impactSpeed})");
            }
        }
    }
}
