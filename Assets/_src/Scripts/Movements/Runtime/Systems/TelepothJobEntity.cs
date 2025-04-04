using Unity.Entities;
using Unity.Transforms;
using UnityEngine.UIElements;

namespace _src.Scripts.Movements.Runtime.Systems
{
    public partial struct TelepothJobEntity : IJobEntity
    {
        public void Execute(in TelepothComponentData telepothComponentData,ref LocalTransform localTransform)
        {
            localTransform.Position = telepothComponentData.Position;

        }
    }
}