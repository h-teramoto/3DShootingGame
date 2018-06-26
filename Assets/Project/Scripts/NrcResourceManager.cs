using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NrcResourceManager
{
    private static NrcResourceManager Instance = new NrcResourceManager();

    private Dictionary<string, Object> resourceDictionary
        = new Dictionary<string, Object>();

    private static Object GetObject(string key)
    {
        Object obj;

        if (!NrcResourceManager.IsExist(key))
        {
            obj = Resources.Load(key);
        }
        else
        {
            obj = Instance.resourceDictionary[key];
        }

        return obj;
    }

    /// <summary>
    /// key名でResourceの取得
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static GameObject GetGameObject(string key)
    {
        Object obj = GetObject(key);
        if (obj is GameObject)return obj as GameObject;
        return null;
    }

    /// <summary>
    /// key名でResourceの取得
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static AudioClip GetAudioClip(string key)
    {
        Object obj = GetObject(key);
        if (obj is AudioClip) return obj as AudioClip;
        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="go"></param>
    /// <returns></returns>
    public static void PutResource(string key, Object go)
    {
        Instance.resourceDictionary.Add(key, go);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool IsExist(string key)
    {
        return Instance.resourceDictionary.ContainsKey(key);
    }


}
