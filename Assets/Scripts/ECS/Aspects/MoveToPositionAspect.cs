using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
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
    private readonly RefRO<MovementZoneIndex> _moveZoneIndex;
    
    private const float REACHEDTARGETDISTANCE = .5f;

    public void Move(float deltaTime)
    {
        float3 direction = math.normalize(_targetPosition.ValueRW.Value - _transform.ValueRW.Position);

        _transform.ValueRW.Position += direction * deltaTime * _speed.ValueRO.Value;
    }

    public void TestReachedTargetPosition(RefRW<RandomComponent> randomComponent, NativeArray<PersonZone> zoneList)
    {
        if (math.distance(_transform.ValueRW.Position, _targetPosition.ValueRW.Value) < REACHEDTARGETDISTANCE)
        {
            _targetPosition.ValueRW.Value = GetDestinationPosition(randomComponent, zoneList);
        }
    }

    private float3 GetDestinationPosition(RefRW<RandomComponent> randomComponent, NativeArray<PersonZone> zoneList)
    {
        PersonZone zone = zoneList[_moveZoneIndex.ValueRO.MovementIndex];

        float startPosX = randomComponent.ValueRW.Random.NextFloat(zone.SpawnCenterZone.x - (zone.SizeXZone / 2), zone.SpawnCenterZone.x + (zone.SizeXZone / 2));
        float startPosZ = randomComponent.ValueRW.Random.NextFloat(zone.SpawnCenterZone.z - (zone.SizeZZone / 2), zone.SpawnCenterZone.z + (zone.SizeZZone / 2));

        return new float3(startPosX, 0, startPosZ);
    }
}
 