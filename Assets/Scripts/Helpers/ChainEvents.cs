using System;

public static class ChainEvents
{
    public static event Action OnChainLock;
    public static event Action OnChainUnlock;
    public static void LockVideoChain()
    {
        OnChainLock?.Invoke();
    }
    public static void UnlockVideoChain()
    {
        OnChainUnlock?.Invoke();
    }
}
