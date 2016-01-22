using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsspGate.libs.operation.addon
{
    class Sequence : Addon
    {
        class Counter
        {
            private uint _count;
            private long _start = 0;
            private long _end = long.MaxValue;
            private int _step = 1;
            private bool _isloop = true;

            public void SetIsLoop(bool isloop)
            {
                this._isloop = isloop;
            }

            public void SetStart(long start)
            {
                this._start = start;
            }
            public void SetEnd(long end)
            {
                this._end = end;
            }

            public Counter(Type type)
            {
                SetStart(0);
                if (type.GetField("MaxValue") == null)
                {
                    throw new config.ConfigErrorException("error sequence type.");
                }
                else
                {
                    SetEnd((long)(type.GetField("MaxValue").GetValue(null)));
                }
            }

        }

        private Counter counter = null;
        private static string[] AvaliableType = { "uint", "ulong", "ushort" };

        private ulong start = 0;
        private ulong end = ulong.MaxValue;

        public Sequence(dynamic data)
        {

            if (data.type())
            {
                string t = ((string)data.type).Trim().ToLower();
                if (AvaliableType.Contains(t))
                {
                    SetType(data.type);
                }
                else
                {
                    throw new config.ConfigErrorException("Sequence only avaliable uint, ulong, ushort type.");
                }
            }
            else
            {
                SetType("uint");
            }


            counter = new Counter(TypeTable[this.type]);

            if (data.loop() && data.loop.GetType().Name == "Boolean")
            {
                counter.SetIsLoop(data.loop);
            }

            if (data.start() && data.start.GetType.Name == "Double")
            {
                if (data.start < 0)
                {
                    throw new config.ConfigErrorException("Sequence start must bigger than 0.");
                }
                counter.SetStart(data.start);
            }

            if (data.end() && data.end.GetType.Name == "Double")
            {
                System.Reflection.FieldInfo f = TypeTable[this.type].GetField("MaxValue");
                if (f != null)
                {
                    long typemax = (long)(f.GetValue(null));
                    if ((long)data.end > typemax)
                    {
                        throw new config.ConfigErrorException("Sequence end is bigger than type define maxmium.");
                    }
                    else
                    {
                        counter.SetEnd(data.end);
                    }
                }
            }

            if (data.step() && data.direction.GetType().Name == "Double")
            {
            }


        }
        public override int GetLength()
        {
            return this.length;
        }

        public override byte[] GetValue()
        {
            throw new NotImplementedException();
        }
    }
}
