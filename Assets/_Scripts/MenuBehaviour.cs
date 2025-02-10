using UnityEngine;

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

    
}
