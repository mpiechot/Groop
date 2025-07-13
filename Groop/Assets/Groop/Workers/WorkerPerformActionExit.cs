#nullable enable

using UnityEngine;

namespace Groop.Workers
{
    /// <summary>
    /// State machine behaviour that handles the exit of the "Perform Action" animation state for a worker.
    /// </summary>
    public class WorkerPerformActionExit : StateMachineBehaviour
    {
        /// <inheritdoc />
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var worker = animator.GetComponent<Worker>();
            worker.OnPerformActionFinished();
        }
    }
}
