using UnityEngine;

public class ColorSwapping : MonoBehaviour
{

    [SerializeField] GameObject red_m2;
    [SerializeField] GameObject yellow_m2;
    [SerializeField] GameObject blue_m2;
    public void UpdateColor(ref PrimaryColor playerColor)
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerColor = PrimaryColor.RED;
            red_m2.SetActive(true);
            yellow_m2.SetActive(false);
            blue_m2.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerColor = PrimaryColor.YELLOW;
            red_m2.SetActive(false);
            yellow_m2.SetActive(true);
            blue_m2.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerColor = PrimaryColor.BLUE;
            red_m2.SetActive(false);
            yellow_m2.SetActive(false);
            blue_m2.SetActive(true);
        }
    }
}
