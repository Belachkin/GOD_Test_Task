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
            throw new System.NotImplementedException();
        }

        public void Updater()
        {
            throw new System.NotImplementedException();
        }

        public void FixedUpdater()
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}