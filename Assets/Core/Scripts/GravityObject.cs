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
        _rb.useGravity = true;
    }
}
