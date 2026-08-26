using UnityEngine;

public class EnemyAttackCollision : MonoBehaviour
{
    [SerializeField] private PlayerData playerData = null;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(playerData.tagName))
        {
        }
    }
}