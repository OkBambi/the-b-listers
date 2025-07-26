using System.Collections;
using UnityEngine;
using static EasingLibrary;

public class PlayerArm : MonoBehaviour
{
    [SerializeField] Renderer model;
    [SerializeField] Renderer Leftmodel;
    [SerializeField] Renderer QuakeModel;

    Vector3 defaultPos;

    private void Start()
    {
        defaultPos = transform.localPosition;
        QuakeModel.gameObject.SetActive(false);
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
                if (!LevelModifierManager.instance.QuakePro)
                {
                    model.material.color = Color.red;
                }
                    if (LevelModifierManager.instance.QuakePro)
                {
                    QuakeModel.material.color = Color.red;
                }
                break;
            case PrimaryColor.YELLOW:
                if (!LevelModifierManager.instance.QuakePro)
                {
                    model.material.color = Color.yellow;
                }
                if (LevelModifierManager.instance.QuakePro)
                {
                    QuakeModel.material.color = Color.yellow;
                }
                break;
            case PrimaryColor.BLUE:
                if (!LevelModifierManager.instance.QuakePro)
                {
                    model.material.color = Color.blue;
                }
                    if (LevelModifierManager.instance.QuakePro)
                {
                    QuakeModel.material.color = Color.blue;
                }
                break;
            case PrimaryColor.OMNI:
            default:
                if (!LevelModifierManager.instance.QuakePro)
                {
                    model.material.color = Color.black;
                }
                    if (LevelModifierManager.instance.QuakePro)
                {
                    QuakeModel.material.color = Color.black;
                }
                break;
        }
         Leftmodel.material.color = model.material.color;
    }

    public void QuakeGaming(bool QuakePro)
    {
        model.gameObject.SetActive(!QuakePro);
        Leftmodel.gameObject.SetActive(!QuakePro);
        QuakeModel.gameObject.SetActive(true);
    }
}
