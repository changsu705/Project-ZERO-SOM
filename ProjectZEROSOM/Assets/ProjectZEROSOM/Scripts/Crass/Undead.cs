using UnityEngine;

public class Undead : Monster
{
    public bool canRevive = true; // ��Ȱ ���� ����

    // �𵥵� ���ʹ� ���� �� ���� Ȯ���� ��Ȱ
    protected override void Die()
    {
        if (canRevive)
        {
            Revive();
        }
        else
        {
            base.Die(); // �⺻ ������ Die() ����
        }
    }

    // ��Ȱ �޼���
    private void Revive()
    {
        //�߰��ۼ�����
    }
}

