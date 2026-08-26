using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private Image hpImage = null;
    [SerializeField] private EnemyData enemyData = null;
    [SerializeField] private PlayerData playerData = null;

    private float maxHP;
    private float currentHP;
    private float damage;

    private void Start()
    {
        maxHP = enemyData.hp;
        currentHP = maxHP;
        damage = playerData.attackDamage;
    }

    public void MinusHP()
    {
        if(currentHP - damage <= 0)
        {
            Destroy(this.gameObject);
        }

        currentHP -= damage;
        //現在HPの割合
        float hpRate = currentHP / maxHP;

        Vector3 size = hpImage.rectTransform.localScale;
        size.x = hpRate;
        hpImage.rectTransform.localScale = size;
    }
}