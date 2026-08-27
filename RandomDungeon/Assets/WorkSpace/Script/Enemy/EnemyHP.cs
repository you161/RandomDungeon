using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private Image hpImage = null;
    [SerializeField] private EnemyData enemyData = null;
    [SerializeField] private PlayerData playerData = null;

    private float maxHP;
    private float currentHP;
    private float damage;
    public event Action OnDead;
    public event Action OnDamage;

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
            Dead();
        }

        OnDamage?.Invoke();

        currentHP -= damage;
        //現在HPの割合
        float hpRate = currentHP / maxHP;

        Vector3 size = hpImage.rectTransform.localScale;
        size.x = hpRate;
        hpImage.rectTransform.localScale = size;
    }
    private void Dead()
    {
        OnDead?.Invoke();
        Destroy(gameObject);
    }
}