using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DifficultyObject", menuName = "Difficulty", order = 1)]

public class DifficultyObject : ScriptableObject
{
    public enum DifficultyType { Normal, Easy, Hard}
    public DifficultyType difficulty;

    public List<LevelModifier> easyModifiers;
    public List<LevelModifier> normalModifiers;
    public List<LevelModifier> hardModifiers;
}
