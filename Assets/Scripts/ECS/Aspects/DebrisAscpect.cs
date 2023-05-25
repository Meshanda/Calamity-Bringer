using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;


public readonly partial struct DebrisAscpect : IAspect
{
    public readonly Entity Entity;

    private readonly RefRW<DebrisExplosion> Debri;

    private readonly RefRW<LocalTransform> Transform;
    //private readonly RefRW<PhysicsMass> CurrentMass;
    private readonly RefRW<PhysicsVelocity> Velocity;

    public void Launch( float3 origin) 
    {

        if (Debri.ValueRO.launched)
            return;

        Debri.ValueRW.launched = true;
        Velocity.ValueRW.Linear = math.normalize((Transform.ValueRO.Position - origin)) * 50;

    }

    public void AddGravity(float deltaTime)
    {
        if (!Debri.ValueRO.launched)
            return;


        float3 gravity = new float3(0f,-9.81f,0f);


        Velocity.ValueRW.Linear += gravity* deltaTime * 10f;

    }
}