using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "LevelModifier", menuName = "Level Modifier", order = 1)]

public class LevelModifier : ScriptableObject
{
    [Tooltip("This field determines in what difficulty the modifier will appear")]
    [SerializeField] DifficultyObject.DifficultyType difficulty;
    [SerializeField] Image modifierIcon;
    [TextArea] [SerializeField] string description;

}
