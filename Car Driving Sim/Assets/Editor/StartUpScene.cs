using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public static class StartupScene
{
    private const string ScenePath = "Assets/Scenes/MainMenu.unity";

    static StartupScene() { EditorApplication.delayCall += OpenStartupScene; }

    private static void OpenStartupScene()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode)
            return;

        if (EditorSceneManager.GetActiveScene().path == ScenePath)
            return;

        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            EditorSceneManager.OpenScene(ScenePath);
        }
    }
}
