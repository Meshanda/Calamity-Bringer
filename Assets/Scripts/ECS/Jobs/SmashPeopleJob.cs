using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[BurstCompile]
public partial struct SmashPeopleJob : IJobEntity
{
    public EntityCommandBuffer EntityCommandBuffer;
    public float3 PlayerPosition;
    
    public void Execute(PersonAspect personAspect)
    {
        // Check if player is too close
        float3 position = personAspect.GetAspectPosition();

        if(math.distancesq(PlayerPosition, position) < 200)
        {
            EntityCommandBuffer.DestroyEntity(personAspect.GetAspectEntity());
            // Debug.Log("DESTROY");
        }
    }
}
