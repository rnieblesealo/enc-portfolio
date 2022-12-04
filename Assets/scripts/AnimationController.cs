using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private Locomotor locomotor;

    private string currentState;

    private void Awake(){
        animator = GetComponent<Animator>();
        locomotor = GetComponent<Locomotor>();
    }

    private void Update(){
        //animator.speed = 1 / locomotor.distance;
    }
}
