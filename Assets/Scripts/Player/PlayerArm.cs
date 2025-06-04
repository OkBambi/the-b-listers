using System.Collections;
using UnityEngine;

public class PlayerArm : MonoBehaviour
{
    [SerializeField] Renderer model;
    public IEnumerator Recoil(float duration, float magnitudeXY, float magnitudeZ)
    {
        Vector3 originalPos = new Vector3(0f, -0.5f, 0.21f);

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float _x = Random.Range(-1f, 1f) * magnitudeXY;
            float _y = Random.Range(-1f, 1f) * magnitudeXY;
            float _z = magnitudeZ;


            transform.localPosition += new Vector3(_x, _y, _z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, originalPos, 4f);
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
