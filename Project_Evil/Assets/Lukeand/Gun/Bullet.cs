using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    int collisionsTotal;
    int collisionsCurrent;
    bool hasLayer;
    int[] AllowedLayers;
    [SerializeField]LayerMask layer;
    Vector3 dir;
    float damage;
    string shooterID;
    float speed;

    public void SetUp(string shooterID, Vector3 dir, float damage, float speed)
    {
        this.shooterID = shooterID;
        this.dir = dir;
        this.damage = damage;
        this.speed = speed;
    }

    public void SetDestroySelf(float distanceAllowed)
    {
        KillSelf self = gameObject.AddComponent<KillSelf>();

        self.SetUpTimer(30);
        self.SetUpDistance(transform.position, distanceAllowed);
    }

    public void MakeCollision(int quantity)
    {
        collisionsTotal = quantity;
    }

    public void MakeLayer(int[] layerIndexes)
    {
        AllowedLayers = layerIndexes;
        hasLayer = true;
        foreach (var item in layerIndexes)
        {
            layer |= (1 << item);
        }
    }

    private void FixedUpdate()
    {
        transform.position += dir * 0.01f * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == null) return;
        HandleCollision(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == null) return;
        HandleCollision(collision.gameObject);
    }

    void HandleCollision(GameObject collision)
    {
        if (hasLayer)
        {
            if (!IsRightLayer(collision.gameObject.layer))
            {
                Debug.Log("not right layer");
                return;
            }
        }

        EntityDamageable damageable = collision.GetComponent<EntityDamageable>();

        if (damageable == null)
        {
            return;
        }

        if (damageable.id == shooterID)
        {
            return;
        }

        damageable.TakeDamage(damage);

        collisionsCurrent += 1;

        if(collisionsCurrent >= collisionsTotal)
        {
            Destroy(gameObject);
        }

    }


    bool IsRightLayer(int collisionLayer)
    {
        foreach (var item in AllowedLayers)
        {
            if (collisionLayer == item) return true;
        }


        return false;
    }

}
