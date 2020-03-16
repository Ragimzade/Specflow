using Framework.BaseClasses;

namespace Framework.Utils
{
    public class StepHelper : BaseEntity
    {
        private int _numberOfStep;

        public StepHelper()
        {
            ResetSteps();
        }

        public void LogStep(string message)
        {
            ++_numberOfStep;
            Log.Info($"Step {_numberOfStep}: {message}");
        }

        private void ResetSteps()
        {
            _numberOfStep = 0;
        }
    }
}