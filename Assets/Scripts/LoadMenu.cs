using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadMenu : MonoBehaviour
{
    public void Load()
    {
        SceneManager.LoadScene("Menu");
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
