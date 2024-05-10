using System;
using UnityEngine;

public static class PlayerPrefSerializer
{
    public static void Save<T>(string key, T obj) where T : class
    {
        try
        {
            string jsonValue = JsonUtility.ToJson(obj);
            PlayerPrefs.SetString(key, jsonValue);
        }
        catch (Exception e)
        {
            Debug.LogError($"[PlayerPrefSerializer] - Failed to Save to key: {key}, Error: {e.Message}");
        }
    }

    public static T Load<T>(string key) where T : class
    {
        if (PlayerPrefs.HasKey(key))
        {
            try
            {
                string jsonValue = PlayerPrefs.GetString(key);
                T readObject = JsonUtility.FromJson<T>(jsonValue);
                return readObject;
            }
            catch (Exception e)
            {
                Debug.LogError($"[PlayerPrefSerializer] - Failed to load key: {key} as {nameof(T)}. Error: {e.Message}");
                return default(T);
            }
        }
        else
        {
            Debug.LogError($"[PlayerPrefSerializer] - Key: {key} do not exist in PlayerPrefs!");
            return default(T);
        }
    }
}
