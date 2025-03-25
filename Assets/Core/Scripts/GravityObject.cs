using UnityEngine;

public class GravityObject : MonoBehaviour
{
    Rigidbody rb;
    
    private void Start() 
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetGravity()
    {
        rb.useGravity = true;
    }
}
