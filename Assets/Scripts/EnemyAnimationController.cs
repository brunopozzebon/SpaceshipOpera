using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void death()
    {
        animator.SetBool("isDeath", true);
    }
}
