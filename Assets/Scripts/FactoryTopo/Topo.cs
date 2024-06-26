﻿using System;
using Unity.Collections;
using UnityEngine;

public abstract class Topo : MonoBehaviour
{
    [SerializeField] protected string id;
    [SerializeField] protected float damage;
    [SerializeField] protected float timeToOutOfGround;
    [SerializeField] protected float timeToInit;
    [SerializeField] protected float timeToSearch;
    [SerializeField] protected float timeToAction;
    [SerializeField] protected float timeToBite;
    [SerializeField] protected float timeToEnd;
    [SerializeField] protected float timeToDead;
    [SerializeField] protected float distanceToSearch;
    [SerializeField] protected int biteCount;
    [SerializeField] protected AnimationControllerTopo animationControllerTopo;
    protected bool touched;
    private float _deltaTimeLocal;
    public Action OnTopoDie;
    private Fruit _fruitSelected;
    private Vector2 direction;
    
    public float TotalTime => timeToInit + timeToSearch + timeToAction + timeToEnd + timeToDead; 
    public string Id => id;
    
    private TeaTime _idle, _search, _action, _end, _dead, _destroyed, _outOfGround;
    private PointToTopo _parent;
    private bool otherTopoOutOfGround = true;

    private void ConfigureTeaTime()
    {
        _outOfGround = this.tt().Pause().Add(() =>
        {
            otherTopoOutOfGround = _parent.OtherTopoOutOfGround();
            if(otherTopoOutOfGround)
            {
                timeToOutOfGround = 0;
            }
            else
            {
                _parent.OutFromGround();
            }
            _parent.SetFree(false);
        }).Add(timeToOutOfGround).Add(() =>
        {
            _idle.Play();
        });
        _idle = this.tt().Pause().Add(() =>
        {
            touched = false;
            animationControllerTopo.PlayRise();
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
            _deltaTimeLocal = 0;
            animationControllerTopo.PlaySearch();
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
                if(direction == Vector2.zero)
                {
                    _end.Play();
                    t.Break();
                }
                else
                {
                    _action.Play();
                    t.Break();
                }
            }
        });

        _action = this.tt().Pause().Add(() =>
        {
            _deltaTimeLocal = 0;
            animationControllerTopo.PlayAction(direction);
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
            animationControllerTopo.PlayEnd();
        }).Add(timeToEnd).Add(() =>
        {
            _destroyed.Play();
        });
        
        _dead = this.tt().Pause().Add(() =>
        {
            animationControllerTopo.PlayDead();
        }).Add(timeToDead).Add(() =>
        {
            _destroyed.Play();
        });
        
        _destroyed = this.tt().Pause().Add(() =>
        {
            _parent.SetFree(true);
            OnTopoDie?.Invoke();
            transform.SetParent(null);
            gameObject.SetActive(false);
        });
    }

    public void Configure(PointToTopo parent)
    {
        transform.SetParent(parent.transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        ConfigureTeaTime();
        _parent = parent;
    }

    private void FindFruits()
    {
        FruitToAttack top = new (), bottom = new (), left = new (), right = new();
        
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, Vector2.up * distanceToSearch);
        foreach (var raycastHit2D in hit)
        {
            if (raycastHit2D.collider.CompareTag("Fruit"))
            {
                top.fruit = raycastHit2D.collider.GetComponent<Fruit>();
                top.direction = Vector2.up;
                break;
            }
        }
        
        //shot down
        hit = Physics2D.RaycastAll(transform.position, Vector2.down * distanceToSearch);
        foreach (var raycastHit2D in hit)
        {
            if (raycastHit2D.collider.CompareTag("Fruit"))
            {
                bottom.fruit = raycastHit2D.collider.GetComponent<Fruit>();
                bottom.direction = Vector2.down;
                break;
            }
        }
        
        //shot left
        hit = Physics2D.RaycastAll(transform.position, Vector2.left * distanceToSearch);
        foreach (var raycastHit2D in hit)
        {
            if (raycastHit2D.collider.CompareTag("Fruit"))
            {
                left.fruit = raycastHit2D.collider.GetComponent<Fruit>();
                left.direction = Vector2.left;
                break;
            }
        }
        
        //shot right
        hit = Physics2D.RaycastAll(transform.position, Vector2.right * distanceToSearch);
        foreach (var raycastHit2D in hit)
        {
            if (raycastHit2D.collider.CompareTag("Fruit"))
            {
                right.fruit = raycastHit2D.collider.GetComponent<Fruit>();
                right.direction = Vector2.right;
                break;
            }
        }
        
        //get random fruit to attack
        var fruits = new[] {top, bottom, left, right};
        //filter nulls
        fruits = Array.FindAll(fruits, fruit => fruit.fruit != null && fruit.fruit.AreDead == false);
        if (fruits.Length == 0)
        {
            Debug.Log("No fruits to attack");
            return;
        }
        var fruitToAttack = fruits[UnityEngine.Random.Range(0, fruits.Length)];
        direction = fruitToAttack.direction;
        _fruitSelected = fruitToAttack.fruit;
    }
    

    public void Touch()
    {
        touched = true;
    }

    public void StartTopo()
    {
        _outOfGround.Play();
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

public class FruitToAttack
{
    public Fruit fruit;
    public Vector2 direction;
}