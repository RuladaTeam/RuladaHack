using TMPro;
using UnityEngine;

public class LoadMenuButtonSonocPatient : MonoBehaviour
{
    public void OnClick()
    {
        UltrasonographyTabs.Instance.SetCurrentPatient(GetComponentInChildren<TextMeshProUGUI>().text);
    }
}
