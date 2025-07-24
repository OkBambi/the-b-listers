using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ChainUIController : MonoBehaviour
{
    public RawImage chainLockImage;
    public VideoPlayer lockVideoPlayer;
    public string lockVideoFile = "ChainLock.mp4";

    public RawImage chainUnlockImage;
    public VideoPlayer unlockVideoPlayer;
    public string unlockVideoFile = "ChainUnlock.mp4";
    void Awake()
    {
        ChainEvents.OnChainLock += PlayLockVideo;
        ChainEvents.OnChainUnlock += PlayUnlockVideo;

        string basePath = Application.streamingAssetsPath;
#if UNITY_WEBGL
        lockVideoPlayer.url = $"{basePath}/{lockVideoFile}";
        unlockVideoPlayer.url = $"{basePath}/{unlockVideoFile}";
#else
lockVideoPlayer.url = System.IO.Path.Combine(basePath, lockVideoFile);
unlockVideoPlayer.url = System.IO.Path.Combine(basePath, unlockVideoFile);

#endif
        chainLockImage.gameObject.SetActive(false);
        chainUnlockImage.gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        ChainEvents.OnChainLock -= PlayLockVideo;
        ChainEvents.OnChainUnlock -= PlayUnlockVideo;
    }
    public void PlayLockVideo()
    {
        chainUnlockImage.gameObject.SetActive(false);
        chainLockImage.gameObject.SetActive(true);
        lockVideoPlayer.Play();
    }

    public void PlayUnlockVideo()
    {
        chainUnlockImage.gameObject.SetActive(true);
        chainLockImage.gameObject.SetActive(false);
        unlockVideoPlayer.Play();
    }
}
