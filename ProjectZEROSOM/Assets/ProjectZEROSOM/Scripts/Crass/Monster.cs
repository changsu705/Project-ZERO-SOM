using UnityEngine;

public class Monster : MonoBehaviour
{
    public string monsterName = "MonsterName"; 
    public int health = 100; // 체력
    public int attackPower = 10; // 공격력

    
    // 몬스터가 공격하는 메서드
    public void Attack()
    {
        Debug.Log(monsterName + "이(가) 공격합니다! 공격력: " + attackPower);
    }

    // 몬스터가 피해를 받는 메서드
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(monsterName + "이(가) " + damage + "의 피해를 입었습니다. 남은 체력: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    // 몬스터가 죽는 메서드
    protected virtual void Die()
    {
        Debug.Log(monsterName + "이(가) 쓰러졌습니다.");
        Destroy(gameObject);
    }
}
