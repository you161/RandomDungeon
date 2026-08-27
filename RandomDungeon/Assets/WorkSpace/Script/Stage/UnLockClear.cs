using UnityEngine;

public class UnLockClear : MonoBehaviour
{
    [SerializeField] private EnemyHP enemyHP = null;
    [SerializeField] private StageManager stageManager = null;

    private void OnEnable()
    {
        enemyHP.OnDead += UnlockClear;
    }

    private void OnDisable()
    {
        enemyHP.OnDead -= UnlockClear;
    }

    private void UnlockClear()
    {
        stageManager.UnlockClear();
    }
}