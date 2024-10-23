using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseMenuCanvas;
    private bool isPaused = false;
    public string mainMenuSceneName = "MainMenu";
    public GameObject[] pauseMenuUIElements;

    GameObject buildmanager;
    BuildMenu buildmenu;
    void Start()
    {
        pauseMenuCanvas.SetActive(false);

        buildmanager = GameObject.FindGameObjectWithTag("BuildManager");
        buildmenu = buildmanager.GetComponent<BuildMenu>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!buildmenu.towerBeingPlaced)
                TogglePauseMenu();
        }
    }

    void TogglePauseMenu()
    {
        isPaused = !isPaused;
        pauseMenuCanvas.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;

        if (isPaused)
        {
            DeactivatePauseMenuUIElements();
        }
        else
        {
            ActivatePauseMenuUIElements();
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        ActivatePauseMenuUIElements();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
        Time.timeScale = 1f;
    }

    void DeactivatePauseMenuUIElements()
    {
        foreach (GameObject uiElement in pauseMenuUIElements)
        {
            uiElement.SetActive(false);
        }
    }

    void ActivatePauseMenuUIElements()
    {
        foreach (GameObject uiElement in pauseMenuUIElements)
        {
            uiElement.SetActive(true);
        }
    }
}