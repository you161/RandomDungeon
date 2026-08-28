using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    protected bool isHitCool = false;
    protected float countHitCoolTime = 0f;
    protected float hitCoolTime = 0f;

    protected void UpdateCoolTime()
    {
        if (!isHitCool)
        {
            return;
        }

        countHitCoolTime += Time.deltaTime;

        if (countHitCoolTime >= hitCoolTime)
        {
            isHitCool = false;
            countHitCoolTime = 0f;
        }
    }

    protected void StartCoolTime()
    {
        isHitCool = true;
    }
}