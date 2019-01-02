using UnityEngine;
using System.Collections;

namespace Tamana
{
    public static class PS4
    {
        public enum ButtonName
        {
            R1, R2, R3,
            L1, L2, L3,
            Triangle, Circle, Cross, Square,
            Option,
            Share,
            TouchPad,
            PS
        }

        public enum AxisName
        {
            AnalogLeft_X, AnalogLeft_Y,
            AnalogRight_X, AnalogRight_Y,
            DPad_X, DPad_Y,
            R2, L2,
        }

        public static bool GetButtonDown(ButtonName buttonName) => Input.GetButtonDown(GetButtonName(buttonName));
        public static bool GetButtonUp(ButtonName buttonName) => Input.GetButtonUp(GetButtonName(buttonName));
        public static bool GetButton(ButtonName buttonName) => Input.GetButton(GetButtonName(buttonName));

        public static float GetAxis(AxisName axisName) => Input.GetAxis(GetAxisName(axisName));

        private static string GetButtonName(ButtonName buttonName)
        {
            switch (buttonName)
            {
                case ButtonName.R1:
                    return "R1";
                case ButtonName.R2:
                    return "R2 as Button";
                case ButtonName.R3:
                    return "R3";
                case ButtonName.L1:
                    return "L1";
                case ButtonName.L2:
                    return "L2 as Button";
                case ButtonName.L3:
                    return "L3";
                case ButtonName.Triangle:
                    return "Triangle Button";
                case ButtonName.Circle:
                    return "Circle Button";
                case ButtonName.Cross:
                    return "Cross Button";
                case ButtonName.Square:
                    return "Square Button";
                case ButtonName.Option:
                    return "Option Button";
                case ButtonName.Share:
                    return "Share Button";
                case ButtonName.TouchPad:
                    return "Touchpad Click";
                default:
                    return "PS Button";
            }
        }
        private static string GetAxisName(AxisName axisName)
        {
            switch (axisName)
            {
                case AxisName.AnalogLeft_X:
                    return "Left Stick X-Axis";
                case AxisName.AnalogLeft_Y:
                    return "Left Stick Y-Axis";
                case AxisName.AnalogRight_X:
                    return "Right Stick X-Axis";
                case AxisName.AnalogRight_Y:
                    return "Right Stick Y-Axis";
                case AxisName.DPad_X:
                    return "DPAD X-Axis";
                case AxisName.DPad_Y:
                    return "DPAD Y-Axis";
                case AxisName.R2:
                    return "R2 as Axis";
                case AxisName.L2:
                    return "L2 as Axis";
                default:
                    return "";
            }

        }
    }
}