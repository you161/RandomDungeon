using Unity.AI.Navigation;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [Header("ステージ生成")]
    [SerializeField] private StageGenerator stageGenerator = null;
    [Header("敵生成")]
    [SerializeField] private EnemySpawner enemySpawner = null;
    [Header("NavMesh")]
    [SerializeField] private NavMeshSurface navMeshSurface = null;
    private void Start()
    {
        //ステージを生成
        stageGenerator.GenerateStage();
        //ナビメッシュを生成
        navMeshSurface.BuildNavMesh();
        //逃げる敵を生成
        enemySpawner.SpawnEscapeEnemy();
        //逃げる敵を生成してから通常の敵を生成
        enemySpawner.SpawnEnemies();
    }
}