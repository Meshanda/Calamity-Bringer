using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct MoveToPositionAspect : IAspect
{
    private readonly Entity _entity;

    private readonly RefRW<LocalTransform> _transform;

    private readonly RefRO<Speed> _speed;
    private readonly RefRW<TargetPosition> _targetPosition;

    private const float REACHEDTARGETDISTANCE = .5f;

    public void Move(float deltaTime)
    {
        float3 direction = math.normalize(_targetPosition.ValueRW.Value - _transform.ValueRW.Position);

        _transform.ValueRW.Position += direction * deltaTime * _speed.ValueRO.Value;
    }

    public void TestReachedTargetPosition(RefRW<RandomComponent> randomComponent)
    {
        if (math.distance(_transform.ValueRW.Position, _targetPosition.ValueRW.Value) < REACHEDTARGETDISTANCE)
        {
            _targetPosition.ValueRW.Value = GetRandomPosition(randomComponent);
        }
    }

    private float3 GetRandomPosition(RefRW<RandomComponent> randomComponent)
    {
        return new float3(
            randomComponent.ValueRW.Random.NextFloat(-50f, 50f),
            0,
            randomComponent.ValueRW.Random.NextFloat(-20f, 20f));
    }
}
 