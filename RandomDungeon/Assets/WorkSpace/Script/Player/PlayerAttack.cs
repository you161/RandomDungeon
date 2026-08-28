using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerAttack : AttackCollision
{
    [SerializeField] private PlayerData playerData = null;
    [SerializeField] private EnemyData enemyData = null;
    [SerializeField] private GameObject attackCollision = null;
    [SerializeField] private Image coolTimeImage = null;
    [SerializeField] protected GameObject coolTimeObject = null;

    private bool isAttack = false;
    private bool isCool = false;
    private bool isDelay = false;
    private float countAttackTime = 0;
    private float countCoolTime = 0;
    private float countDelayTime = 0;
    private bool canMove = false;
    private void Start()
    {
        isAttack = false;
        isCool = false;
        isDelay = false;
        countAttackTime = 0;
        countCoolTime = 0;
        countDelayTime = 0;
        attackCollision.SetActive(false);

        isHitCool = false;
        countHitCoolTime = 0;
        hitCoolTime = enemyData.attackTime;

        coolTimeObject.SetActive(false);
    }

    private void Update()
    {
        if (!canMove)
        {
            return;
        }

        if(Mouse.current.leftButton.wasPressedThisFrame && !EventSystem.current.IsPointerOverGameObject())
        {
            Attack();
        }

        CountDelayTime();
        CountAttackTime();
        CountCoolTime();

        UpdateCoolTime();
    }

    private void Attack()
    {
        if (!isAttack && !isCool)
        {
            isAttack = true;
            isDelay = true;
        }
    }

    private void CountDelayTime()
    {
        if (!isDelay)
        {
            return;
        }

        countDelayTime += Time.deltaTime;
        if (countDelayTime >= playerData.delayTime)
        {
            countDelayTime = 0;
            isDelay = false;
            attackCollision.SetActive(true);
        }
    }

    private void CountAttackTime()
    {
        if (!isAttack || isDelay)
        {
            return;
        }

        countAttackTime += Time.deltaTime;
        if (countAttackTime >= playerData.attackTime)
        {
            countAttackTime = 0;
            isAttack = false;
            attackCollision.SetActive(false);
            isCool = true;
        }
    }
    private void CountCoolTime()
    {
        if (!isCool)
        {
            return;
        }

        countCoolTime += Time.deltaTime;

        if (!coolTimeObject.activeSelf)
        {
            coolTimeObject.SetActive(true);
        }

        float changeSize = countCoolTime / playerData.coolTime;
        changeSize = Mathf.Clamp01(changeSize);
        Vector3 size = coolTimeImage.rectTransform.localScale;
        size.x = changeSize;

        coolTimeImage.rectTransform.localScale = size;

        if (countCoolTime >= playerData.coolTime)
        {
            countCoolTime = 0;
            isCool = false;
            coolTimeObject.SetActive(false);
        }
    }
    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }
    public void StartHitCool()
    {
        StartCoolTime();
    }
    public bool GetIsHitCool()
    {
        return isHitCool;
    }
}
