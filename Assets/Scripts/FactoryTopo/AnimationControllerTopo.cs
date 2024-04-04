using UnityEngine;
using UnityEngine.Serialization;

public class AnimationControllerTopo : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private string rise;
    [SerializeField] private string Search, Action, End, Dead;

    public void PlaySearch()
    {
        animator.SetTrigger(Search);
    }
    
    public void PlayAction(bool attackLeft)
    {
        spriteRenderer.flipX = attackLeft;
        animator.SetTrigger(Action);
    }
    
    public void PlayEnd()
    {
        //return to original position
        transform.localPosition = Vector3.zero;
        animator.SetTrigger(End);
    }
    
    public void PlayDead()
    {
        //return to original position
        transform.localPosition = Vector3.zero;
        animator.SetTrigger(Dead);
    }

    public void PlayRise()
    {
        //move 0.5 in X
        transform.localPosition = Vector3.zero;
        transform.localPosition = new Vector3(transform.localPosition.x + 0.5f, transform.localPosition.y, transform.localPosition.z);
        animator.SetTrigger(rise);
    }
}