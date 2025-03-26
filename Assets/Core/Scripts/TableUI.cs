using System;
using UnityEngine;

public class TableUI : MonoBehaviour
{
    
    public void CloseTab()
    {
        Workspace.Instance.DisableUI();
    }
}
