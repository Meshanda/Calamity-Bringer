using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct CapsuleAspect : IAspect
{
    public readonly Entity Entity;

    public readonly RefRW<CapsuleTag> capTag;
    private readonly RefRW<LocalTransform> _transform;
    //private readonly RefRW<LocalTransform> _rotation;
    private LocalTransform Transform => _transform.ValueRO;
    // Start is called before the first frame update
    public void SetPos(float3 pos, quaternion quat) 
    {
        _transform.ValueRW.Position = pos;
        _transform.ValueRW.Rotate(quat);
    }
}
