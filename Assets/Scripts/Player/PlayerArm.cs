using System.Collections;
using UnityEngine;
using static EasingLibrary;

public class PlayerArm : MonoBehaviour
{
    [SerializeField] Renderer model;

    Vector3 defaultPos;

    private void Start()
    {
        defaultPos = transform.localPosition;
    }

    public IEnumerator Recoil(float duration, float magnitudeXY, float magnitudeZ)
    {
        float elapsed = 0.0f;
        float _x;
        float _y;
        float _z = -magnitudeZ;

        while (elapsed < duration)
        {
            if (Time.timeScale == 0f) yield break;
            _x = Random.Range(-1f, 1f) * magnitudeXY;
            _y = Random.Range(-1f, 1f) * magnitudeXY;

            transform.localPosition = defaultPos + new Vector3(_x, _y, _z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = defaultPos;
    }

    public IEnumerator RecoilTween(float duration, float magnitudeXY, float magnitudeZ, float easeSpeed)
    {
        float elapsed = 0.0f;

        float _z = 0f;
        float _x;
        float _y;

        while (elapsed < duration)
        {
            if (Time.timeScale == 0f) yield break;
            _x = Random.Range(-1f, 1f) * magnitudeXY;
            _y = Random.Range(-1f, 1f) * magnitudeXY;
            _z = EaseOutBounce(_z, -magnitudeZ, easeSpeed);


            transform.localPosition = defaultPos + new Vector3(_x, _y, _z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = defaultPos;
    }

    public void UpdateArm(PrimaryColor armColor)
    {
        switch (armColor)
        {
            case PrimaryColor.RED:
                model.material.color = Color.red;
                break;
            case PrimaryColor.YELLOW:
                model.material.color = Color.yellow;
                break;
            case PrimaryColor.BLUE:
                model.material.color = Color.blue;
                break;
            case PrimaryColor.OMNI:
            default:
                model.material.color = Color.black;
                break;
        }
    }
}
