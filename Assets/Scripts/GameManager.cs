using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public bool isPaused = false;
    private bool isTutorialActive = true;

    [Header("UI Panels")]
    public GameObject pausePanel;
    public GameObject tutorialPanel;

    void Start()
    {
        StartTutorial();
    }

    void Update()
    {
        ReturnMenu();
        HandleInput();
    }

    public void ReturnMenu()
    {
        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void HandleInput()
    {
        if (isTutorialActive)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                EndTutorial();
            }

            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                tutorialPanel.SetActive(false);
                isTutorialActive = false;
            }
            return;
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void StartTutorial()
    {
        Time.timeScale = 0f;
        isTutorialActive = true;
        if (tutorialPanel != null) tutorialPanel.SetActive(true);
        if (pausePanel != null) pausePanel.SetActive(false);
    }

    private void EndTutorial()
    {
        isTutorialActive = false;
        if (tutorialPanel != null) tutorialPanel.SetActive(false);
        Resume();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
        if (pausePanel != null) pausePanel.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false;
        if (pausePanel != null) pausePanel.SetActive(false);
    }
}
