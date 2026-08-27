using Unity.AI.Navigation;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [Header("ステージ生成")]
    [SerializeField] private StageGenerator stageGenerator = null;

    [Header("経路探索")]
    [SerializeField] private StagePathFinder stagePathFinder = null;

    [Header("敵生成")]
    [SerializeField] private EnemySpawner enemySpawner = null;

    [Header("ステージデータ")]
    [SerializeField] private StageData stageData = null;

    [Header("ゴールアイテム")]
    [SerializeField] private GameObject goalItem = null;

    [Header("NavMesh")]
    [SerializeField] private NavMeshSurface navMeshSurface = null;
    private void Start()
    {
        //ステージを生成
        stageGenerator.GenerateStage();
        //ゴール位置を決定
        Vector2Int goalPosition = stagePathFinder.FindFarthestRoom(Vector2Int.zero);
        //ゴールアイテムを設定
        SetGoal(goalPosition);
        //ナビメッシュを生成
        navMeshSurface.BuildNavMesh();
        //逃げる敵を生成
        enemySpawner.SpawnEscapeEnemy(goalPosition);
        //逃げる敵を生成してから通常の敵を生成
        enemySpawner.SpawnEnemies(goalPosition);
        //ゴールアイテムの表示を消す
        goalItem.SetActive(false);
    }

    private void SetGoal(Vector2Int goalPosition)
    {
        goalItem.transform.localPosition =
            new Vector3(
                goalPosition.x * stageData.roomSize,
                goalItem.transform.localPosition.y,
                goalPosition.y * stageData.roomSize
            );
    }
    public void UnlockClear()
    {
        goalItem.SetActive(true);
    }
}