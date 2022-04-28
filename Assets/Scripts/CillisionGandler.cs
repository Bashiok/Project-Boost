using UnityEngine;
using UnityEngine.SceneManagement;

public class CillisionGandler : MonoBehaviour
{

    [SerializeField] float nextLevelDelayTiem = 2f;
    [SerializeField] AudioClip crasheSound;
    [SerializeField] AudioClip success;
    AudioSource audioSource;

        // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other) 
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You Hit with Friendly object");
                break;

            case "Finish":
                Debug.Log("You Hit with Finishe object");
                NextLevelDelay();
                break;
            default:
                Debug.Log("End Game");
                StartCrasheSequence();
                break;
        }
    }

    void NextLevelDelay()
    {            
        GetComponent<Movment>().enabled = false;
        Invoke("LoadNextLevel", nextLevelDelayTiem);
        SondNextLevel();
    }

    void StartCrasheSequence()
    {
        GetComponent<Movment>().enabled = false;
        Invoke("ReloadLevel", nextLevelDelayTiem);
        SoundCrasheEfect();
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void SoundCrasheEfect()
    {
                if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(crasheSound, 0.05f);
            }
        
        else 
        {
                audioSource.Stop();
        }

    }
    void SondNextLevel()
    {
                if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(success, 0.05f);
            }
        
        else 
        {
                audioSource.Stop();
        }

    }
}
