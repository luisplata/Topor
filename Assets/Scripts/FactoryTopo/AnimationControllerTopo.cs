using UnityEngine;
using UnityEngine.Serialization;

public class AnimationControllerTopo : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private string rise;
    [SerializeField] private string Search;
    [SerializeField] private string attackLeftAndRight;
    [SerializeField] private string attackTop;
    [SerializeField] private string attackBottom;
    [SerializeField] private string End, Dead;

    public void PlaySearch()
    {
        animator.SetTrigger(Search);
    }
    
    public void PlayAction(Vector2 direction)
    {
        if (direction == Vector2.left || direction == Vector2.right)
        {
            spriteRenderer.flipX = direction == Vector2.left;
            animator.SetTrigger(attackLeftAndRight);
        }
        else
        {
            animator.SetTrigger(direction == Vector2.up ? attackTop : attackBottom);
        }
    }
    
    public void PlayEnd()
    {
        animator.SetTrigger(End);
    }
    
    public void PlayDead()
    {
        animator.SetTrigger(Dead);
    }

    public void PlayRise()
    {
        animator.SetTrigger(rise);
    }
}