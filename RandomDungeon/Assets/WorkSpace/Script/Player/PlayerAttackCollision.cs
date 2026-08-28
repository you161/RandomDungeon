using UnityEngine;

public class PlayerAttackCollision : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData = null;
    [SerializeField] private PlayerAttack playerAttack = null;
    [SerializeField] private Transform player = null;

    private void OnTriggerEnter(Collider other)
    {
        if (!playerAttack.GetIsHitCool() && other.gameObject.CompareTag(enemyData.tagName))
        {
            playerAttack.StartHitCool();

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