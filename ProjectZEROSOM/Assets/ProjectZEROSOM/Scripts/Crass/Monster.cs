using UnityEngine;

public class Monster : Unit
{
    [Header("UI ����")]
    public HealthBar healthBar;
    [Header("�и� ����")]
    public float currentParryThreshold = 4f;
    public string ParryStatus;
    public bool isparred;
    [Header("�ִϸ��̼� ����")]
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
        Debug.Log(this.name + damage + "�� �������� �޾ҽ��ϴ�.");

        base.TakeDamage(damage); // �θ� Ŭ������ TakeDamage ����

        health = Mathf.Clamp(health, 0, maxHealth);

        if (healthBar != null)
        {
            healthBar.SetHealth(health);
            Debug.Log("ü�� �� ������Ʈ: " + health);
        }
    }
    public void Attack()
    {
        Debug.Log(UnitName + "��(��) �����մϴ�! ���ݷ�: " + attackPower);
    }
    public void Parried()
    {
        if (ParryStatus == "Groggy")
        {
            Debug.Log($"{UnitName} �и� ���� �� �׷α� ����!");
            animator.SetTrigger("Groggy"); // �׷α� �ִϸ��̼� Ʈ���� ���� T����� ��µǴ� ���װ� �ִ� �Ƹ� �޸ӳ��̵� ���� �����ε� �ϴ�
        }
        else if (ParryStatus == "Block")
        {
            Debug.Log($"{UnitName} �и� ���� �� ���� ó�� (idle)");
            animator.SetTrigger("Idle"); // idle ���� ���Ϳ� Ʈ����
        }
    }
   
}
