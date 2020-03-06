using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public CanvasGroup mainMenuCanvasGroup;
    public CanvasGroup quitMenuCanvaGroup;

    public void QuitMenu()
    {
        mainMenuCanvasGroup.alpha = 0;
        mainMenuCanvasGroup.interactable = false;
        mainMenuCanvasGroup.blocksRaycasts = false;

        quitMenuCanvaGroup.alpha = 1;
        quitMenuCanvaGroup.interactable = true;
        quitMenuCanvaGroup.blocksRaycasts = true;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void DontQuit()
    {
        mainMenuCanvasGroup.alpha = 1;
        mainMenuCanvasGroup.interactable = true;
        mainMenuCanvasGroup.blocksRaycasts = true;

        quitMenuCanvaGroup.alpha = 0;
        quitMenuCanvaGroup.interactable = false;
        quitMenuCanvaGroup.blocksRaycasts = false;
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("SetupScene");
    }


}
