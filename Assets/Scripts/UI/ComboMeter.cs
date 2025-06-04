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
        comboGradeText.text = "<size=0.7><color=#FF0000>P</size></color><size=0.3><color=black>RIMARY</size></color>";
    }
}
