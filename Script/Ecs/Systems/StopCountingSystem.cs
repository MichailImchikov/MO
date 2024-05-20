using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using static HistoryManager;
using System.Collections.Generic;
using TMPro;

namespace Client {
    sealed class StopCountingSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<NelderComponent, PointComponent,StopCountingEvent>> _filter1 = default;
        readonly EcsPoolInject<UpdatePointEvent> _updatePointComponent = default;
        readonly EcsPoolInject<PointComponent> _pointComponent = default;
        readonly EcsPoolInject<NelderComponent> _nelderComponent = default;
        readonly EcsPoolInject<StopCountingEvent> _stopCountingEvent = default;
        readonly EcsPoolInject<TestComponent> _testComponent = default;
        public void Run (IEcsSystems systems) 
        {
            foreach(var entity in _filter1.Value)
            {
                ref var stopCountingEvent = ref _stopCountingEvent.Value.Get(entity);
                if(stopCountingEvent.exception!="")
                {
                    ref var nelderComponent = ref _nelderComponent.Value.Get(entity);
                    GameState.Instance.answer.text = stopCountingEvent.exception;
                    HistoryManager.Save(new Data.Example(nelderComponent.expression.GetExpression(), stopCountingEvent.exception));
                }
                else
                {
                    ref var pointComponent= ref _pointComponent.Value.Get(entity);
                    ref var nelderComponent= ref _nelderComponent.Value.Get(entity);
                    var resultPoint = pointComponent._lowPoint.GetCoarseningPoint();
                    var resultExpression = nelderComponent.expression.Calculation(resultPoint);
                    var answer = resultPoint.ToString(resultExpression.ToString());
                    GameState.Instance.answer.text = answer;
                    
                    
                    HistoryManager.Save(new Data.Example(nelderComponent.expression.GetExpression(), answer));
                    if (_testComponent.Value.Has(entity))
                    {
                        _testComponent.Value.Get(entity).Result =resultExpression;
                    }
                } 
                if (_updatePointComponent.Value.Has(entity)) 
                {
                    _updatePointComponent.Value.Del(entity); 
                }                    
                _nelderComponent.Value.Del(entity);
                _pointComponent.Value.Del(entity);
                _stopCountingEvent.Value.Del(entity);
            }
        }
    }
}