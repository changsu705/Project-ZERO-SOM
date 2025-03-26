using UnityEngine;

public class Monster : Unit
{
    public HealthBar healthBar;
    public float currentParryThreshold = 4f;
    public string ParryStatus;
    public Animator animator;
    void Start()
    {
        health = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }
    public override void TakeDamage(int damage)
    {
        Debug.Log(this.name + damage + "의 데미지를 받았습니다.");

        base.TakeDamage(damage); // 부모 클래스의 TakeDamage 실행

        health = Mathf.Clamp(health, 0, maxHealth);

        if (healthBar != null)
        {
            healthBar.SetHealth(health);
            Debug.Log("체력 바 업데이트: " + health);
        }
    }
    public void Attack()
    {
        Debug.Log(UnitName + "이(가) 공격합니다! 공격력: " + attackPower);
    }
    public void Parried()
    {
        if (ParryStatus == "Groggy")
        {
            Debug.Log($"{UnitName} 패링 성공 → 그로기 진입!");
            animator.SetTrigger("Groggy"); // 그로기 애니메이션 트리거
        }
        else if (ParryStatus == "Block")
        {
            Debug.Log($"{UnitName} 패링 성공 → 막힘 처리 (idle)");
            animator.SetTrigger("Idle"); // idle 상태 복귀용 트리거
        }
    }
   
}
