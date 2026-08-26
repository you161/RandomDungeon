using UnityEngine;

public class EnemyAttackCollision : MonoBehaviour
{
    [SerializeField] private PlayerData playerData = null;
    [SerializeField] private Transform enemy = null;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(playerData.tagName))
        {
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