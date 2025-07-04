using UnityEngine;
using UnityEngine.EventSystems;

public class PopOut : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //public float liftHeight = 0.5f;
    //[SerializeField] Vector3 originalPosition;
    //private bool isHovering = false;

    [SerializeField] Vector3 NormalPosition;
    [SerializeField] Vector3 HoverPosition;

    void Start()
    {
        NormalPosition = transform.position;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Move the card to the hover position when the mouse pointer enters
        transform.position = HoverPosition;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Return the card to its normal position when the mouse pointer exits
        transform.position = NormalPosition;
    }
    //void Update()
    //{
    //    if (isHovering)
    //    {
    //        Debug.Log("Mouse Over");
    //    }
    //}

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    isHovering = true;
    //    transform.Translate(Vector3.up * liftHeight);
    //    Debug.Log("Mouse enter");
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    isHovering = false;
    //    transform.Translate(Vector3.down * liftHeight);
    //    Debug.Log("Mouse exit");
    //}
}
