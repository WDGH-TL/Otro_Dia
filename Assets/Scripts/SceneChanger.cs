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
}
