using UnityEngine;

public class PlayerArm : MonoBehaviour
{
    [SerializeField] Renderer model;

    public void UpdateArm(PrimaryColor armColor)
    {
        switch (armColor)
        {
            case PrimaryColor.RED:
                model.material.color = Color.red;
                break;
            case PrimaryColor.YELLOW:
                model.material.color = Color.yellow;
                break;
            case PrimaryColor.BLUE:
                model.material.color = Color.blue;
                break;
            case PrimaryColor.OMNI:
            default:
                model.material.color = Color.black;
                break;
        }
    }
}
