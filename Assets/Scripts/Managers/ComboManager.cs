using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public static ComboManager instance;

    [SerializeField] float BASE_COMBO_DECAY = 20;
    [Space]
    [SerializeField] int totalScore;
    [SerializeField] int currentComboScore;
    [SerializeField] ComboGrade comboGrade;
    [SerializeField] float comboHoldTimer;
    [SerializeField] float currentMult;

    [SerializeField] List<int> comboFloors;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        CheckGrade();
    }

    void CheckGrade()
    {

    }

    public void AddScore(int amount)
    {

    }
}
public enum ComboGrade
{
    None,
    D,
    C,
    B,
    A,
    S,
    SS,
    SSS,
    P
}
