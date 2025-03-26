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
    public bool isAttacking = false;



    public void ChangePattern(string patternName)
    {
        switch (patternName)
        {
            case "Slash":
                currentParryThreshold = 1f;
                ParryStatus = "Block";
                break;
            case "CastSlash":
                currentParryThreshold = 2f;
                ParryStatus = "Groggy";
                break;
        }
        // 애니메이션 트리거 이름 설정
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
