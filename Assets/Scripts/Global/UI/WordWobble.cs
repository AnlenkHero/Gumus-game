using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordWobble : MonoBehaviour
{
    [SerializeField]
    private float verticesOffset1=3.5f;
    [SerializeField]
    private float verticesOffset2=3f;
    [SerializeField]
    private float verticesOffset3=1.5f;
    [SerializeField]
    private float verticesOffset4=2f;
    [SerializeField]
    private float colorLength1=1.2f;
    [SerializeField]
    private float colorLength2=1.3f;
    [SerializeField]
    private float colorLength3=1.4f;
    [SerializeField]
    private float colorLength4=1.5f;
    TMP_Text _textMesh;

    Mesh _mesh;

    Vector3[] _vertices;

    List<int> _wordIndexes;
    List<int> _wordLengths;

    public Gradient rainbow;
    
    void Start()
    {
        _textMesh = GetComponent<TMP_Text>();

        _wordIndexes = new List<int>{0};
        _wordLengths = new List<int>();

        string s = _textMesh.text;
        for (int index = s.IndexOf(' '); index > -1; index = s.IndexOf(' ', index + 1))
        {
            _wordLengths.Add(index - _wordIndexes[_wordIndexes.Count - 1]);
            _wordIndexes.Add(index + 1);
        }
        _wordLengths.Add(s.Length - _wordIndexes[_wordIndexes.Count - 1]);
    }
    
    void Update()
    {
        _textMesh.ForceMeshUpdate();

        var textInfo = _textMesh.textInfo;
        

        for (int i = 0; i < textInfo.characterCount; ++i)
        {
            var charInfo = textInfo.characterInfo[i];
            if (!charInfo.isVisible)
            {
                continue;
            }

            _vertices = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;
            for (int j = 0; j < 4; ++j)
            {
                Vector3 offset = Wobble(Time.time + i);
                
                var orig = _vertices[charInfo.vertexIndex + j];
                //vertices[charInfo.vertexIndex+j]=orig+new Vector3(0,Mathf.Sin(Time.time*2f+orig.x*0.01f)*10f,0);
                _vertices[charInfo.vertexIndex + j] += offset*verticesOffset1;
                _vertices[charInfo.vertexIndex + j+1] += offset*verticesOffset2;
                _vertices[charInfo.vertexIndex + j+2] += offset*verticesOffset3;
                _vertices[charInfo.vertexIndex + j+3] += offset*verticesOffset4;

            }
        }

        for (int i = 0; i < textInfo.meshInfo.Length; ++i)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            _textMesh.UpdateGeometry(meshInfo.mesh,i);
        }
        
        _mesh = _textMesh.mesh;
        _vertices = _mesh.vertices;

        Color[] colors = _mesh.colors;

        for (int w = 0; w < _wordIndexes.Count; w++)
        {
            int wordIndex = _wordIndexes[w];
            Vector3 offset = Wobble(Time.time + w);

            for (int i = 0; i < _wordLengths[w]; i++)
            {
                TMP_CharacterInfo c = _textMesh.textInfo.characterInfo[wordIndex+i];

                int index = c.vertexIndex;

                colors[index] = rainbow.Evaluate(Mathf.Repeat(Time.time + _vertices[index].x*0.0004f, colorLength1));
                colors[index + 1] = rainbow.Evaluate(Mathf.Repeat(Time.time + _vertices[index + 1].x*0.0007f, colorLength2));
                colors[index + 2] = rainbow.Evaluate(Mathf.Repeat(Time.time + _vertices[index + 2].x*0.0007f, colorLength3));
                colors[index + 3] = rainbow.Evaluate(Mathf.Repeat(Time.time + _vertices[index + 3].x*0.0007f, colorLength4));

            }
            _mesh.colors = colors;
            _textMesh.canvasRenderer.SetMesh(_mesh);
        }
    }

    Vector2 Wobble(float time) {
        return new Vector2(Mathf.Sin(time*3.3f), Mathf.Cos(time*2.5f));
    }
}