using System.Text;
using Unity.Burst;
using Unity.Entities;

namespace _src.Scripts.Movements.Runtime.Systems
{
    public partial struct TelepothSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var telepothJobEntity = new TelepothJobEntity();
            telepothJobEntity.ScheduleParallel();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}