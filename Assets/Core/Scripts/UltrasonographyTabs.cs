using System.Collections;
using System.Collections.Generic;
using Core.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class UltrasonographyTabs : MonoBehaviour
{
    [SerializeField] private GameObject _patientButtonPrefab;
    [SerializeField] private GameObject _namesButtonPrefab;
    [SerializeField] private Transform _patientContent;
    [SerializeField] private Transform _namesContent;

    public static UltrasonographyTabs Instance { get; private set; }

    private List<string> _patientsArray = new();
    private List<string> _namesArray = new();
    private const string URL_PATIENTS = Config.URL + "/ultras/patients";
    private const string URL_NAMES = Config.URL + "/ultras/names";

    public string _currentPatient = "";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        StartCoroutine(FetchPatientsFromApi());
    }
    
    IEnumerator FetchPatientsFromApi()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL_PATIENTS);
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


        for (int i = _patientContent.childCount - 1; i >= 0; i--)
        {
            Destroy(_patientContent.GetChild(i).gameObject);
        }

        foreach (var item in receivedNames)
        {
            _patientsArray.Add(item);
            GameObject spawnedButton = Instantiate(_patientButtonPrefab, _patientContent);
            spawnedButton.GetComponentInChildren<TextMeshProUGUI>().text = item;
        }
        
    }
    
    IEnumerator FetchNamesFromApi()
    {
        if (_currentPatient == "") yield break;
        UnityWebRequest www = UnityWebRequest.Get(URL_NAMES + "?patient=" + _currentPatient);
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


        for (int i = _namesContent.childCount - 1; i >= 0; i--)
        {
            Destroy(_namesContent.GetChild(i).gameObject);
        }

        foreach (var item in receivedNames)
        {
            _namesArray.Add(item);
            GameObject spawnedButton = Instantiate(_namesButtonPrefab, _namesContent);
            spawnedButton.GetComponentInChildren<TextMeshProUGUI>().text = item;
        }
        
    }

    public void RefreshList()
    {
        StartCoroutine(FetchPatientsFromApi());
        StartCoroutine(FetchNamesFromApi());
    }

    public void SetCurrentPatient(string patient)
    {
        _currentPatient = patient;
        RefreshList();
    }
}
