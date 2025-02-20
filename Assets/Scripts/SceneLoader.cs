using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void OpenHome()
    {
        Application.LoadLevel("MainMenu"); // Index of the scene in the Build Settings (1 for the second scene)
    }

    // Method to open Level 1 using index
    public void OpenLevel1()
    {
        Application.LoadLevel("Level1"); // Index of the scene in the Build Settings (0 for the first scene)
    }

    // Method to open Level 2 using index
    public void OpenLevel2()
    {
        Application.LoadLevel("Level2"); // Index of the scene in the Build Settings (1 for the second scene)
    }

    public void OpeExamples()
    {
        Application.LoadLevel("Examples"); // Index of the scene in the Build Settings (1 for the second scene)
    }
}
