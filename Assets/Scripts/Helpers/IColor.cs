using UnityEngine;

public interface IColor
{


    enum primaryColor
    {
        RED,
        YELLOW,
        BLUE,
        OMNI
    }

    void SetColor(primaryColor myColor);
}
