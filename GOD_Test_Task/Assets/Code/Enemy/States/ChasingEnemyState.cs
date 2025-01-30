using UnityEngine;

namespace Code.Enemy.States
{
    public class ChasingEnemyState : IEnemyState
    {
        private EnemyStateMashine _stateMashine;
        public ChasingEnemyState(EnemyStateMashine stateMashine)
        {
            _stateMashine = stateMashine;
        }
        
        public void Enter()
        {
            Debug.Log("EnterChasingState");
        }

        public void Updater()
        {
            
        }

        public void FixedUpdater()
        {
            
        }

        public void Exit()
        {
            Debug.Log("ExitChasingState");
        }
    }
}