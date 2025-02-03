using UnityEngine;

namespace Code.Enemy.States
{
    public class DeadEnemyState : IEnemyState
    {
        private Animator _animator;
        private EnemyStateMashine _stateMashine;

        public DeadEnemyState(EnemyStateMashine stateMashine, Animator animator)
        {
            _stateMashine = stateMashine;
            _animator = animator;
        }
        
        public void Enter()
        {
            _animator.SetTrigger("Dead");
        }

        public void Updater()
        {
            
        }

        public void FixedUpdater()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}