using UnityEngine;
using UnityEngine.EventSystems;

public class PopOut : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Vector3 NormalPosition;
    [SerializeField] Vector3 HoverPosition;

    private void Awake()
    {
        NormalPosition = transform.localPosition;
    }

    private void OnEnable()
    {
        ReturnButton();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localPosition += new Vector3(0f, 80f, 0f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localPosition -= new Vector3(0f, 80f, 0f);
    }

    public void ReturnButton()
    {
        transform.localPosition = NormalPosition;
    }

}
