using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    DifficultyObject difficulty;

    public void SetDifficultyEasy()
    {
        difficulty.difficulty = DifficultyObject.DifficultyType.Easy;
    }

    public void SetDifficultyNormal()
    {
        difficulty.difficulty = DifficultyObject.DifficultyType.Normal;
    }

    public void SetDifficultyHard()
    {
        difficulty.difficulty = DifficultyObject.DifficultyType.Hard;
    }
}
