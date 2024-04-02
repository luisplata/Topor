using System;
using Unity.Collections;
using UnityEngine;

public abstract class Topo : MonoBehaviour
{
    [SerializeField] protected string id;
    [SerializeField] protected float damage;
    [SerializeField] protected float timeToInit;
    [SerializeField] protected float timeToSearch;
    [SerializeField] protected float timeToAction;
    [SerializeField] protected float timeToBite;
    [SerializeField] protected float timeToEnd;
    [SerializeField] protected float timeToDead;
    [SerializeField] protected float distanceToSearch;
    [SerializeField] protected int biteCount;
    [ReadOnly][SerializeField] protected bool touched;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Color colorToInit, colorToSearch, colorToAction, colorToEnd, colorToDead;
    private float _deltaTimeLocal;
    public Action OnTopoDie;
    [SerializeField] private Fruit _fruitSelected;
    
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
            FindFruits();
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
            else if(_deltaTimeLocal >= timeToBite && biteCount > 0)
            {
                _fruitSelected?.Bite(damage);
                biteCount--;
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

    private void FindFruits()
    {
        //shot 2D raycasts to find fruits in top, bottom, left and right
        //shot up
        
        Fruit top = null, bottom = null, left = null, right = null;
        
        //ServiceLocator.Instance.GetService<IDebugCustom>().DebugText($"Topo {id} is searching for fruits");
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, Vector2.up * distanceToSearch);
        foreach (var raycastHit2D in hit)
        {
            if (raycastHit2D.collider.CompareTag("Fruit"))
            {
                ServiceLocator.Instance.GetService<IDebugCustom>()
                    .DebugText($"Topo {id} found a fruit {raycastHit2D.collider.name} in top");
                top = raycastHit2D.collider.GetComponent<Fruit>();
                break;
            }
        }
        
        //shot down
        hit = Physics2D.RaycastAll(transform.position, Vector2.down * distanceToSearch);
        foreach (var raycastHit2D in hit)
        {
            if (raycastHit2D.collider.CompareTag("Fruit"))
            {
                ServiceLocator.Instance.GetService<IDebugCustom>()
                    .DebugText($"Topo {id} found a fruit  {raycastHit2D.collider.name} in bottom");
                bottom = raycastHit2D.collider.GetComponent<Fruit>();
                break;
            }
        }
        
        //shot left
        hit = Physics2D.RaycastAll(transform.position, Vector2.left * distanceToSearch);
        foreach (var raycastHit2D in hit)
        {
            if (raycastHit2D.collider.CompareTag("Fruit"))
            {
                ServiceLocator.Instance.GetService<IDebugCustom>()
                    .DebugText($"Topo {id} found a fruit {raycastHit2D.collider.name} in left");
                left = raycastHit2D.collider.GetComponent<Fruit>();
                break;
            }
        }
        
        //shot right
        hit = Physics2D.RaycastAll(transform.position, Vector2.right * distanceToSearch);
        foreach (var raycastHit2D in hit)
        {
            if (raycastHit2D.collider.CompareTag("Fruit"))
            {
                ServiceLocator.Instance.GetService<IDebugCustom>()
                    .DebugText($"Topo {id} found a fruit {raycastHit2D.collider.name} in right");
                right = raycastHit2D.collider.GetComponent<Fruit>();
                break;
            }
        }
        
        //get random fruit to attack
        var fruits = new[] {top, bottom, left, right};
        //filter nulls
        fruits = Array.FindAll(fruits, fruit => fruit != null);
        if (fruits.Length == 0)
        {
            ServiceLocator.Instance.GetService<IDebugCustom>().DebugText($"Topo {id} didn't find any fruit");
            return;
        }
        _fruitSelected = fruits[UnityEngine.Random.Range(0, fruits.Length)];
    }
    

    public void Touch()
    {
        touched = true;
    }

    public void StartTopo()
    {
        _idle.Play();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.up * distanceToSearch);
        Gizmos.DrawRay(transform.position, Vector2.down * distanceToSearch);
        Gizmos.DrawRay(transform.position, Vector2.left * distanceToSearch);
        Gizmos.DrawRay(transform.position, Vector2.right * distanceToSearch);
    }
}