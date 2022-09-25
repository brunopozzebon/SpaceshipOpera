using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform playerPosition;
    public Transform shootingPointPosition;
    public GameObject shootObject;
    private NavMeshAgent ai;
   
    private Rigidbody rigidBody;

    void Start()
    {
        ai = gameObject.GetComponent<NavMeshAgent>();
        rigidBody = gameObject.GetComponent<Rigidbody>();
        StartCoroutine(shoot());
    }

    void Update()
    {
        ai.SetDestination(playerPosition.position);
    }

    IEnumerator shoot()
    {
        while (true)
        {
            GameObject shootClone = Instantiate(shootObject, shootingPointPosition.position, shootingPointPosition.rotation);
        
            Rigidbody shootRigidBody = shootClone.GetComponent<Rigidbody>();
            shootClone.AddComponent<EnemyShootScript>();


            Vector3 shootForce = playerPosition.position - rigidBody.position;
            shootForce.y = 0;
            shootRigidBody.AddForce(shootForce * 100);
            
            yield return new WaitForSeconds(2f);
        }
    }
    
    public void receiveDamage()
    {
        Destroy(gameObject);
    }
}