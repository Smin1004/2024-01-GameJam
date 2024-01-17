using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mob_Base : MonoBehaviour
{
    protected Animator anim;
    public Action dieAction;
    public Action destoryAction;
    public Vector2Int curPos;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        dieAction += DieDestroy;
        dieAction += destoryAction;
    }
    private void OnDestroy()
    {
        destoryAction?.Invoke();
    }
    protected abstract void DieDestroy();
}
