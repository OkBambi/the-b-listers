using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyObject", menuName = "Difficulty", order = 1)]

public class DifficultyObject : ScriptableObject
{
    public enum DifficultyType { Normal, Easy, Hard}
    public DifficultyType difficulty;
}
