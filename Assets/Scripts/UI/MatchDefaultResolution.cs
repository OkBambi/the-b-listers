using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MatchDefaultResolution : MonoBehaviour
{
    [SerializeField] CanvasScaler scaler;
    [SerializeField] float ratioX;
    [SerializeField] float ratioY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        scaler.referenceResolution = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);

        ratioX = scaler.referenceResolution.x / 1920f;
        ratioY = scaler.referenceResolution.y / 1080f;
        RectTransform[] allRects = FindObjectsByType<RectTransform>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);

        foreach (RectTransform rectTransform in allRects)
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x * ratioX, rectTransform.sizeDelta.y * ratioY);

            if (!rectTransform.gameObject.GetComponentInParent<Slider>(true) || rectTransform.gameObject.GetComponentInParent<Slider>(true) == rectTransform.gameObject.GetComponent<Slider>())
            {
                rectTransform.localPosition = new Vector2(rectTransform.localPosition.x * ratioX, rectTransform.localPosition.y * ratioY);
            }
            else
            {
                rectTransform.localPosition = new Vector2(rectTransform.localPosition.x, rectTransform.localPosition.y * ratioY);
            }

        }

        TextMeshProUGUI[] allText = FindObjectsByType<TextMeshProUGUI>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        float textRatio = (ratioX < ratioY ? ratioX : ratioY);
        foreach (TextMeshProUGUI text in allText)
        {
            text.fontSize = text.fontSize * textRatio;

        }
    }


    //private void OnEnable()
    //{
    //    StartCoroutine(FixUI());
    //}

    //public IEnumerator FixUI()
    //{
    //    yield return new WaitForSecondsRealtime(2f);
        
}
