using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuBehaviour : MonoBehaviour
{
    public void goToGame() 
    {
        StartCoroutine(WaitForSoundAndTransition("MainGame"));
    }

    public IEnumerator WaitForSoundAndTransition(string sceneName)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        SceneManager.LoadScene(sceneName);
    }

    public void goToMenu()
    {
        StartCoroutine(WaitForSoundAndTransition("MainMenu"));
    }

    public void goToCharacterSelection()
    {
        StartCoroutine(WaitForSoundAndTransition("CharacterSelection"));
    }


    public void restartGameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
