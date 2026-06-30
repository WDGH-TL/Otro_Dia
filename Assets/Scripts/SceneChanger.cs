using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Starting Exploration");
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

    public void Home()
    {
        SceneManager.LoadScene("Home");
    }

    public void waiting()
    {
        SceneManager.LoadScene("Waiting For");
    }
    public void BandagindDone()
    {
        SceneManager.LoadScene("Bandaging Result");
    }

    public void Lose()
    {
        SceneManager.LoadScene("Caught or Lose");
    }
    public void Finale()
    {
        SceneManager.LoadScene("Final");
    }
    public void LastExplor()
    {
        SceneManager.LoadScene("Last Exploration");
    }
    public void Shootout()
    {
        SceneManager.LoadScene("Shootout");
    }

    public void WayHome()
    {
        SceneManager.LoadScene("On the way Home");
    }
}
