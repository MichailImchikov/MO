using Leopotam.EcsLite;

namespace Client {
    sealed class InitGameState : IEcsInitSystem {
        public void Init (IEcsSystems systems) 
        {
            GameState.Init(systems.GetWorld());
            HistoryManager.Init();
        }
    }
}