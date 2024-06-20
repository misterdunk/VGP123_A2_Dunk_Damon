using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyTurret : Enemy
{
    [SerializeField] private float projectileFireRate = 0;
    [SerializeField] private Transform player;
    public Transform playertransform;
    private float timeSinceLastFire = 0;
    private bool ifInRange;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        if (projectileFireRate <= 0)
            projectileFireRate = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
