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

    void Navigate(Vector2 direction)
    {
        // Get the currently selected button
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(selectedObject);
        }

        // Find the next selectable object in the given direction
        var current = EventSystem.current.currentSelectedGameObject;
        if (current != null)
        {
            var next = FindNextSelectable(current, direction);
            if (next != null)
            {
                EventSystem.current.SetSelectedGameObject(next);
                selectedObject = next;
            }
        }
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
