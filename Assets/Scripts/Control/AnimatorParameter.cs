﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public class AnimatorParameter
    {
        private Animator animator;

        public AnimatorParameter(Animator animator)
        {
            this.animator = animator;
        }

        public float AnalogLeftX
        {
            get { return animator.GetFloat("AnalogLeftX"); }
            set { animator.SetFloat("AnalogLeftX", value); }
        }
        public float AnalogLeftY
        {
            get { return animator.GetFloat("AnalogLeftY"); }
            set { animator.SetFloat("AnalogLeftY", value); }
        }

        public bool isMoving
        {
            get { return animator.GetBool("isMoving"); }
            set { animator.SetBool("isMoving", value); }
        }

        public bool isLeftAnalogMoving
        { 
            get { return animator.GetBool("isLeftAnalogMoving"); }
            set { animator.SetBool("isLeftAnalogMoving", value); }
        }

        public bool isAttacking
        {
            get { return animator.GetBool("isAttacking"); }
            set { animator.SetBool("isAttacking", value); }
        }

        public bool isStopMovingL
        {
            get { return animator.GetBool("isStopMovingL"); }
            set { animator.SetBool("isStopMovingL", value); }
        }

        public bool isSword1h_Equip
        {
            get { return animator.GetBool("isSword1h_Equip"); }
            set { animator.SetBool("isSword1h_Equip", value); }
        }

        public bool isHolsterOrEquip
        {
            get { return animator.GetBool("isHolsterOrEquip"); }
            set { animator.SetBool("isHolsterOrEquip", value); }
        }
    }
}

