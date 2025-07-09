using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "LevelModifier", menuName = "Level Modifier", order = 1)]

public class LevelModifier : ScriptableObject
{
    [Tooltip("Add this modifier into the correct list in the difficulty object")]
    [SerializeField] int INFO = 0;
    [Tooltip("This is the field that will be read for processing when randomizing modifiers")]
    [SerializeField] string modifierName;
    [SerializeField] Image modifierIcon;
    [TextArea] [SerializeField] string description;

}
