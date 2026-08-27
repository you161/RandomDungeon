using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform targetPosition = null;
    [SerializeField] private EnemyData enemyData = null;
    [SerializeField] private EnemyMove enemyMove = null;
    [SerializeField] private EnemyAttack enemyAttack = null;
    private bool isKnockback = false;
    private void Start()
    {
        enemyMove.SetMoveSpeed(enemyData.enemyMoveSpeed);
    }

    private void Update()
    {
        if (isKnockback)
        {
            enemyMove.SetCanMove(false);
            return;
        }

        float distance = Vector3.Distance(transform.position, targetPosition.position);

        //攻撃できる距離
        if (distance <= enemyData.attackDistance)
        {
            enemyMove.SetCanMove(false);
            enemyAttack.Attack();
        }
        else
        {
            if (distance <= enemyData.moveDistance
                && !enemyAttack.GetIsAttack()
                && !enemyAttack.GetIsDelay())
            {
                enemyMove.SetCanMove(true);
                enemyMove.Move(targetPosition.position);
            }
            else
            {
                enemyMove.SetCanMove(false);
            }
        }
    }
    public void SetIsKnockback(bool value)
    {
        isKnockback = value;
    }
}