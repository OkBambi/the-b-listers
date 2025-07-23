using UnityEngine;
using UnityEngine.EventSystems;

public class PopOut : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Vector3 NormalPosition;
    [SerializeField] Vector3 HoverPosition;

    private void Start()
    {
        Invoke("PositionInvoke", 0.01f);
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
        transform.localPosition = NormalPosition;
    }

    public void PositionInvoke()
    {
        NormalPosition = transform.localPosition;
        ReturnButton();
    }

    public void MoveButtonForward()
    {
        transform.localPosition += new Vector3(0f, 80f, 0f);
    }
    public void MoveButtonBackward()
    {
        transform.localPosition -= new Vector3(0f, 80f, 0f);
    }

}
