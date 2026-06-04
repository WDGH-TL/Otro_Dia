using UnityEngine;


[System.Serializable]
public struct OpcionRespuesta
{
    public string textoOpcion;
    public int indiceDestino;
}

[CreateAssetMenu(fileName = "Textos", menuName = "Scriptable Objects/Textos")]
public class Textos : ScriptableObject
{
    [TextArea(5, 10)]
    public string textoNarrativo;

    
    public OpcionRespuesta[] opciones;

    public bool esFinal;
}