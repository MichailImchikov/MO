using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Collections.Generic;
using static UnityEngine.EventSystems.EventTrigger;

namespace Client {
    sealed class NelderMethodSystem : IEcsRunSystem 
    {
        readonly EcsFilterInject<Inc<NelderComponent,PointComponent>,Exc<UpdatePointEvent,StopCountingEvent>> _filter1 = default;
        readonly EcsPoolInject<PointComponent> _pointComponent = default;
        readonly EcsPoolInject<NelderComponent> _nelderComponent = default;
        readonly EcsPoolInject<UpdatePointEvent> _updatePoint = default;
        readonly EcsPoolInject<ReductionEvent> _reductionEvent = default;
        public void Run (IEcsSystems systems) 
        {
            foreach(var entity in _filter1.Value)
            {
                ref var pointComponent= ref _pointComponent.Value.Get(entity);
                ref var nelderComponent=ref _nelderComponent.Value.Get(entity);
                var _reflectionPoint = pointComponent._centerPoint + nelderComponent._reflectionRatio * (pointComponent._centerPoint - pointComponent._highPoint);
                pointComponent._reflectionPoint = _reflectionPoint;
                if (nelderComponent.expression.Calculation(_reflectionPoint) < nelderComponent.expression.Calculation(pointComponent._lowPoint))
                {
                    if (!CreatingStretchingPoint(pointComponent, nelderComponent,entity))
                    { 
                        ref var updatePoint=ref _updatePoint.Value.Add(entity);
                        updatePoint.Replacement = pointComponent._highPoint;
                        updatePoint.NewPoint = _reflectionPoint;
                    }
                }
                else
                {
                    if (nelderComponent.expression.Calculation(pointComponent._middlePoint) < nelderComponent.expression.Calculation(_reflectionPoint) && nelderComponent.expression.Calculation(_reflectionPoint) <= nelderComponent.expression.Calculation(pointComponent._highPoint))
                    {
                        Point compressionPoint = pointComponent._centerPoint + nelderComponent._compressionRatio * (pointComponent._highPoint - pointComponent._centerPoint);
                        ref var updatePoint = ref _updatePoint.Value.Add(entity);
                        updatePoint.Replacement = pointComponent._highPoint;
                        updatePoint.NewPoint = compressionPoint;
                        return;
                    }
                    if (nelderComponent.expression.Calculation(pointComponent._lowPoint) < nelderComponent.expression.Calculation(_reflectionPoint) && nelderComponent.expression.Calculation(_reflectionPoint) <= nelderComponent.expression.Calculation(pointComponent._middlePoint))
                    {
                        ref var updatePoint = ref _updatePoint.Value.Add(entity);
                        updatePoint.Replacement = pointComponent._highPoint;
                        updatePoint.NewPoint = _reflectionPoint;
                        return;
                    }
                    _reductionEvent.Value.Add(entity);
                }
            }
        }
        public bool CreatingStretchingPoint(PointComponent pointComponent,NelderComponent nelderComponent,int entity)
        {
            Point stretchingPoint = pointComponent._centerPoint + nelderComponent._stretchingRatio * (pointComponent._reflectionPoint - pointComponent._centerPoint);
            if (nelderComponent.expression.Calculation(stretchingPoint) < nelderComponent.expression.Calculation(pointComponent._lowPoint))
            {
                ref var updatePoint = ref _updatePoint.Value.Add(entity);
                updatePoint.Replacement = pointComponent._highPoint;
                updatePoint.NewPoint = stretchingPoint;
                return true;
            }
            return false;
        }
    }
}