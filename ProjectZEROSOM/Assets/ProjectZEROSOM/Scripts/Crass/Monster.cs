using UnityEngine;

public class Monster : MonoBehaviour
{
    public string monsterName = "MonsterName"; 
    public int health = 100; // ü��
    public int attackPower = 10; // ���ݷ�

    
    // ���Ͱ� �����ϴ� �޼���
    public void Attack()
    {
        Debug.Log(monsterName + "��(��) �����մϴ�! ���ݷ�: " + attackPower);
    }

    // ���Ͱ� ���ظ� �޴� �޼���
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(monsterName + "��(��) " + damage + "�� ���ظ� �Ծ����ϴ�. ���� ü��: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    // ���Ͱ� �״� �޼���
    protected virtual void Die()
    {
        Debug.Log(monsterName + "��(��) ���������ϴ�.");
        Destroy(gameObject);
    }
}
