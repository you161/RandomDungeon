using UnityEngine;

public class EnemyAttack : AttackCollision
{
    [SerializeField] private EnemyData enemyData = null;
    [SerializeField] private PlayerData playerData = null;
    [SerializeField] private GameObject attackCollision = null;
    private float countDelayTime = 0;
    private float countAttackTime = 0;
    private float countCoolTime = 0;
    private bool isAttack = false;
    private bool isDelay = false;
    private bool isCoolTime = false;
    private void Start()
    {
        countDelayTime = 0;
        countAttackTime = 0;
        countCoolTime = 0;
        isAttack = false;
        isCoolTime = false;
        attackCollision.SetActive(false);

        isHitCool = false;
        countHitCoolTime = 0;
        hitCoolTime = playerData.attackTime;
    }
    private void Update()
    {
        CountDelayTime();
        CountAttackTime();
        CountCoolTime();

        UpdateCoolTime();
    }
    public void Attack()
    {
        if (!isAttack && !isCoolTime)
        {
            isAttack = true;
            isDelay = true;
        }
    }
    private void CountDelayTime()
    {
        if (!isDelay)
        {
            return;
        }

        countDelayTime += Time.deltaTime;
        if (countDelayTime >= enemyData.delayTime)
        {
            countDelayTime = 0;
            isDelay = false;
            attackCollision.SetActive(true);
        }
    }
    private void CountAttackTime()
    {
        if(!isAttack || isDelay)
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

    public bool GetIsAttack() { return isAttack; }
    public bool GetIsDelay() { return isDelay; }

    public void StartHitCool()
    {
        StartCoolTime();
    }
    public bool GetIsHitCool()
    {
        return isHitCool;
    }
}