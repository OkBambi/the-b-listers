using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textForTimer;
    [SerializeField] TextMeshProUGUI fadeInText;
    [SerializeField] TextMeshProUGUI fadeOutText;

    [SerializeField] float timeRemainingInSeconds;

    public bool isCounting;
    public float fadeMovementSpeed = 100;
    int minutes;
    int seconds;
    bool isFading;
    bool isColorSet;

    TMP_Text timerTextMesh;
    TMP_Text fadeInTextMesh;
    TMP_Text fadeOutTextMesh;
    Mesh mesh;
    Vector3[] vertices;

    private void Start()
    {
        timerTextMesh = textForTimer.GetComponent<TMP_Text>();
        fadeInTextMesh = fadeInText.GetComponent<TMP_Text>();
        fadeOutTextMesh = fadeOutText.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCounting) return;

        if (timeRemainingInSeconds > 1)
        {
            timeRemainingInSeconds -= Time.deltaTime;
        }
        else
        {
            timeRemainingInSeconds = 0;
            GameManager.instance.OnEndCondition();
        }

        minutes = Mathf.FloorToInt(timeRemainingInSeconds / 60);
        seconds = Mathf.FloorToInt(timeRemainingInSeconds % 60);


        if (timeRemainingInSeconds <= 11)
        {
            ShakeTextPerChar(timerTextMesh);
            ShakeTextPerChar(fadeInTextMesh);
            ShakeTextPerChar(fadeOutTextMesh);
        }

        if (timeRemainingInSeconds <= 21)
        {
            ChangeColors();
        }

        if (!isFading)
        {
            StartCoroutine(FadeInEffect());
            StartCoroutine(FadeOutEffect());
            textForTimer.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
        else
        {
            fadeInText.transform.position = Vector3.MoveTowards(fadeInText.transform.position, textForTimer.transform.position, fadeMovementSpeed * Time.deltaTime);
            fadeOutText.transform.position = Vector3.MoveTowards(fadeOutText.transform.position, fadeOutText.transform.position - new Vector3(0,fadeMovementSpeed, 0), fadeMovementSpeed * Time.deltaTime);
        }
        


    }

    IEnumerator FadeInEffect()//look to see if you make fade in and fade out work in the same fuction
    {
        isFading = true;
        int displaySeconds = (seconds - 1) % 10;

        if (seconds == 0)
        {
            fadeInText.text = string.Format("{0:0}<alpha=#00>:<alpha=#FF>{1:00}", minutes - 1, 59);
        }
        else if (displaySeconds == 0)
        {
            fadeInText.text = string.Format("<alpha=#00>{0:0}:<alpha=#FF>{1:00}", minutes, seconds - 1);
        }
        else
        {
            fadeInText.text = string.Format("<alpha=#00>{0:0}:{1:0<alpha=#FF>0}", minutes, displaySeconds);
        }

        fadeInText.CrossFadeAlpha(255f, 1f, false);
        yield return new WaitForSeconds(1);
        fadeInText.CrossFadeAlpha(1f, 0f, false);
        fadeInText.transform.position = fadeInText.transform.position + new Vector3(0f, fadeMovementSpeed, 0f);
        isFading = false;
    }
    IEnumerator FadeOutEffect()
    {
        int displaySeconds = (seconds + 1) % 10;

        if (seconds + 1 == 60)
        {
            fadeOutText.text = string.Format("{0:0}<alpha=#00>:<alpha=#FF>{1:00}", minutes + 1, 0);
        }
        else if (displaySeconds == 0)
        {
            fadeOutText.text = string.Format("<alpha=#00>{0:0}:<alpha=#FF>{1:00}", minutes, seconds + 1);
        }
        else
        {
            fadeOutText.text = string.Format("<alpha=#00>{0:0}:{1:0<alpha=#FF>0}", minutes, displaySeconds);
        }

        fadeOutText.CrossFadeAlpha(1f, 1f, false);
        yield return new WaitForSeconds(1);
        fadeOutText.CrossFadeAlpha(255f, 0f, false);
        fadeOutText.transform.position = fadeOutText.transform.position + new Vector3(0f, fadeMovementSpeed, 0f);
    }

    private void ChangeColors()
    {
        if (!isColorSet)
        {
            isColorSet = true;
            textForTimer.CrossFadeColor(Color.red, 10f, false, false);
            fadeInText.color = new Color(Color.red.r, Color.red.g, Color.red.b, fadeInText.alpha);
        }
        var newColor = textForTimer.canvasRenderer.GetColor();
        fadeOutText.color = new Color(newColor.r, newColor.g, newColor.b, fadeOutText.alpha);
    }

    private void ShakeTextPerChar(TMP_Text textMesh)
    {
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;
        vertices = mesh.vertices;

        for (int index = 0; index < textMesh.textInfo.characterCount; index++)
        {
            TMP_CharacterInfo c = textMesh.textInfo.characterInfo[index];

            int loc = c.vertexIndex;

            Vector3 offset = Shake(timeRemainingInSeconds);
            vertices[loc] += offset;
            vertices[loc + 1] += offset;
            vertices[loc + 2] += offset;
            vertices[loc + 3] += offset;
        }

        mesh.vertices = vertices;
        textMesh.canvasRenderer.SetMesh(mesh);
    }
    private Vector2 Shake(float timeLeft)
    {
        float _x;
        float _y;
        float shakeAmount = 10 - timeLeft;

        _x = Random.Range(-1f, 1f) * shakeAmount;
        _y = Random.Range(-1f, 1f) * shakeAmount;

        return new Vector2(_x, _y);
    }

}
