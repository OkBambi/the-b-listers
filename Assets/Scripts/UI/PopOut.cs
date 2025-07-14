using UnityEngine;
using UnityEngine.EventSystems;

public class PopOut : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Vector3 NormalPosition;
    [SerializeField] Vector3 HoverPosition;

    void Awake()
    {
        //NormalPosition = transform.position;
        //HoverPosition = NormalPosition + new Vector3(0f, 80f, 0f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //moves the onject to the hover position when the mouse pointer enters
        //transform.position = HoverPosition;
        transform.position += new Vector3(0f, 80f, 0f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //returns the object to its normal position when the mouse pointer exits
        //transform.position = NormalPosition;
        transform.position -= new Vector3(0f, 80f, 0f);
    }

    public void ReturnButton()
    {
        transform.position -= new Vector3(0f, 80f, 0f);
    }

}
