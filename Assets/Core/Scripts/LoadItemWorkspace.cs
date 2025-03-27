using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class LoadItemWorkspace : MonoBehaviour
{
    [SerializeField] private GameObject _buttonPrefab;
    [SerializeField] private Transform _content;
    
    //todo: change to localhost
    private const string URL = "http://192.168.137.239:3000/api/names";
    private List<string> _namesArray = new ();

    private void Start()
    {
        Debug.Log(URL);
        StartCoroutine(FetchStringsFromApi());
    }

    IEnumerator FetchStringsFromApi()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();

        if (www.isNetworkError)
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

public class BypassCertificateValidation : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        // Always return true to accept all certificates
        return true;
    }
}
