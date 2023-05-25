using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public readonly partial struct BuildingAspect : IAspect
{
        public readonly Entity Entity;

        private readonly RefRW<BuildingTag> Tag;

}