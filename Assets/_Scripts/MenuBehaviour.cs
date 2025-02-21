using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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
        StartCoroutine(WaitForSoundAndTransition("MainGame"));
    }

    public IEnumerator WaitForSoundAndTransition(string sceneName)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
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
