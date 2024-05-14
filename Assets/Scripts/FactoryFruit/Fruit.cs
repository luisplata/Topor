using System;
using UnityEngine;

public abstract class Fruit : MonoBehaviour
{
    [SerializeField] private string id;
    [SerializeField] private float life;
    [SerializeField] private float timeToidle, timeToGame, timeToBite, timeToDead, timeToDestroyed;
    [SerializeField] private AnimationControllerFruit animationControllerFruit;
    public event Action OnFruitDie;
    private TeaTime _idle, _game, _bite, _dead, _destroyed;
    private bool _areYouDead;
    public bool AreDead => _areYouDead;
    public string Id => id;

    public void Configure(PointToFruit parent)
    {
        transform.SetParent(parent.transform);
        transform.localPosition = Vector3.zero;
        ConfigureTeaTime();
        _idle.Play();
    }

    private void ConfigureTeaTime()
    {
        _idle = this.tt().Pause().Add(() =>
        {
            if (life <= 0) return;
            //ServiceLocator.Instance.GetService<IDebugCustom>().DebugText($"Fruit {id}: Idle start animation");
            animationControllerFruit.PlayRise();
        }).Add(timeToidle).Add(() =>
        {
            _game.Play();
        });

        _game = this.tt().Pause().Add(() =>
        {
            //ServiceLocator.Instance.GetService<IDebugCustom>().DebugText($"Fruit {id}: Game");
            animationControllerFruit.PlayGame();
        }).Wait(() => life <= 0).Add(() =>
        {
            _dead.Play();
        });
        
        _bite = this.tt().Pause().Add(() =>
        {
            //ServiceLocator.Instance.GetService<IDebugCustom>().DebugText($"Fruit {id}: Bite");
            animationControllerFruit.PlayBite();
        }).Add(timeToBite).Add(() =>
        {
            if (life <= 0)
            {
                _dead.Play();
                _areYouDead = true;
                OnFruitDie?.Invoke();
            }
        }).Add(() =>
        {
            if (!_areYouDead)
            {
                _idle.Play();
            }
        });
        
        _dead = this.tt().Pause().Add(() =>
        {
            //ServiceLocator.Instance.GetService<IDebugCustom>().DebugText($"Fruit {id}: Dead");
            animationControllerFruit.PlayDead();
        }).Add(timeToDead).Add(() =>
        {
            _destroyed.Play();
        });
        
        _destroyed = this.tt().Pause().Add(() =>
        {
            //ServiceLocator.Instance.GetService<IDebugCustom>().DebugText($"Fruit {id}: Destroyed Start");
        }).Add(timeToDestroyed).Add(() =>
        {
            //ServiceLocator.Instance.GetService<IDebugCustom>().DebugText($"Fruit {id}: Destroyed End");
            //gameObject.SetActive(false);
        });
    }

    public void Bite(float damage)
    {
        life -= damage;
        if(_areYouDead) return;
        _idle.Stop();
        _game.Stop();
        _bite.Play();
    }

}