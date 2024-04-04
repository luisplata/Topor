using UnityEngine;
using UnityEngine.Serialization;

public class AnimationControllerFruit : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string trigger_rise;
    [SerializeField] private string trigger_game;
    [SerializeField] private string trigger_bite, trigger_dead;

    public void PlayRise()
    {
        animator.SetTrigger(trigger_rise);
    }

    public void PlayGame()
    {
        animator.SetTrigger(trigger_game);
    }

    public void PlayBite()
    {
        animator.SetTrigger(trigger_bite);
    }

    public void PlayDead()
    {
        animator.SetTrigger(trigger_dead);
    }
}