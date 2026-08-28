using UnityEngine;
using UnityEngine.UI;

public class ReticleController : MonoBehaviour
{
    [SerializeField] private Image reticleImage = null;

    [SerializeField] private Transform player = null;
    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] private Vector3 attackSize = new(1f, 1f, 2f);
    [SerializeField] private float attackCenterDistance = 1.5f;

    private void Update()
    {
        CheckAttackRange();
    }

    private void CheckAttackRange()
    {
        //攻撃判定のサイズの半分
        Vector3 halfExtents = attackSize * 0.5f;
        Vector3 origin = player.position + player.forward * (attackCenterDistance - halfExtents.z);

        float castDistance = attackSize.z;
        bool canAttack = Physics.BoxCast(
            origin,
            halfExtents,
            player.forward,
            player.rotation,
            castDistance,
            enemyLayer
        );

        if (canAttack)
        {
            reticleImage.color = Color.red;
        }
        else
        {
            reticleImage.color = Color.white;
        }
    }
}