
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StageManager : MonoBehaviour
{

    [SerializeField] GameObject stage;
    [SerializeField] GameObject Pathway;
    [SerializeField] float Scaler;
    Vector3 originalStageSize;

    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (stage != null)
        {
            originalStageSize = stage.transform.localScale;
        }
    }
    void Update()
    {
        if (stage != null && LevelModifierManager.instance.largerStage)
        {
            Vector3 Scaled = originalStageSize;
            Scaled.x *= Scaler;
            Scaled.z *= Scaler;
            stage.transform.localScale = Scaled;
        }
    }

    public void Lvl3PathwayBlock()
    {
       if ( Pathway != null)
        {
            AudioManager.instance.Play("Floor_Crumble");
            Pathway.SetActive(false);

        }
    }
}