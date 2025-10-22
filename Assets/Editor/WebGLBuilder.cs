using UnityEditor;
using UnityEngine;

public static class WebGLBuilder
{
    [MenuItem("Build/Build WebGL")]
    public static void BuildWebGL()
    {
        string[] scenes = GetScenePaths();
        string buildPath = "Build/WebGL";

        // WebGL用のビルド設定
        BuildPlayerOptions buildOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = buildPath,
            target = BuildTarget.WebGL,
            options = BuildOptions.None
        };

        // テンプレートの設定（デフォルトテンプレート使用）
        PlayerSettings.WebGL.template = "PROJECT:Better2020";

        Debug.Log("WebGLビルドを開始します...");
        var report = BuildPipeline.BuildPlayer(buildOptions);

        if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            Debug.Log($"ビルド成功: {report.summary.totalSize} bytes");
        }
        else
        {
            Debug.LogError($"ビルド失敗: {report.summary.result}");
            EditorApplication.Exit(1);
        }
    }

    private static string[] GetScenePaths()
    {
        // ビルド設定から有効なシーンを取得
        var scenes = EditorBuildSettings.scenes;
        var scenePaths = new string[scenes.Length];

        for (int i = 0; i < scenes.Length; i++)
        {
            scenePaths[i] = scenes[i].path;
        }

        // シーンがない場合はエラー
        if (scenePaths.Length == 0)
        {
            Debug.LogError("ビルド設定にシーンが追加されていません！");
            EditorApplication.Exit(1);
        }

        return scenePaths;
    }
}
