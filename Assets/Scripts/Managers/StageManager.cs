using UnityEngine;

public class StageManager : MonoBehaviour
{

    [SerializeField] GameObject stage;
    [SerializeField] float Scaler;

    Vector3 originalStageSize;

   
    void Start()
    {
        if (stage != null)
        {
            originalStageSize = stage.transform.localScale;

            if (LevelModifierManager.instance.largerStage)
            {
                Vector3 Scaled = originalStageSize;
                Scaled.x *= Scaler;
                Scaled.z *= Scaler;

                stage.transform.localScale = Scaled;
          }
        }
    }

}
