using UnityEngine;

public class Monster : Unit
{
    // ���Ͱ� �����ϴ� �޼���
    public void Attack()
    {
        Debug.Log(UnitName + "��(��) �����մϴ�! ���ݷ�: " + attackPower);
    }


    // ���Ͱ� �״� �޼���
    protected virtual void Die()
    {
        Debug.Log(UnitName + "��(��) ���������ϴ�.");
        Destroy(gameObject);
    }
}
