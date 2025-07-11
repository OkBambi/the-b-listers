using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    private GameObject selectedObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject FindNextSelectable(GameObject current, Vector2 direction)
    {
        Selectable currentSelectable = current.GetComponent<Selectable>();
        if (currentSelectable == null) return null;

        Selectable next = null;
        if (direction == Vector2.up)
            next = currentSelectable.FindSelectableOnUp();
        else if (direction == Vector2.down)
            next = currentSelectable.FindSelectableOnDown();
        else if (direction == Vector2.left)
            next = currentSelectable.FindSelectableOnLeft();
        else if (direction == Vector2.right)
            next = currentSelectable.FindSelectableOnRight();

        return next != null ? next.gameObject : current;
    }  
}
