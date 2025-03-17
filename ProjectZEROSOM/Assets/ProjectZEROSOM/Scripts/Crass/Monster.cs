using UnityEngine;

public class Monster : Unit
{
    // 몬스터가 공격하는 메서드
    public void Attack()
    {
        Debug.Log(UnitName + "이(가) 공격합니다! 공격력: " + attackPower);
    }


    // 몬스터가 죽는 메서드
    protected virtual void Die()
    {
        Debug.Log(UnitName + "이(가) 쓰러졌습니다.");
        Destroy(gameObject);
    }
}
