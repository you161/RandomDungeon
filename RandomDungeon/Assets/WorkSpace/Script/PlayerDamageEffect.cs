using UnityEngine;
using System.Collections;

public class PlayerDamageEffect : MonoBehaviour
{
    [Header("揺れの強さ（左右の幅）")]
    [SerializeField] private float shakeAmount = 20f;

    [Header("揺れの時間")]
    [SerializeField] private float shakeDuration = 0.2f;

    [Header("揺れの速さ")]
    [SerializeField] private float shakeSpeed = 25f;
    [SerializeField] private RectTransform rect = null;

    private Vector2 originalPos;

    private void Awake()
    {
        originalPos = rect.anchoredPosition;
    }

    public void Shake()
    {
        StopAllCoroutines();
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Mathf.Sin(elapsed * shakeSpeed) * shakeAmount;
            rect.anchoredPosition = originalPos + new Vector2(x, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        rect.anchoredPosition = originalPos;
    }
}