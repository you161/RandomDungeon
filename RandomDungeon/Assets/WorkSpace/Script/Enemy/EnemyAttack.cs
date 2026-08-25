using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData = null;
    [SerializeField] private GameObject attackCollision = null;
    private float countAttackTime = 0;
    private float countCoolTime = 0;
    private bool isAttack = false;
    private bool isCoolTime = false;
    private void Start()
    {
        countAttackTime = 0;
        countCoolTime = 0;
        isAttack = false;
        isCoolTime = false;
        attackCollision.SetActive(false);
    }
    private void Update()
    {
        CountAttackTime();
        CountCoolTime();
    }
    public void Attack()
    {
        if (!isAttack && !isCoolTime)
        {
            isAttack = true;
            attackCollision.SetActive(true);
        }
    }
    private void CountAttackTime()
    {
        if(!isAttack)
        {
            return;
        }

        countAttackTime += Time.deltaTime;
        if(countAttackTime >= enemyData.attackTime)
        {
            isAttack = false;
            isCoolTime = true;
            countAttackTime = 0;
            attackCollision.SetActive(false);
        }
    }
    private void CountCoolTime()
    {
        if(!isCoolTime)
        {
            return;
        }

        countCoolTime += Time.deltaTime;
        if(countCoolTime >= enemyData.coolTime)
        {
            isCoolTime = false;
            countCoolTime = 0;
        }
    }
}