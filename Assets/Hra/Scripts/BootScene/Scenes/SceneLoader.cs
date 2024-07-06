using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public enum Scenes
    {
        BootScene,
        MenuScene,
        GameScene
    }

    private static readonly float MAX_LOAD_PROGRESS = 1f;
    private static Coroutine _loadingCoroutine = null;
    private static AsyncOperation _asyncOp;
    private static Scenes _sceneToLoad;
    private static Scenes _sceneToUnload;
    private static bool _activateAfterLoad;

    private static DummyMonoBehaviour _loaderMonoBehaviour;
    private static DummyMonoBehaviour LoaderMonoBehaviour
    {
        get
        {
            if (_loaderMonoBehaviour == null)
            {
                GameObject loaderGameObject = new GameObject("SceneLoader Game Object");
                _loaderMonoBehaviour = loaderGameObject.AddComponent<DummyMonoBehaviour>();
            }

            return _loaderMonoBehaviour;
        }
    }

    /// <summary>
    /// Reports async load progress
    /// </summary>
    public static event Action<float> OnSceneLoadProgress;
    /// <summary>
    /// This action is triggered when async load has finished loading
    /// </summary>
    public static event Action<Scenes> OnSceneLoadDone;
    /// <summary>
    /// This action is triggered when async unload has finished loading
    /// </summary>
    public static event Action<Scenes> OnSceneUnloadDone;
    /// <summary>
    /// This action is triggered when the scene has been marked as active
    /// </summary>
    public static event Action<Scenes> OnSceneMadeActive;

    public static void LoadScene(Scenes scene, bool additive = true, bool setActive = true, Scenes? toUnload = null)
    {
        if (toUnload != null)
        {
            UnloadScene((Scenes)toUnload);
        }

        if (_loadingCoroutine != null)
        {
            Debug.LogError("[Scene loader] - Cannot load scene " + scene + ". Scene loading already in progress, multiple scene loading is not currently supported");
            return;
        }

        _sceneToLoad = scene;
        _activateAfterLoad = setActive;
        _loadingCoroutine = LoaderMonoBehaviour.StartCoroutine(LoadSceneCR(_sceneToLoad, additive ? LoadSceneMode.Additive : LoadSceneMode.Single));
    }

    /// <summary>
    /// Set scene as active. In typical use case this is called automatically afer scene is loaded, but it can be invoked even manually.
    /// </summary>
    /// <param name="scene">Scene to set active</param>
    public static void SetSceneAsActive(Scenes scene)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene.ToString()));
        //Debug.Log("[Scene loader] - Currently set active scene: " + SceneManager.GetActiveScene().name);

        OnSceneMadeActive?.Invoke(scene);
    }

    /// <summary>
    /// Returns active scene
    /// </summary>
    /// <returns>Active scene, enum</returns>
    public static Scenes GetActiveScene()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        Enum.TryParse(activeScene.name, out Scenes currScene);

        return currScene;
    }

    public static bool IsSceneLoaded(Scenes scene)
    {
        int countLoaded = SceneManager.sceneCount;
        for (int i = 0; i < countLoaded; i++)
        {
            Scene currScene = SceneManager.GetSceneAt(i);
            Enum.TryParse(currScene.name, out Scenes sceneToCompare);
            if (scene == sceneToCompare)
            {
                return true;
            }
        }

        return false;
    }

    public static void UnloadScene(Scenes scene)
    {
        //Debug.Log("[Scene loader] Unloading the scene " + scene);

        _sceneToUnload = scene;
        AsyncOperation unloadOp = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(scene.ToString());
        unloadOp.completed += SceneUnloadDone;
    }

    private static void SceneUnloadDone(AsyncOperation unloadOp)
    {
        //Debug.Log("[Scene loader] - Scene unloading done! Scene: " + _sceneToUnload);
        unloadOp.completed -= SceneUnloadDone;

        LoaderMonoBehaviour.StartCoroutine(DelayedUnloadDoneInvoke());
    }

    /// <summary>
    /// Internal coroutine to handle loading progress. It automatically turn on the loaded scene.
    /// In case we want to wait for player input before the scene is loaded, _asyncOp.allowSceneActivation needs to be set to false.
    /// In that case we would have to change the code slightly.
    /// See more info here: https://docs.unity3d.com/ScriptReference/AsyncOperation.html
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="sceneLoadMode"></param>
    /// <returns></returns>
    private static IEnumerator LoadSceneCR(Scenes scene, LoadSceneMode sceneLoadMode)
    {
        _asyncOp = SceneManager.LoadSceneAsync(scene.ToString(), sceneLoadMode);
        _asyncOp.completed += SceneLoadingDone;

        while (_asyncOp.progress < MAX_LOAD_PROGRESS)
        {
            //Debug.Log("[Scene loader] - Loading scene " + scene + ", progress: " + _asyncOp.progress);
            OnSceneLoadProgress?.Invoke(_asyncOp.progress);
            yield return null;
        }

        OnSceneLoadProgress?.Invoke(MAX_LOAD_PROGRESS);
        _loadingCoroutine = null;
    }

    /// <summary>
    /// Callback after successful scene load
    /// </summary>
    /// <param name="asyncOp"></param>
    private static void SceneLoadingDone(AsyncOperation asyncOp)
    {
        //Debug.Log("[Scene loader] - Scene loading done!");
        _asyncOp.completed -= SceneLoadingDone;
        if (_activateAfterLoad)
        {
            SetSceneAsActive(_sceneToLoad);
        }

        LoaderMonoBehaviour.StartCoroutine(DelayedLoadDoneInvoke());
    }

    private static IEnumerator DelayedLoadDoneInvoke()
    {
        yield return null;
        OnSceneLoadDone?.Invoke(_sceneToLoad);
    }

    private static IEnumerator DelayedUnloadDoneInvoke()
    {
        yield return null;
        OnSceneUnloadDone?.Invoke(_sceneToUnload);
    }
}
