using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class SceneManager : MonoBehaviour
{
    public Button reloadButton;    // Assign this button in the Inspector
    public Button untrackButton;  // Assign this button in the Inspector

    private List<ObserverBehaviour> trackedObjects = new List<ObserverBehaviour>();

    private void Start()
    {
        // Assign button listeners
        if (reloadButton != null)
            reloadButton.onClick.AddListener(ReloadApp);

        if (untrackButton != null)
            untrackButton.onClick.AddListener(UntrackAll);

        // Find all Observer Behaviours in the scene and add them to the list
        var allTrackables = FindObjectsOfType<ObserverBehaviour>();
        foreach (var trackable in allTrackables)
        {
            trackedObjects.Add(trackable);
        }

        Debug.Log($"Found {trackedObjects.Count} trackable objects.");
    }

    // Reload the current scene
    public void ReloadApp()
    {
        UnityEngine.SceneManagement.Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene.name);
        Debug.Log("App reloaded!");
    }

    // Untrack all tracked Vuforia objects
    public void UntrackAll()
    {
        foreach (var trackable in trackedObjects)
        {
            if (trackable != null)
            {
                trackable.enabled = false; // Disable tracking
                Debug.Log($"Untracked: {trackable.TargetName}");
            }
        }

        Debug.Log("All trackable objects have been untracked.");
    }

    // Optional: Re-enable tracking for all objects
    public void ReTrackAll()
    {
        foreach (var trackable in trackedObjects)
        {
            if (trackable != null)
            {
                trackable.enabled = true; // Re-enable tracking
                Debug.Log($"Re-tracked: {trackable.TargetName}");
            }
        }
        Debug.Log("All trackable objects have been re-tracked.");
    }
}
