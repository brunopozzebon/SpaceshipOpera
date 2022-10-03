
using UnityEngine;

public class CromulumController : MonoBehaviour
{
    public Transform player;
    
    void Update()
    {
        Quaternion _lookRotation = 
            Quaternion.LookRotation((player.position - transform.position).normalized);
        
        transform.rotation = _lookRotation;
    }
}
