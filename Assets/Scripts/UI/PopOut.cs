using UnityEngine;
using UnityEngine.EventSystems;

public class PopOut : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public RectTransform rectT;
    [SerializeField] private Vector2 HoverOffset = new Vector2(0f, 80f);
    [SerializeField] Vector2 NormalPosition;
    [SerializeField] Vector2 HoverPosition;

    private void Awake()
    {
        rectT = GetComponent<RectTransform>();
        NormalPosition = rectT.anchoredPosition;
        HoverPosition = NormalPosition + HoverOffset;
    }

    //private void OnEnable()
    //{

    //}

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
        MoveButtonForward();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MoveButtonBackward();
    }

    public void ReturnButton()
    {
        //transform.localPosition = NormalPosition;
        rectT.anchoredPosition = NormalPosition;
    }

    public void PositionInvoke()
    {
        NormalPosition = transform.localPosition;
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            NormalPosition -= new Vector2(0f, 80f);
        }
        ReturnButton();
    }

    public void MoveButtonForward()
    {
        //transform.localPosition += new Vector3(0f, 80f, 0f);
        rectT.anchoredPosition = HoverPosition;
    }
    public void MoveButtonBackward()
    {
        //transform.localPosition -= new Vector3(0f, 80f, 0f);
        rectT.anchoredPosition = NormalPosition;
    }

}
