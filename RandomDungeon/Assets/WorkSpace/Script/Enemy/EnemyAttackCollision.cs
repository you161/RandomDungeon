using UnityEngine;

public class EnemyAttackCollision : AttackCollision
{
    [SerializeField] private PlayerData playerData = null;
    [SerializeField] private Transform enemy = null;
    private void Start()
    {
        isCool = false;
        countTime = 0;
        attackTime = playerData.attackTime;
    }
    private void Update()
    {
        UpdateCoolTime();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(playerData.tagName) && !isCool)
        {
            StartCoolTime();

            Knockback knockback = other.gameObject.GetComponent<Knockback>();
            if (knockback != null)
            {
                knockback.StartKnockback(enemy.transform.position,true);
            }

            PlayerHP playerHP = other.gameObject.GetComponent<PlayerHP>();
            if(playerHP != null)
            {
                playerHP.MinusHP();
            }
        }
    }
}