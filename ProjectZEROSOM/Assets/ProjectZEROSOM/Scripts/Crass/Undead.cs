using UnityEngine;

public class Undead : Monster
{
    public bool canRevive = true; // 부활 가능 여부

    // 언데드 몬스터는 죽을 때 일정 확률로 부활
    protected override void Die()
    {
        if (canRevive)
        {
            Revive();
        }
        else
        {
            base.Die(); // 기본 몬스터의 Die() 실행
        }
    }

    // 부활 메서드
    private void Revive()
    {
        //추가작성예정
    }
}

