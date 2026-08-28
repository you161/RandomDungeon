using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform targetPosition = null;
    [SerializeField] private EnemyData enemyData = null;
    [SerializeField] private EnemyMove enemyMove = null;
    [SerializeField] private EnemyPatrol enemyPatrol = null;
    [SerializeField] private EnemyAttack enemyAttack = null;
    [SerializeField] private FadeManager fadeManager = null;
    private bool isKnockback = false;
    private void Start()
    {
        enemyMove.SetMoveSpeed(enemyData.enemyMoveSpeed);
    }

    private void Update()
    {
        if (fadeManager.GetIsFading())
        {
            return;
        }

        if (isKnockback)
        {
            enemyMove.SetCanMove(false);
            return;
        }
        Move();
    }
    private void Move()
    {

        float distance = Vector3.Distance(transform.position, targetPosition.position);

        if (distance <= enemyData.attackDistance)
        {
            enemyPatrol.SetPatrol(false);

            enemyMove.SetCanMove(false);
            enemyAttack.Attack();
        }
        else if (distance <= enemyData.moveDistance)
        {
            enemyPatrol.SetPatrol(false);

            enemyMove.SetCanMove(true);
            enemyMove.SetMoveSpeed(enemyData.enemyMoveSpeed);
            enemyMove.Move(targetPosition.position);
        }
        else
        {
            enemyMove.SetMoveSpeed(enemyData.enemyMoveSpeed / 2);
            enemyPatrol.SetPatrol(true);
        }
    }
    public void SetIsKnockback(bool value)
    {
        isKnockback = value;
    }
}