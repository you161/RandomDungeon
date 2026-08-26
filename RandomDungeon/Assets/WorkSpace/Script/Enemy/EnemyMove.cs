using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private Transform targetPosition = null;
    [SerializeField] private EnemyData enemyData = null;
    [SerializeField] private NavMeshAgent agent = null;

    private bool canMove = false;

    private void Start()
    {
        agent.speed = enemyData.moveSpeed;
    }

    private void Update()
    {
        if (!canMove)
        {
            return;
        }

        agent.SetDestination(targetPosition.position);
    }
    public void SetCanMove(bool value)
    {
        canMove = value;
    }
}