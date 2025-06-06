using UnityEngine;

public interface IAEC
{
    //call this when an AEC enemy spawn
    void OnAECAwake()
    {
        EnemyManager.instance.OnAECAwake();
    }
    //call this when an AEC enemy dies
    void OnAECDestroy()
    {
        EnemyManager.instance.OnAECDestroy();
    }
}
