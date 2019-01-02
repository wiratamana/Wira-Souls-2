using UnityEngine;
using System.Collections;

namespace Tamana
{
    public class Status : MonoBehaviour
    {
        private Stat1 _hp;
        private Stat1 _mp;
        private Stat1 _st;

        private float _atk;
        private float _def;

        public void SetUp(Vector3 hpmpst, Vector2 atkdef)
        {
            _hp = new Stat1(hpmpst.x);
            _mp = new Stat1(hpmpst.y);
            _st = new Stat1(hpmpst.z);

            _atk = atkdef.x;
            _def = atkdef.y;
        }
    }

    public struct Stat1
    {
        public float cur { private set; get; }
        public float max { private set; get; }

        public Stat1(float max)
        {
            cur = max;
            this.max = max;
        }

        public Stat1(float cur, float max)
        {
            this.cur = cur;
            this.max = max;
        }

        public static Stat1 operator+ (Stat1 a, float b)
        {
            return new Stat1(Mathf.MoveTowards(a.cur, 0, b), a.max);
        }
        public static Stat1 operator -(Stat1 a, float b)
        {
            return new Stat1(Mathf.MoveTowards(a.cur, a.max, b), a.max);
        }
    }
}