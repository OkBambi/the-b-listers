using UnityEngine;

public class ColorSwapping : MonoBehaviour
{
    public void UpdateColor(ref PrimaryColor playerColor)
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerColor = PrimaryColor.RED;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerColor = PrimaryColor.YELLOW;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerColor = PrimaryColor.BLUE;
        }
    }
}
