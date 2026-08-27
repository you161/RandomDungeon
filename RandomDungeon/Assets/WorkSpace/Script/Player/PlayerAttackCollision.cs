using UnityEngine;

public class PlayerAttackCollision : AttackCollision
{
    [SerializeField] private EnemyData enemyData = null;
    [SerializeField] private Transform player = null;

    private void Start()
    {
        isCool = false;
        countTime = 0;
        attackTime = enemyData.attackTime;
    }
    private void Update()
    {
        UpdateCoolTime();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(enemyData.tagName) && !isCool)
        {
            StartCoolTime();

            Knockback knockback = other.GetComponent<Knockback>();
            if(knockback != null)
            {
                knockback.StartKnockback(player.position, false);
            }

            EnemyHP enemyHP = other.gameObject.GetComponent<EnemyHP>();
            if(enemyHP != null)
            {
                enemyHP.MinusHP();
            }
        }
    }
}