using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float duration = 0;
    [SerializeField] private float magnitude = 0;
    private Vector3 originalPos = Vector3.zero;
    private bool isShaking = false;
    private void Awake()
    {
        originalPos = transform.localPosition;
    }

    public IEnumerator Shake()
    {
        if (isShaking)
        {
            yield break;
        }

        isShaking = true;

        originalPos = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition =
                originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
        isShaking = false;
    }
}