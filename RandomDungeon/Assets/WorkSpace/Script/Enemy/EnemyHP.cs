using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private Image hpImage = null;
    [SerializeField] private EnemyData enemyData = null;
    [SerializeField] private PlayerData playerData = null;
    [SerializeField] private GameObject damageEffect = null;

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
            DamageEffect(transform.position);
            Dead();
            return;
        }

        DamageEffect(transform.position);
        OnDamage?.Invoke();

        currentHP -= damage;
        //現在HPの割合
        float hpRate = currentHP / maxHP;

        Vector3 size = hpImage.rectTransform.localScale;
        size.x = hpRate;
        hpImage.rectTransform.localScale = size;
    }
    private void DamageEffect(Vector3 pos)
    {
        GameObject effect = Instantiate(damageEffect, pos,Quaternion.identity);
        Destroy(effect,1.0f);
    }
    private void Dead()
    {
        OnDead?.Invoke();
        Destroy(gameObject);
    }
}