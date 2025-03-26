using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paladin : Monster
{

    private Dictionary<string, string> patternToAnimTrigger = new Dictionary<string, string>()
{
    { "Slash", "SlashAnim" },
    { "CastSlash", "CastSlashAnim" }
};
    public string currentAnimTrigger;



    public void ChangePattern(string patternName)
    {
        switch (patternName)
        {
            case "Slash":
                currentParryThreshold = 2f;
                ParryStatus = "Block";
                break;
            case "CastSlash":
                currentParryThreshold = 5f;
                ParryStatus = "Groggy";
                break;
        }
        // �ִϸ��̼� Ʈ���� �̸� ����
        if (patternToAnimTrigger.TryGetValue(patternName, out var animTrigger))
        {
            currentAnimTrigger = animTrigger;
            if (animator != null)
            {
                animator.SetTrigger(currentAnimTrigger); 
            }
        }
    }

}
