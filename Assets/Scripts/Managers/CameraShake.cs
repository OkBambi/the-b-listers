using System.Collections;
using UnityEngine;
using static EasingLibrary;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;
        float _x;
        float _y;

        while (elapsed < duration)
        {
            if (Time.timeScale == 0f) yield break;
            _x = Random.Range(-1f, 1f) * magnitude;
            _y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPos + new Vector3(_x, _y, 0);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originalPos;
    }

    public IEnumerator ShakeTween(float duration, float magnitude, float easeDestination, float easeSpeed)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;
        float _x;
        float _y;

        while (elapsed < duration)
        {
            if (Time.timeScale == 0f) yield break;
            _x = Random.Range(-1f, 1f) * magnitude;
            _y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPos + new Vector3(_x, _y, 0);

            elapsed += Time.deltaTime;

            magnitude = EaseInCubic(magnitude, easeDestination, easeSpeed);

            yield return null;
        }
        transform.localPosition = originalPos;
    }
}
