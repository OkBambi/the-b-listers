using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FOVSlider : MonoBehaviour
{
    //this is a reference to our fov slider
    public Slider fovSlider;
    //same as the slider but to the camera
    public Camera mainCamera;
    public Camera pixelCamera;

    //this can be optional but im putting this here if wanted
    //this is for the text element that will display the fov value
    //could use TMP?
    public TextMeshProUGUI fovText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //sets teh initial fov val to match the slider's val
        mainCamera.fieldOfView = fovSlider.value;

        //if adding the text
        //this updates the text initially
        if (fovText != null )
        {
            //fovText.text = "FOV: " + fovSlider.value.ToString("F0");
            fovText.text = fovSlider.value.ToString("F0");    //displays it as a whole number
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //this gets called when the slider's val changes
    public void OnFOVChanged(float newFOV)
    {
        //updates the camera's fov
        mainCamera.fieldOfView = newFOV;
        pixelCamera.fieldOfView = newFOV;

        //again, if added text, this updates the text just like the above one
        if (fovText != null )
        {
            //fovText.text = "FOV: " + newFOV.ToString("F0");
            fovText.text = newFOV.ToString("F0");
        }
    }
}
