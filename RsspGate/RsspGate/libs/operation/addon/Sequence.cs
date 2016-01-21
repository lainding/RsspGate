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
            private long _count;
        }
        private bool IsLoop = true;
        private bool IsIncrease = true;
        private static string[] AvaliableType = { "uint", "ulong", "ushort" };

        private ulong start = 0;
        private ulong end = ulong.MaxValue;

        public Sequence(dynamic data)
        {
            if (data.loop() && data.loop.GetType().Name == "Boolean")
            {
                IsLoop = data.loop;
            }

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

            if (data.direction() && data.direction.GetType().Name == "String")
            {
                string d = ((string)data.direction).Trim().ToLower();
                if (d == "increase")
                {
                    this.IsIncrease = true;
                }
                if (d == "decrease")
                {
                    this.IsIncrease = false;
                }
            }

            if (data.start() && data.start.GetType.Name == "Double")
            {
                start = (ulong)data.start;
            }

            if (data.end() && data.end.GetType.Name == "Double")
            {
                end = (ulong)data.end;
            }

        }
        public override int GetLength()
        {
            throw new NotImplementedException();
        }

        public override byte[] GetValue()
        {
            throw new NotImplementedException();
        }
    }
}
