using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackCollision : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData = null;
    [SerializeField] private Transform player = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(enemyData.tagName))
        {
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