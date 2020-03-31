using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public CanvasGroup mainMenuCanvasGroup;
    public CanvasGroup quitMenuCanvaGroup;

    public void QuitMenu()
    {
        CloseMainMenu();
        ActivateQuitMenu();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void DontQuit()
    {
        CloseQuitMenu();
        ActivateMainMenu();
    }

    public void QuitToMainMenu()
    {
        // save the player status
        StartGame.instance.SaveGame();
        SceneManager.LoadScene("SetupScene");
    }

    private void ActivateMainMenu()
    {
        mainMenuCanvasGroup.alpha = 1;
        mainMenuCanvasGroup.interactable = true;
        mainMenuCanvasGroup.blocksRaycasts = true;
    }
    private void CloseMainMenu()
    {
        mainMenuCanvasGroup.alpha = 0;
        mainMenuCanvasGroup.interactable = false;
        mainMenuCanvasGroup.blocksRaycasts = false;
    }
    private void ActivateQuitMenu()
    {
        quitMenuCanvaGroup.alpha = 1;
        quitMenuCanvaGroup.interactable = true;
        quitMenuCanvaGroup.blocksRaycasts = true;
    }
    private void CloseQuitMenu()
    {
        quitMenuCanvaGroup.alpha = 0;
        quitMenuCanvaGroup.interactable = false;
        quitMenuCanvaGroup.blocksRaycasts = false;
    }
}