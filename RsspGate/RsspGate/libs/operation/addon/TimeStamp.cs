﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsspGate.libs.operation.addon
{
    class TimeStamp : Addon
    {
        public TimeStamp(dynamic data)
        {
            SetLength(4);
            SetType("int");
            if (data.endian())
            {
                SetEndian(data.endian);
            }
        }
        public override int GetLength()
        {
            return length;
        }

        private int UnixTimeNow()
        {
            var timeSpan = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0));
            return (int)timeSpan.TotalSeconds;
        }

        public override byte[] GetValue()
        {
            SetValue(UnixTimeNow());
            return value;
        }
    }
}