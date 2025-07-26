using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MatchDefaultResolution : MonoBehaviour
{
    public static MatchDefaultResolution instance;
    [SerializeField] CanvasScaler scaler;
    [SerializeField] float ratioX;
    [SerializeField] float ratioY;
    public bool isFeedMade;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        FixAllUI();
        instance = this;
    }

    public void FixAllUI()
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

       FixText();
    }

    public void FixText()
    {
        TextMeshProUGUI[] allText = FindObjectsByType<TextMeshProUGUI>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        float textRatio = (ratioX < ratioY ? ratioX : ratioY);
        foreach (TextMeshProUGUI text in allText)
        {
            text.fontSize = text.fontSize * textRatio;

        }
    }

    public void FixCombo(TextMeshProUGUI test)
    {
        float textRatio = (ratioX < ratioY ? ratioX : ratioY);
        test.fontSize = test.fontSize * textRatio;
    }

    //private void OnEnable()
    //{
    //    StartCoroutine(FixUI());
    //}

    //public IEnumerator FixUI()
    //{
    //    yield return new WaitForSecondsRealtime(2f);

}
