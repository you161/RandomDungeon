using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform targetPosition = null;
    [SerializeField] private EnemyData enemyData = null;
    [SerializeField] private EnemyMove enemyMove = null;
    [SerializeField] private EnemyAttack enemyAttack = null;

    private void Update()
    {
        float distance = Vector3.Distance(transform.position,targetPosition.position);

        //攻撃できる距離
        if (distance <= enemyData.attackDistance)
        {
            enemyMove.SetCanMove(false);
            enemyAttack.Attack();
        }
        else
        {
            if(distance <= enemyData.moveDistance)
            {
                enemyMove.SetCanMove(true);
            }
            else
            {
                enemyMove.SetCanMove(false);
            }
        }
    }
}