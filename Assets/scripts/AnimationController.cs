// This is a VERY simple rushed input based animation controller.
// Note that it doesn't switch to an idle state by default; it just stays at the last set state, so turn looping off!
// It also requires the default state to be called "idle" exactly.
// I know, it sucks. Do better.

using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private Locomotor locomotor;

    public KeyCode[] triggers;
    public AnimationClip[] stateClips;
    public string[] states;

    private Coroutine ongoingRoutine;
    private string currentState;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        for (int i = 0; i < triggers.Length; ++i)
        {
            if (Input.GetKeyDown(triggers[i]))
            {
                // Set and reset state automatically
                SetState(states[i]);
                ongoingRoutine = StartCoroutine(ResetState(i));
            }
        }
    }

    private void SetState(string newState)
    {
        if (currentState == newState)
            return;

        animator.Play(newState);
        currentState = newState;
    }

    private IEnumerator ResetState(int index)
    {
        yield return new WaitForSeconds(stateClips[index].length);

        SetState("idle"); // This is fucking horrible. Do better :(
        ongoingRoutine = null;
    }
}
