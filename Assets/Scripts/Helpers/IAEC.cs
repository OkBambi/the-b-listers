using UnityEngine;

public interface IAEC
{
    //call this when an AEC enemy spawn
    public void OnAECAwake()
    {
        EnemyManager.instance.OnAECAwake();
    }
    //call this when an AEC enemy dies
    public void OnAECDestroy()
    {
        EnemyManager.instance.OnAECDestroy();
    }
}
