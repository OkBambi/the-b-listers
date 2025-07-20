
using System.Collections;
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
        Pathway = GameObject.Find("Pathway");
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

    public IEnumerator Lvl3PathwayBlock()
    {
        AudioManager.instance.Play("Floor_Crumble");
        Pathway.SetActive(false);
        yield return new WaitForSecondsRealtime(0.01f);
    }
}