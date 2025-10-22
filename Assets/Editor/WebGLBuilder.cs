using UnityEditor;
using UnityEngine;
using System.Linq;

public class WebGLBuilder
{
    public static void BuildWebGL()
    {
        // ビルドに含めるシーンを取得
        string[] scenes = EditorBuildSettings.scenes
            .Where(scene => scene.enabled)
            .Select(scene => scene.path)
            .ToArray();

        if (scenes.Length == 0)
        {
            Debug.LogError("No scenes are enabled in Build Settings!");
            EditorApplication.Exit(1);
            return;
        }

        string buildPath = "build/WebGL";

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = buildPath,
            target = BuildTarget.WebGL,
            options = BuildOptions.None
        };

        var report = BuildPipeline.BuildPlayer(buildPlayerOptions);

        if (report.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            Debug.LogError($"Build failed: {report.summary.result}");
            EditorApplication.Exit(1);
        }
        else
        {
            Debug.Log($"Build succeeded: {report.summary.totalSize} bytes");
            EditorApplication.Exit(0);
        }
    }
}