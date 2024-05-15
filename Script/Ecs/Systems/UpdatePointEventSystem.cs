using JetBrains.Annotations;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System;
using System.Collections.Generic;

namespace Client {
    sealed class UpdatePointEventSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<NelderComponent,UpdatePointEvent,PointComponent>,Exc<StopCountingEvent>> _filter1 = default;
        readonly EcsPoolInject<UpdatePointEvent> _updatePointComponent = default;
        readonly EcsPoolInject<PointComponent> _pointComponent = default;
        readonly EcsPoolInject<NelderComponent> _nelderComponent = default;
        readonly EcsPoolInject<StopCountingEvent> _stopCountingEvent = default;
        public void Run (IEcsSystems systems) 
        {
            foreach(var entity in _filter1.Value)
            {
                ref var pointComponent = ref _pointComponent.Value.Get(entity);
                pointComponent.InitPoint();
                ref var nelderComponent=ref _nelderComponent.Value.Get(entity);                
                ref var updatePointEvent = ref _updatePointComponent.Value.Get(entity);
                if(updatePointEvent.NewPoint is not null&&updatePointEvent.Replacement is not null)
                {
                    int index=pointComponent.Polyhedron.IndexOf(updatePointEvent.Replacement);
                    pointComponent.Polyhedron[index]=updatePointEvent.NewPoint;
                }
                pointComponent.Polyhedron=ShellSort(pointComponent.Polyhedron, nelderComponent.expression);
                pointComponent._lowPoint = pointComponent.Polyhedron[0];
                var canvas=GameState.Instance.GetCanvas<ChartCanvasMB>()as ChartCanvasMB;
                var pointHistory = pointComponent._lowPoint.GetCoarseningPoint();
                canvas.SetPoint(pointHistory, nelderComponent.expression.Calculation(pointHistory));
                pointComponent._middlePoint = pointComponent.Polyhedron[1];
                pointComponent._highPoint = pointComponent.Polyhedron[GameState.Instance.CountVariables];
                foreach (var point in pointComponent.Polyhedron)
                {
                    if(point!=pointComponent._highPoint)
                    {
                        pointComponent._centerPoint += point;
                    }
                }
                pointComponent._centerPoint /= GameState.Instance.CountVariables;
                float sum = 0;
                foreach(var point in pointComponent.Polyhedron)
                {
                    sum+= MathF.Pow((nelderComponent.expression.Calculation(point) - nelderComponent.expression.Calculation(pointComponent._lowPoint)),2);
                }
                sum = MathF.Sqrt(sum / (GameState.Instance.CountVariables + 1));
                if (sum<0.0005)
                {
                    if (!GameState.Instance.ecsWorld.GetPool<StopCountingEvent>().Has(entity))
                    {
                        GameState.Instance.ecsWorld.GetPool<StopCountingEvent>().Add(entity).exception = "";
                    }
                }
                _updatePointComponent.Value.Del(entity);
            }
            
        }
        public List<Point> ShellSort(List<Point> array,MathExpression math)
        {
            for (int interval = array.Count / 2; interval > 0; interval /= 2)
            {
                for (int i = interval; i < array.Count; i++)
                {
                    var currentKey = array[i];
                    var k = i;
                    while (k >= interval && math.Calculation(array[k - interval]) > math.Calculation(currentKey))
                    {
                        array[k] = array[k - interval];
                        k -= interval;
                    }
                    array[k] = currentKey;
                }
            }
            return array;
        }
    }
}