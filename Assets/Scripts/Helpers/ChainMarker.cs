using UnityEngine;
public enum ChainType { Lock, Unlock}
public class ChainMarker : MonoBehaviour
{
    public ChainType chainType;

    public void AssignType(ChainType type)
    {
        chainType = type;
    }
}
