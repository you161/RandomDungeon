using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;
    private bool canMove = false;
    public void Move(Vector3 destination)
    {
        if (!canMove)
        {
            return;
        }

        agent.SetDestination(destination);
    }
    public void SetCanMove(bool value)
    {
        canMove = value;
        agent.isStopped = !canMove;
    }
    public void SetMoveSpeed(float speed)
    {
        agent.speed = speed;
    }
}