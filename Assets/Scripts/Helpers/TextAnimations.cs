using TMPro;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public static class TextAnimations
{
    static Mesh mesh;
    static Vector3[] vertices;
    public static void ShakeTextPerChar(TMP_Text textMesh, float shakePower)
    {
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;
        vertices = mesh.vertices;

        for (int index = 0; index < textMesh.textInfo.characterCount; index++)
        {
            TMP_CharacterInfo c = textMesh.textInfo.characterInfo[index];

            int loc = c.vertexIndex;

            Vector3 offset = Shake(shakePower);
            vertices[loc] += offset;
            vertices[loc + 1] += offset;
            vertices[loc + 2] += offset;
            vertices[loc + 3] += offset;
        }

        mesh.vertices = vertices;
        textMesh.canvasRenderer.SetMesh(mesh);
    }
    public static Vector2 Shake(float shakePower)
    {
        float _x;
        float _y;
        float shakeAmount = shakePower;

        _x = Random.Range(-1f, 1f) * shakeAmount;
        _y = Random.Range(-1f, 1f) * shakeAmount;

        return new Vector2(_x, _y);
    }
}
