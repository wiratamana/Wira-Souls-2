using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tamana
{
    public static class UI
    {
        public static class PS4
        {
            private const string path = "Sprites/GUI - PS4/";

            public static Sprite GetButton(Tamana.PS4.ButtonName buttonName)
            {
                switch (buttonName)
                {
                    case Tamana.PS4.ButtonName.R1:
                        break;
                    case Tamana.PS4.ButtonName.R2:
                        break;
                    case Tamana.PS4.ButtonName.R3:
                        break;
                    case Tamana.PS4.ButtonName.L1:
                        break;
                    case Tamana.PS4.ButtonName.L2:
                        break;
                    case Tamana.PS4.ButtonName.L3:
                        break;
                    case Tamana.PS4.ButtonName.Triangle:
                        return PS4_Triangle;
                    case Tamana.PS4.ButtonName.Circle:
                        return PS4_Circle;
                    case Tamana.PS4.ButtonName.Cross:
                        return PS4_Cross;
                    case Tamana.PS4.ButtonName.Square:
                        return PS4_Square;
                    case Tamana.PS4.ButtonName.Option:
                        break;
                    case Tamana.PS4.ButtonName.Share:
                        break;
                    case Tamana.PS4.ButtonName.TouchPad:
                        break;
                    case Tamana.PS4.ButtonName.PS:
                        break;
                }

                return null;
            }

            private static Sprite _PS4_Circle;
            private static Sprite PS4_Circle
            {
                get
                {
                    if(_PS4_Circle == null)
                    {
                        _PS4_Circle = Resources.Load<Sprite>(path + "PS4_Cirlce");
                    }

                    return _PS4_Circle;
                }
            }

            private static Sprite _PS4_Square;
            private static Sprite PS4_Square
            {
                get
                {
                    if (_PS4_Square == null)
                    {
                        _PS4_Square = Resources.Load<Sprite>(path + "PS4_Cirlce");
                    }

                    return _PS4_Square;
                }
            }

            private static Sprite _PS4_Triangle;
            private static Sprite PS4_Triangle
            {
                get
                {
                    if (_PS4_Triangle == null)
                    {
                        _PS4_Triangle = Resources.Load<Sprite>(path + "PS4_Cirlce");
                    }

                    return _PS4_Triangle;
                }
            }

            private static Sprite _PS4_Cross;
            private static Sprite PS4_Cross
            {
                get
                {
                    if (_PS4_Cross == null)
                    {
                        _PS4_Cross = Resources.Load<Sprite>(path + "PS4_Cirlce");
                    }

                    return _PS4_Cross;
                }
            }
        }
    }
}

