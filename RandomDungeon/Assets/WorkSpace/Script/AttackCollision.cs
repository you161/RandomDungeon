using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    protected bool isCool = false;
    protected float countTime = 0f;
    protected float attackTime;

    protected void UpdateCoolTime()
    {
        if (!isCool)
        {
            return;
        }

        countTime += Time.deltaTime;

        if (countTime >= attackTime)
        {
            isCool = false;
            countTime = 0f;
        }
    }

    protected void StartCoolTime()
    {
        isCool = true;
    }
}