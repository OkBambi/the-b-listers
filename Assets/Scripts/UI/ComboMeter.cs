using UnityEngine;
using TMPro;

public class ComboMeter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI comboGradeText;

    void Start()
    {
        UpdateGrade();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            UpdateGrade();
        }
    }

    void UpdateGrade()
    {
        comboGradeText.text = "<size=130><color=#FF0000>P</size></color><size=90><color=black>RIMARY</size></color>";
    }
}
