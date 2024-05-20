using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using TMPro.Examples;

namespace Client {
    sealed class ReductionSystem : IEcsRunSystem 
    {
        readonly EcsFilterInject<Inc<NelderComponent, PointComponent,ReductionEvent>, Exc<UpdatePointEvent>> _filter1 = default;
        readonly EcsPoolInject<PointComponent> _pointComponent = default;
        readonly EcsPoolInject<ReductionEvent> _reductionEvent = default;
        readonly EcsPoolInject<UpdatePointEvent> _updatePoint = default;
        public void Run (IEcsSystems systems) {
            foreach(var entity in _filter1.Value)
            {
                ref var pointComponent = ref _pointComponent.Value.Get(entity);
                for (int i = 0; i < pointComponent.Polyhedron.Count; i++)
                {
                    pointComponent.Polyhedron[i] = pointComponent._lowPoint + 0.5f * (pointComponent.Polyhedron[i] - pointComponent._lowPoint);
                }
                if (GameState.Instance.ecsWorld.GetPool<TestComponent>().Has(entity))
                {
                    GameState.Instance.ecsWorld.GetPool<TestComponent>().Get(entity)._type = TestType.Reduction;
                }
                _reductionEvent.Value.Del(entity);
                _updatePoint.Value.Add(entity);
            }
        }
    }
}