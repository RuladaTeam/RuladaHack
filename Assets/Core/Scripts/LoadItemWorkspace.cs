using System.Collections;
using System.Collections.Generic;
using Core.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class LoadItemWorkspace : MonoBehaviour
{
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private Transform _content;
   
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
            yield break;
        }
        
        string text = www.downloadHandler.text;
        if (text == "\"\"")
        {
            yield break;
        }
        
        string[] receivedNames = text.Substring(1,text.Length-2 ).Split(",");


        for (int i = _content.childCount - 1; i >= 0; i--)
        {
            Destroy(_content.GetChild(i).gameObject);
        }

        foreach (var item in receivedNames)
        {
            _namesArray.Add(item);
            GameObject spawnedButton = Instantiate(_buttonPrefab, _content);
            spawnedButton.GetComponentInChildren<TextMeshProUGUI>().text = item;
        }
        
    }

    public void RefreshList()
    {
        StartCoroutine(FetchStringsFromApi());
    }
}