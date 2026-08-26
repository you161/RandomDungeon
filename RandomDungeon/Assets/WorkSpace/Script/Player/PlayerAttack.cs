using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerData playerData = null;
    [SerializeField] private GameObject attackCollision = null;

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
        if (countCoolTime >= playerData.coolTime)
        {
            countCoolTime = 0;
            isCool = false;
        }
    }
    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }
}
