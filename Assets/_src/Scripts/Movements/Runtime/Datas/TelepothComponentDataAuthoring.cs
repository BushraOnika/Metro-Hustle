using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class TelepothComponentDataAuthoring : MonoBehaviour
{
    public float3 Position;

    public class TelepothComponentDataBaker : Baker<TelepothComponentDataAuthoring>
    {
        public override void Bake(TelepothComponentDataAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new TelepothComponentData { Position = authoring.Position });
        }
    }
}