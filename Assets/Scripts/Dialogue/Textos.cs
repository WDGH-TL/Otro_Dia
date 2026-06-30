using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public struct OpcionRespuesta
{ 
    public string textoOpcion;
    public int indiceDestino;
    public Image spritePersonaje;
}


[CreateAssetMenu(fileName = "Textos", menuName = "Scriptable Objects/Textos")]
public class Textos : ScriptableObject
{
    public string nombre;
    public Sprite characterSprite;
    [TextArea(5, 10)]
    public string textoNarrativo;

    
    public OpcionRespuesta[] opciones;
    public AudioClip audioClip;

    public bool esFinal;
    public bool hasDestinoNext;
    public bool isDecision;
    public int indiceDestinoNext;
    public bool goToLose;
}