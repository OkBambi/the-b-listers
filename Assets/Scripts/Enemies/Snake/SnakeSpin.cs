using UnityEngine;

public class SnakeSpin : SpinningObj
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (LevelModifierManager.instance.lowEnemyCooldowns)
            rotationSpeed = rotationSpeed * 4f;
    }

}
