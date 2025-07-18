using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class TextAnimations
{
    static Mesh mesh;
    static Vector3[] vertices;
    static TMP_Text textMesh;
    static List<int> wordIndex;
    static List<int> wordLength;
    public delegate Vector3 AnimationName(float powerAmt);
    public delegate void AnimationStyle(float powerAmt, AnimationName animationName);

    public static void AnimateText(TMP_Text textField, float powerAmt, AnimationStyle animationType, AnimationName animationName)
    {
        textMesh = textField;
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;
        vertices = mesh.vertices;

        animationType(powerAmt,animationName);

        mesh.vertices = vertices;
        textMesh.canvasRenderer.SetMesh(mesh);
    }
    #region AnimationTypes
    public static void PerTextField(float powerAmt, AnimationName animationName)
    {
        for (int index = 0; index < vertices.Length; index++)
        {
            Vector3 offset = animationName(powerAmt);
            
            vertices[index] = vertices[index] + offset;
        }
    }

    public static void PerWord(float powerAmt, AnimationName animationName)
    {
        wordIndex = new List<int> {0};
        wordLength = new List<int>();

        string textField = textMesh.text;
        for(int index = textField.IndexOf(' '); index > -1; index = textField.IndexOf(' ', index + 1))
        {
            wordLength.Add(index - wordIndex[wordIndex.Count - 1]);
            wordIndex.Add(index + 1);
        }
        wordLength.Add(textField.Length - wordIndex[wordIndex.Count - 1]);

        for (int wordLoc = 0; wordLoc < wordIndex.Count; wordLoc++)
        {
            Vector3 offset = animationName(powerAmt);

            for (int charLoc = 0; charLoc < wordLength[wordLoc]; charLoc++)
            {
                TMP_CharacterInfo character = textMesh.textInfo.characterInfo[wordIndex[wordLoc] + charLoc];
                int index = character.vertexIndex;
                vertices[index] += offset;
                vertices[index + 1] += offset;
                vertices[index + 2] += offset;
                vertices[index + 3] += offset;
            }
        }
    }

    public static void PerChar(float powerAmt, AnimationName animationName)
    {
        for (int index = 0; index < textMesh.textInfo.characterCount; index++)
        {
            TMP_CharacterInfo character = textMesh.textInfo.characterInfo[index];

            int loc = character.vertexIndex;

            Vector3 offset = animationName(powerAmt);
            vertices[loc] += offset;
            vertices[loc + 1] += offset;
            vertices[loc + 2] += offset;
            vertices[loc + 3] += offset;
        }
    }
    #endregion

    #region Animations
    public static Vector2 Shake(float powerAmt)
    {
        float _x;
        float _y;

        _x = UnityEngine.Random.Range(-1f, 1f) * powerAmt;
        _y = UnityEngine.Random.Range(-1f, 1f) * powerAmt;

        return new Vector2(_x, _y);
    }

    public static Vector2 Wobble(float powerAmt)
    {
        return new Vector2(Mathf.Sin(Time.deltaTime * powerAmt), Mathf.Cos(Time.deltaTime * (powerAmt - 0.2f)));
    }

    
    #endregion
}
