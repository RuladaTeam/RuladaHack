using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadItemWorkspace : MonoBehaviour
{
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private Transform _content;
    
    //todo: change to localhost
    private List<string> _namesArray = new ();
    private const string URL = Config.URL + "/names";

    private void Start()
    {
        StartCoroutine(FetchStringsFromApi());
    }

    IEnumerator FetchStringsFromApi()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }

        string text = www.downloadHandler.text;
        string[] array = text.Substring(1,text.Length-2 ).Split(",");
        foreach (var item in array)
        {
            _namesArray.Add(item);
            GameObject spawnedButton = Instantiate(_buttonPrefab, _content);
            spawnedButton.GetComponentInChildren<TextMeshProUGUI>().text = item;
        }
    }
}