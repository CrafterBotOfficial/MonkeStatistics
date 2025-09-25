using System;
using System.Threading.Tasks;
using UnityEngine;

namespace MonkeStatistics;

internal class AssetLoader
{
    private static Lazy<AssetLoader> instance = new Lazy<AssetLoader>(() => new AssetLoader());
    public static AssetLoader Instance => instance.Value;

    private Task<AssetBundle> loadBundleTask;

    public AssetLoader()
    {
        loadBundleTask = LoadBundle();
    }

    private Task<AssetBundle> LoadBundle()
    {
        Main.Log("Load task started");
        var completionSource = new TaskCompletionSource<AssetBundle>();

        var stream = typeof(Main).Assembly.GetManifestResourceStream("MonkeStatistics.Resources.watch");
        var loadRequest = AssetBundle.LoadFromStreamAsync(stream);
        loadRequest.completed += _ =>
        {
            stream.Dispose();
            completionSource.SetResult(loadRequest.assetBundle);
            Main.Log("Finished loading bundle", BepInEx.Logging.LogLevel.Debug);
        };

        return completionSource.Task;
    }

    public async Task<GameObject> GetAsset(string name)
    {
        var bundle = await loadBundleTask;
        var loadRequest = bundle.LoadAssetAsync<GameObject>(name);
        await loadRequest;
        return loadRequest.asset as GameObject;
    }
}
