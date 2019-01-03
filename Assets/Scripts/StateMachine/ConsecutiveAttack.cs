using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class ConsecutiveAttack : StateMachineBehaviour
    {
        [SerializeField] private float _startTime;
        [SerializeField] private float _endTime;

        private Attacking atk { get { return GM.PlayerController.attacking; } }
        private bool active = true;
        private bool start = false;

        private float startTime;
        private float endTime;

        private System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            sw.Restart();

            active = true;
            start = false;

            startTime = _startTime * (1 / (stateInfo.speedMultiplier));
            endTime   = _endTime   * (1 / (stateInfo.speedMultiplier));
        }
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)  => sw.Stop();

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!active)
                return;

            float elapsedSeconds = sw.ElapsedMilliseconds / 1000.0f;
            if (!start && elapsedSeconds >= startTime)
            {
                start = true;
                atk.canDoConsecutiveAttack = true;
            }
            else if (elapsedSeconds > endTime)
            {
                active = false;
                atk.canDoConsecutiveAttack = false;
            }
        }
    }
}