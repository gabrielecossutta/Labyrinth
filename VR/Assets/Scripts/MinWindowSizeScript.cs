using System.Collections;
using UnityEngine;

public class MinWindowSize : MonoBehaviour
{
    // Minimum width of the window
    private int minWidth = 800;

    // Minimum height of the window
    private int minHeight = 600;

    // New width window
    private int newWidth;

    // New height window
    private int newHeight;

    // Variables to track the last known window dimensions
    private int lastWidth;
    private int lastHeight;

    // Delay before enforcing the minimum size, allowing the user to finish resizing
    private float resizeDelay = 0.2f;

    // Coroutine reference to manage resizing delay
    private Coroutine resizeCoroutine;

    void Start()
    {
        // Save the initial window dimensions
        lastWidth = Screen.width;
        lastHeight = Screen.height;
    }

    void Update()
    {
        // Check if the window size has changed
        if (Screen.width != lastWidth || Screen.height != lastHeight)
        {
            // Update the saved dimensions
            lastWidth = Screen.width;
            lastHeight = Screen.height;

            // Check if there is other couroutine started, and stop them
            // This to let the player finish the resize
            if (resizeCoroutine != null)
            {
                StopCoroutine(resizeCoroutine);
            }

            // Start a new coroutine to check window size after a delay
            resizeCoroutine = StartCoroutine(CheckWindowSizeWithDelay());
        }
    }

    IEnumerator CheckWindowSizeWithDelay()
    {
        // Wait for a short delay to see if the user has finished resizing the window
        yield return new WaitForSeconds(resizeDelay);

        // Check if the window is less than the minimum dimensions
        if (Screen.width < minWidth || Screen.height < minHeight)
        {
            // Calculate the width and height returning the greatest one
            newWidth = Mathf.Max(Screen.width, minWidth);
            newHeight = Mathf.Max(Screen.height, minHeight);

            // Force the window size to the new dimensions
            Screen.SetResolution(newWidth, newHeight, false);
        }

        // Reset the coroutine reference
        resizeCoroutine = null;
    }

    void Awake()
    {
        // Prevent this item from being destroyed by loading another scene
        DontDestroyOnLoad(gameObject);
    }
}
