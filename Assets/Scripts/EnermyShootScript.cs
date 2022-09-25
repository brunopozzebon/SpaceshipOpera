using System;
using System.Collections;
using UnityEngine;

public class EnemyShootScript : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(SelfDestruct());
    }
    
    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
    

    private void OnTriggerEnter(Collider collider)
   {
       
       if (collider.gameObject.CompareTag("Player"))
       {
           StarshipController starshipController = collider.gameObject.GetComponent<StarshipController>();
           starshipController.receiveDamage(gameObject.transform.position);
       }
       Destroy(gameObject);
   }
}
