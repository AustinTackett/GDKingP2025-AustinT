using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void goToGame() 
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainGame");
    }

    public void goToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void goToCharacterSelection()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterSelection");
    }


    public void restartGameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
