using UnityEngine;
using UnityEngine.SceneManagement;

public class CillisionGandler : MonoBehaviour
{

    [SerializeField] float nextLevelDelayTiem = 2f;
    [SerializeField] AudioClip crasheSound;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem crasheSoundParticle;
    [SerializeField] ParticleSystem successParticle;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool coliisionDisabled = false;

        // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() 
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))    
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            coliisionDisabled = !coliisionDisabled;
        }
    }


    void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning || coliisionDisabled) { return; }

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
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success, 0.05f);  
        GetComponent<Movment>().enabled = false;
        successParticle.Play();
        Invoke("LoadNextLevel", nextLevelDelayTiem);
    }

    void StartCrasheSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crasheSound, 0.05f);
        GetComponent<Movment>().enabled = false;
        crasheSoundParticle.Play();
        Invoke("ReloadLevel", nextLevelDelayTiem);
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
}
