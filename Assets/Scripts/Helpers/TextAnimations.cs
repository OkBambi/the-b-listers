using System;
using TMPro;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public static class TextAnimations
{
    static Mesh mesh;
    static Vector3[] vertices;
    public delegate Vector2 AnimationType(float powerAmt);
    public static void AnimateTextPerChar(TMP_Text textMesh, float powerAmt, AnimationType functionName)
    {
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;
        vertices = mesh.vertices;

        for (int index = 0; index < textMesh.textInfo.characterCount; index++)
        {
            TMP_CharacterInfo c = textMesh.textInfo.characterInfo[index];

            int loc = c.vertexIndex;

            Vector3 offset = functionName(powerAmt);
            vertices[loc] += offset;
            vertices[loc + 1] += offset;
            vertices[loc + 2] += offset;
            vertices[loc + 3] += offset;
        }

        mesh.vertices = vertices;
        textMesh.canvasRenderer.SetMesh(mesh);
    }
    public static Vector2 Shake(float powerAmt)
    {
        float _x;
        float _y;

        _x = UnityEngine.Random.Range(-1f, 1f) * powerAmt;
        _y = UnityEngine.Random.Range(-1f, 1f) * powerAmt;

        return new Vector2(_x, _y);
    }
}
