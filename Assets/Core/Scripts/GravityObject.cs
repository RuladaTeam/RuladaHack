using System;
using UnityEngine;

public class GravityObject : MonoBehaviour
{
    private Rigidbody _rb;
    
    private void Start() 
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void SetGravity()
    {
        // try
        // {
        //     _rb.useGravity = true;
        //
        // }
        // catch (Exception e)
        // {
        //     Debug.Log(this.name);
        //     throw;
        // } 
    }
}
