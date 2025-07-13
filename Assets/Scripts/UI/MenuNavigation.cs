using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    private GameObject selectedObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        // Check if this GameObject has a Button component
        Button button = GetComponent<Button>();
        if (button != null)
        {
            selectedObject = gameObject;
        }
        else
        {
            // Fallback: Find the first Button in the scene
            Button fallbackButton = GetComponentInChildren<Button>();
            if (fallbackButton != null)
            {
                selectedObject = fallbackButton.gameObject;
            }
            else
            {
                Debug.Log("No Button found in the scene.");
            }
        }

        EventSystem.current.SetSelectedGameObject(selectedObject);
    }

    // Update is called once per frame
    void Update()
    {
        // WASD Navigation
        if (Input.GetKeyDown(KeyCode.W))
        {
            Navigate(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Navigate(Vector2.down);
            Input.ResetInputAxes(); // Reset input axes to prevent multiple triggers
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Navigate(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Navigate(Vector2.right);
        }

        // Enter to select
        if (Input.GetKeyDown(KeyCode.Return) && selectedObject != null)
        {
            Button button = selectedObject.GetComponent<Button>();
            if (button != null && button.interactable)
            {
                button.onClick.Invoke();

            }
        }
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
                EventSystem.current.SetSelectedGameObject(current);
                selectedObject = current;
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

        foreach (Selectable selectable in Selectable.allSelectablesArray)
        {
            if (selectable == null || !selectable.IsInteractable())
                continue;
            // Debugging output to see all selectables
            Debug.Log($"Selectable: {selectable.gameObject.name}, Interactable: {selectable.IsInteractable()}");
        }
        //Debug.Log(currentSelectable);
        //Debug.Log(direction);
        if (next != null && next.IsInteractable())
        {
            return next.gameObject;
        }
        return current;
    }
}
