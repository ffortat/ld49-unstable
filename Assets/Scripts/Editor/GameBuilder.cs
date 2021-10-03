using UnityEditor;
using UnityEngine;

public class GameBuilder
{
    [MenuItem("Build/Windows")]
    static void BuildWindows()
    {
        Debug.Log("Building Windows");
        BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, "Build/Windows/WindBlows/WindBlows.exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
        Debug.Log("Done");
    }

    [MenuItem("Build/macOS")]
    static void BuildmacOS()
    {
        Debug.Log("Building macOS");
        BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, "Build/macOS/WindBlows.app", BuildTarget.StandaloneOSX, BuildOptions.None);
        Debug.Log("Done");
    }

    [MenuItem("Build/Linux")]
    static void BuildLinux()
    {
        Debug.Log("Building Linux");
        BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, "Build/Linux/WindBlows/WindBlows", BuildTarget.StandaloneLinux64, BuildOptions.None);
        Debug.Log("Done");
    }

    [MenuItem("Build/WebGL")]
    static void BuildWebGL()
    {
        Debug.Log("Building WebGL");
        BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, "Build/WebGL/WindBlows", BuildTarget.WebGL, BuildOptions.None);
        Debug.Log("Done");
    }

    [MenuItem("Build/Android")]
    static void BuildAndroid()
    {
        Debug.Log("Building Android");
        BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, "Build/Android/WindBlows.apk", BuildTarget.Android, BuildOptions.None);
        Debug.Log("Done");
    }

    [MenuItem("Build/iOS")]
    static void BuildiOS()
    {
        Debug.Log("Building iOS");
        BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, "Build/iOS/WindBlows", BuildTarget.iOS, BuildOptions.None);
        Debug.Log("Done");
    }
}
