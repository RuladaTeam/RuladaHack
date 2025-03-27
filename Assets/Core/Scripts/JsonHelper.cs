
using UnityEngine;

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        // Wrap the JSON array in an object to make it compatible with Unity's JsonUtility
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
