using System;
using UnityEngine;

public class AnimationBehaviour : MonoBehaviour, IAnimationBehaviour
{
    [SerializeField] private Animator animator;
    private TeaTime _idle, _idleHammer;

    private void Awake()
    {
        if (FindObjectsOfType<AnimationBehaviour>() != null && FindObjectsOfType<AnimationBehaviour>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        ServiceLocator.Instance.RegisterService<IAnimationBehaviour>(this);
    }

    private void Start()
    {
        _idle = this.tt().Pause().Add(() => { PlayAnimation(AnimationTriggerType.IDLE); }).Add(GetTimeToAnimation())
            .Add(() =>
            {
                //random to play hammer
                if (UnityEngine.Random.Range(0, 2) == 1)
                {
                    _idleHammer.Play();
                }
            });

        _idleHammer = this.tt().Pause().Add(() => { PlayAnimation(AnimationTriggerType.HAMMER); })
            .Add(GetTimeToAnimation())
            .Add(() => { _idle.Play(); });

        _idle.Play();
    }

    private float GetTimeToAnimation()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length;
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.UnregisterService<IAnimationBehaviour>();
    }

    [ContextMenu("Play Idle Animation")]
    public void PlayIdle()
    {
        PlayAnimation(AnimationTriggerType.IDLE);
    }

    [ContextMenu("Play Hammer Animation")]
    public void PlayHammer()
    {
        PlayAnimation(AnimationTriggerType.HAMMER);
    }

    [ContextMenu("Play Success Hit Animation")]
    public void PlaySuccessHit()
    {
        PlayAnimation(AnimationTriggerType.SUCCESS_HIT);
    }

    [ContextMenu("Play Fail Hit Animation")]
    public void PlayFailHit()
    {
        PlayAnimation(AnimationTriggerType.FAIL_HIT);
    }

    [ContextMenu("Play Positive Feedback Animation")]
    public void PlayPositiveFeedback()
    {
        PlayAnimation(AnimationTriggerType.POSITIVE_FEEDBACK);
    }

    [ContextMenu("Play Negative Feedback Animation")]
    public void PlayNegativeFeedback()
    {
        PlayAnimation(AnimationTriggerType.NEGATIVE_FEEDBACK);
    }

    [ContextMenu("Play Victory Animation")]
    public void PlayVictory()
    {
        PlayAnimation(AnimationTriggerType.VICTORY);
    }

    [ContextMenu("Play Defeat Animation")]
    public void PlayDefeat()
    {
        PlayAnimation(AnimationTriggerType.DEFEAT);
    }

    public void PlayAnimation(AnimationTriggerType trigger)
    {
        animator.SetTrigger(trigger.ToString().ToLower());
    }
}

public enum AnimationTriggerType
{
    IDLE,
    HAMMER,
    SUCCESS_HIT,
    FAIL_HIT,
    POSITIVE_FEEDBACK,
    NEGATIVE_FEEDBACK,
    VICTORY,
    DEFEAT,
}

public interface IAnimationBehaviour
{
    void PlaySuccessHit();
    void PlayFailHit();
    void PlayPositiveFeedback();
    void PlayNegativeFeedback();
    void PlayVictory();
    void PlayDefeat();
    void PlayIdle();
    void PlayHammer();
}