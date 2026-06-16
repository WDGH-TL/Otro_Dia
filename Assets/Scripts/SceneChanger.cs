using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Home");
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void HidingMinigame()
    {
        SceneManager.LoadScene("Hiding");
    }

    public void Exploration()
    {
        SceneManager.LoadScene("Exploration");
    }

    public void BandagingMinigame()
    {
        SceneManager.LoadScene("Bandaging");
    }
}
