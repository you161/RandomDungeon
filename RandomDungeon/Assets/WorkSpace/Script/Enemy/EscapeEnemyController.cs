using UnityEngine;
using UnityEngine.AI;

public class EscapeEnemyController : MonoBehaviour
{
    [SerializeField] private Transform targetPosition = null;
    [SerializeField] private EnemyMove enemyMove = null;
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private StagePathFinder stagePathFinder = null;
    [SerializeField] private EnemyData enemyData = null;
    [SerializeField] private EnemyHP enemyHP = null;
    [SerializeField] private FadeManager fadeManager = null;
    [SerializeField] private GameObject smokeEffect = null;
    [SerializeField] private GameObject goalItem = null;
    private bool isEscaping = false;
    private Vector3 escapeDestination;
    private void Start()
    {
        enemyMove.SetMoveSpeed(enemyData.escapeEnemyMoveSpeed);
        enemyHP.OnDamage += EscapeByWarp;
    }
    private void Update()
    {
        if (fadeManager.GetIsFading())
        {
            return;
        }

        Move();
    }
    private void Move()
    {
        float distance = Vector3.Distance(transform.position, targetPosition.position);

        //まだ逃げていない
        if (!isEscaping)
        {
            if (distance <= enemyData.escapeDistance)
            {
                StartEscape();
            }

            return;
        }

        //目的地に到着
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            //プレイヤーがまだ近い
            if (distance <= enemyData.escapeDistance)
            {
                StartEscape();
            }
            else
            {
                isEscaping = false;
                enemyMove.SetCanMove(false);
            }

            return;
        }

        //逃走中
        enemyMove.SetCanMove(true);
        enemyMove.Move(escapeDestination);
    }
    private void SetEscapeDestination()
    {
        escapeDestination = stagePathFinder.GetEscapeDestination(
            transform.position,
            targetPosition.position
            );
        isEscaping = true;
    }
    private void StartEscape()
    {
        SetEscapeDestination();

        enemyMove.SetCanMove(true);
        enemyMove.Move(escapeDestination);
    }
    private void EscapeByWarp()
    {
        GameObject effect = Instantiate(smokeEffect,transform.position,Quaternion.identity);
        Destroy(effect, 2.0f);

        Vector3 warpPosition =
            stagePathFinder.GetWarpDestination(
                transform.position,
                targetPosition.position
            );

        //移動を停止
        isEscaping = false;
        enemyMove.SetCanMove(false);

        if (agent.isOnNavMesh)
        {
            //現在の経路をリセット
            agent.ResetPath();
        }
        //ワープ
        agent.Warp(warpPosition);
    }
    public void SpawnGoalItem()
    {
        GameObject item = Instantiate(goalItem,transform.position,Quaternion.identity);
        item.SetActive(true);
    }
}