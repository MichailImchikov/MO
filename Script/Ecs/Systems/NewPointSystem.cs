using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Collections.Generic;
namespace Client {
    sealed class NewPointSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<NelderComponent>,Exc<PointComponent>> _filter1 = default;
        readonly EcsPoolInject<PointComponent> _pointComponent = default;
        readonly EcsPoolInject<NelderComponent> _nelderComponent = default;
        readonly EcsPoolInject<UpdatePointEvent> _updateEvent = default;
        public void Run (IEcsSystems systems) 
        {
            foreach(var entity in _filter1.Value)
            {
                var _world = systems.GetWorld();
                ref var pointComponent=ref _pointComponent.Value.Add(entity);
                ref var nelderComponent = ref _nelderComponent.Value.Get(entity);
                pointComponent.Polyhedron = new();
                for (int i = 0; i < GameState.Instance.CountVariables + 1; i++)
                {
                    pointComponent.Polyhedron.Add(new Point(-100f, 100f));
                }
                _updateEvent.Value.Add(entity);
            }
        }
    }
}