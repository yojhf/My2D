using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임 스프라이트 오브젝트를 페이드아웃 후 킬
public class FadeBehaviour : StateMachineBehaviour
{
    public float fadeTimer = 1f;
    private float count = 0f;
    
    private SpriteRenderer spriteRenderer;
    private GameObject removeObject;
    private Color startColor;

    public float delay = 2f;
    private float delayCount = 0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        spriteRenderer = animator.transform.GetComponent<SpriteRenderer>();

        startColor = spriteRenderer.color;

        removeObject = animator.gameObject;

        count = fadeTimer;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


        if (delayCount <= delay)
        {
            delayCount += Time.deltaTime;
            return;
        }

        count -= Time.deltaTime;

        float newAlpha = startColor.a * (count / fadeTimer);

        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

        if (count <= 0f)
        {
            Destroy(removeObject);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
