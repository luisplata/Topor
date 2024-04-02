using System;
using Unity.Collections;
using UnityEngine;

public abstract class Topo : MonoBehaviour
{
    [SerializeField] protected string id;
    [SerializeField] protected float timeToInit;
    [SerializeField] protected float timeToSearch;
    [SerializeField] protected float timeToAction;
    [SerializeField] protected float timeToEnd;
    [SerializeField] protected float timeToDead;
    [ReadOnly][SerializeField] protected bool touched;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Color colorToInit, colorToSearch, colorToAction, colorToEnd, colorToDead;
    private float _deltaTimeLocal;
    public Action OnTopoDie;
    
    public float TotalTime => timeToInit + timeToSearch + timeToAction + timeToEnd + timeToDead; 
    public string Id => id;
    
    private TeaTime _idle, _search, _action, _end, _dead, _destroyed;

    private void ConfigureTeaTime()
    {
        _idle = this.tt().Pause().Add(() =>
        {
            touched = false;
            //ServiceLocator.Instance.GetService<IDebugCustom>().DebugText($"Topo {id}: Idle start animation");
            spriteRenderer.color = colorToInit;
            _deltaTimeLocal = 0;
        }).Loop(t =>
        {
            _deltaTimeLocal += t.deltaTime;
            if (touched)
            {
                _dead.Play();
                t.Break();
            }
            else if (_deltaTimeLocal >= timeToInit)
            {
                _search.Play();
                t.Break();
            }
        });
        
        _search = this.tt().Pause().Add(() =>
        {
            //ServiceLocator.Instance.GetService<IDebugCustom>().DebugText($"Topo {id}: Search");
            _deltaTimeLocal = 0;
            spriteRenderer.color = colorToSearch;
        }).Loop(t =>
        {
            _deltaTimeLocal += t.deltaTime;
            if (touched)
            {
                _dead.Play();
                t.Break();
            }
            else if (_deltaTimeLocal >= timeToSearch)
            {
                _action.Play();
                t.Break();
            }
        });

        _action = this.tt().Pause().Add(() =>
        {
            //ServiceLocator.Instance.GetService<IDebugCustom>().DebugText($"Topo {id}: Action");
            _deltaTimeLocal = 0;
            spriteRenderer.color = colorToAction;
        }).Loop(t =>
        {
            _deltaTimeLocal += t.deltaTime;
            if (touched)
            {
                _dead.Play();
                t.Break();
            }
            else if (_deltaTimeLocal >= timeToAction)
            {
                _end.Play();
                t.Break();
            }
        });
        
        _end = this.tt().Pause().Add(() =>
        {
            //ServiceLocator.Instance.GetService<IDebugCustom>().DebugText($"Topo {id}: End");
            spriteRenderer.color = colorToEnd;
        }).Add(timeToEnd).Add(() =>
        {
            _destroyed.Play();
        });
        
        _dead = this.tt().Pause().Add(() =>
        {
            ServiceLocator.Instance.GetService<IDebugCustom>().DebugText($"Topo {id}: Dead");
            spriteRenderer.color = colorToDead;
        }).Add(timeToDead).Add(() =>
        {
            _destroyed.Play();
        });
        
        _destroyed = this.tt().Pause().Add(() =>
        {
            OnTopoDie?.Invoke();
            ServiceLocator.Instance.GetService<IDebugCustom>().DebugText($"Topo {id}: Destroyed");
            transform.SetParent(null);
            gameObject.SetActive(false);
        });
    }

    public void Configure(GameObject parent)
    {
        transform.SetParent(parent.transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        ConfigureTeaTime();
    }

    public void Touch()
    {
        ServiceLocator.Instance.GetService<IDebugCustom>().DebugText($"Topo {id} touched");
        touched = true;
    }

    public void StartTopo()
    {
        _idle.Play();
    }
}