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
        //evaluate the dirrection and play the animation that corresponds Up/Down or Left/Right
        if (direction.x > 0)
        {
            spriteRenderer.flipX = direction.x > 0;
            animator.SetTrigger(attackLeftAndRight);
        }
        else
        {
            animator.SetTrigger(direction.y > 0 ? attackTop : attackBottom);
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