using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private Image hpImage = null;
    [SerializeField] private PlayerData playerData = null;
    [SerializeField] private EnemyData enemyData = null;
    [SerializeField] private CameraShake cameraShake = null;
    [SerializeField] private GameSceneManager gameSceneManager = null;
    private float maxHP;
    private float currentHP;
    private float damage;
    void Start()
    {
        maxHP = playerData.hp;
        currentHP = maxHP;
        damage = enemyData.attackDamage;
    }
    public void MinusHP()
    {
        StartCoroutine(cameraShake.Shake());

        if (currentHP - damage <= 0)
        {
            gameSceneManager.LoadGameOverScene();
        }

        currentHP -= damage;
        //現在HPの割合
        float hpRate = currentHP / maxHP;

        Vector3 size = hpImage.rectTransform.localScale;
        size.x = hpRate;
        hpImage.rectTransform.localScale = size;
    }
}
