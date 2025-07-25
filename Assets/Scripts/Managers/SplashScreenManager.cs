using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup Logo;
    [SerializeField] private float fadeIn;
    [SerializeField] private float displayDuration;
    [SerializeField] private float fadeOut;

    [SerializeField] private string LaunchMainMenu = "MainMenu";

    private void Awake()
    {
        Logo.alpha = 0f;
    }
    void Start()
    {
        StartCoroutine(SplashScreenSequence());
    }

    private IEnumerator SplashScreenSequence()
    {
        yield return Fade(Logo, 0f, 1f, fadeIn);
        AudioManager.instance.Play("Splash_Screen");
        yield return new WaitForSeconds(displayDuration);
        yield return Fade(Logo, 1f, 0f, fadeOut);
        SceneManager.LoadScene(LaunchMainMenu);
    }

    private IEnumerator Fade(CanvasGroup graphic, float from, float to, float duration)
    {
        float elapsed = 0f;
        Logo.alpha = from;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Logo.alpha = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }
        Logo.alpha = to;
    }
}
