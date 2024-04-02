using UnityEngine;

public abstract class Fruit : MonoBehaviour
{
    [SerializeField] private string id;
    [SerializeField] private float life;
    [SerializeField] private float timeToidle, timeToGame, timeToBite, timeToDead, timeToDestroyed;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color colorToGame, colorToDead, colorToDestroyed, colorToBite, colorToIdle;
    private TeaTime _idle, _game, _bite, _dead, _destroyed;
    public string Id => id;

    public void Configure(GameObject parent)
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
            spriteRenderer.color = colorToIdle;
        }).Add(timeToidle).Add(() =>
        {
            if (life <= 0) return;
            _game.Play();
        });

        _game = this.tt().Pause().Add(() =>
        {
            if (life <= 0) return;
            //ServiceLocator.Instance.GetService<IDebugCustom>().DebugText($"Fruit {id}: Game");
            spriteRenderer.color = colorToGame;
        }).Wait(() => life <= 0).Add(() =>
        {
            if (life <= 0) return;
            _dead.Play();
        });
        
        _bite = this.tt().Pause().Add(() =>
        {
            if (life <= 0) return;
            //ServiceLocator.Instance.GetService<IDebugCustom>().DebugText($"Fruit {id}: Bite");
            spriteRenderer.color = colorToBite;
            _game.Stop();
        }).Add(timeToBite).Add(() =>
        {
            if (life <= 0)
            {
                _dead.Play();
            }
        }).Add(() =>
        {
            if (life <= 0) return;
            _game.Play();
        });
        
        _dead = this.tt().Pause().Add(() =>
        {
            //ServiceLocator.Instance.GetService<IDebugCustom>().DebugText($"Fruit {id}: Dead");
            spriteRenderer.color = colorToDead;
        }).Add(timeToDead).Add(() =>
        {
            _destroyed.Play();
        });
        
        _destroyed = this.tt().Pause().Add(() =>
        {
            //ServiceLocator.Instance.GetService<IDebugCustom>().DebugText($"Fruit {id}: Destroyed Start");
            spriteRenderer.color = colorToDestroyed;
        }).Add(timeToDestroyed).Add(() =>
        {
            //ServiceLocator.Instance.GetService<IDebugCustom>().DebugText($"Fruit {id}: Destroyed End");
            Destroy(gameObject);
        });
    }

    public void Bite(float damage)
    {
        life -= damage;
        _bite.Play();
    }
}