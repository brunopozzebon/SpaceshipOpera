using System;
using System.Collections;
using UnityEngine;

public class EnemyShootScript : MonoBehaviour
{

    private const float DAMAGE = 8f;

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
       
       if (collider.gameObject.CompareTag("Player") && SongController.songType != SongType.CLASSIC)
       {
           StarshipController starshipController = collider.gameObject.GetComponent<StarshipController>();
           starshipController.receiveDamage(gameObject.transform.position, DAMAGE);
       }
       Destroy(gameObject);
   }
}
