using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackCollision : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData = null;
    [SerializeField] private Knockback knockback = null;
    [SerializeField] private Transform player = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(enemyData.tagName))
        {
            knockback.StartKnockback(player.position,false);

            EnemyHP enemyHP = other.gameObject.GetComponent<EnemyHP>();
            if(enemyHP == null)
            {
                Debug.Log("enemyHp is null in PlayerAttackCollision");
                return;
            }
            enemyHP.MinusHP();
        }
    }
}