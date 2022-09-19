using UnityEngine.SceneManagement;

public static class SceneHandler
{
    public static void NextScene()
    {
        SceneManager.LoadScene(
            (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }

    public static void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
