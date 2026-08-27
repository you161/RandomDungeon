using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData = null;
    [SerializeField] private EnemyMove enemyMove = null;
    [SerializeField] private StagePathFinder stagePathFinder = null;
    [SerializeField] private NavMeshAgent agent = null;
    private Vector3 destination;
    private float waitTimer;
    private bool isWaiting;
    private bool canPatrol = false;

    private void Start()
    {
        SetNextDestination();
    }

    private void Update()
    {
        if (!canPatrol)
        {
            return;
        }

        if (isWaiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= enemyData.waitTime)
            {
                isWaiting = false;
                waitTimer = 0f;

                SetNextDestination();
            }

            return;
        }

        enemyMove.SetCanMove(true);
        enemyMove.Move(destination);

        //目的地に到着
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            enemyMove.SetCanMove(false);
            isWaiting = true;
        }
    }
    private void SetNextDestination()
    {
        destination = stagePathFinder.GetRandomPatrolDestination(transform.position);
    }
    public void SetPatrol(bool value)
    {
        canPatrol = value;

        if (!canPatrol)
        {
            enemyMove.SetCanMove(false);
        }
    }
}