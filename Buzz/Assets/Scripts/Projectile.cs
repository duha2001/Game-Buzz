﻿using UnityEngine;
using System.Collections;

public abstract class Projectile : MonoBehaviour
{
    public float Speed;
    public LayerMask CollisionMask;


    public GameObject Owner { get; private set; }
    public Vector2 Direction { get; private set; }
    public Vector2 InitialVarlocity { get; private set; }

    public void Initialize(GameObject owner, Vector2 direction, Vector2 initialVarlocity)
    {
        transform.right = direction;

        Owner = owner;
        Direction = direction;
        InitialVarlocity = initialVarlocity;
        OnInitialized();
    }

    protected virtual void OnInitialized()
    {

    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        //Layer #   Binary      Decimal
        //Layer 0 = 0000 0001 = 1
        //Layer 1 = 0000 0010 = 2
        //Layer 2 = 0000 0100 = 4
        //Layer 3 = 0000 1000 = 8
        //Layer 4 = 0001 0000 = 16
        //Layer 5 = 0010 0000 = 32
        //Layer 6 = 0100 0000 = 64
        //Layer 7 = 1000 0000 = 128

        // Layer Mask = 0110 0110
        // is layer 5 in the mask

        // (1 << 5)
        // 0000 0001 << 5 = 0010 0000

        // 0110 0110
        // & (and)
        // 0010 0000
        // ---------
        // 0010 0000

        // 0110 0110
        // & (and)
        // 1000 0000
        //----------
        // 0000 0000

        if ((CollisionMask.value & (1 << other.gameObject.layer)) == 0)
        {
            OnNotCollideWith(other);
            return;
        }

        var isOwner = other.gameObject == Owner;
        if(isOwner)
        {
            OnCollideOwner();
            return;
        }

        //other.GetComponennt<ITakeDamage>
        var takeDamage = (ITakeDamage)other.GetComponent(typeof(ITakeDamage));
        if(takeDamage != null)
        {
            OnCollideTakeDamage(other, takeDamage);
            return;
        }

        OnCollideOther(other);
    }

    protected virtual void OnNotCollideWith(Collider2D other)
    {

    }

    protected virtual void OnCollideOwner()
    {

    }

    protected virtual void OnCollideTakeDamage(Collider2D other, ITakeDamage takedamage)
    {

    }

    protected virtual void OnCollideOther(Collider2D other)
    {

    }

}
