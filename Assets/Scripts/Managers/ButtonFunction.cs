using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonFunction : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Toggle ArcadeToggler;
    public Toggle OutlineToggler;

    [SerializeField] GameObject MainCamera;
    [SerializeField] GameObject ArcadeCamera;

    public SkinnedMeshRenderer SkinnedMeshRenderer;
    public Material materialTexture, OutlineMaterial;

    public void onResume()
    {
        GameManager.instance.stateUnPause();
    }

    public void onRestart()
    {
        Debug.Log("Reloading State...");
        GameManager.instance.stateUnPause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void onSettings()
    {
        GameManager.instance.StateSettings();
    }

    public void onCredits()
    {
        GameManager.instance.stateUnPause();
    }

    public void ontoggleArcade(bool ison)
    { 
        if (ArcadeToggler.isOn)
        {
            Debug.Log("Filter On");
            MainCamera.SetActive(false);
            ArcadeCamera.SetActive(true);
        }
        else
        {
            Debug.Log("default Camera");
            MainCamera.SetActive(true);
            ArcadeCamera.SetActive(false);
        }
    }

    public void ontoggleOutline(bool ison)
    {
        Material[] materials = new Material[SkinnedMeshRenderer.sharedMaterials.Length];
        if (OutlineToggler.isOn)
        {
            Debug.Log("Filter On");
            materials[0] = materialTexture;
            materials[1] = OutlineMaterial;

            SkinnedMeshRenderer.sharedMaterials = materials;
        
        }
        else
        {
            Debug.Log("default Camera");
            materials[0] = materialTexture;

            SkinnedMeshRenderer.sharedMaterials = materials;
        }
    }
    public void BackButtonClick()
    {
        GameManager.instance.BackButton();
    }

  
    public void onQuit()
    {
#if !UNITY_EDITOR
Application.Quit();
#else 
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }


}
