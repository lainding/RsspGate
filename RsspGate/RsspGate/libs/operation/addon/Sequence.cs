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
            private ulong _count;
            private ulong _start = 0;
            private ulong _end = long.MaxValue;
            private long _step = 1;
            private bool _isloop = true;

            public void SetIsLoop(bool isloop)
            {
                this._isloop = isloop;
            }

            public void SetStart(ulong start)
            {
                this._start = start;
                this._count = start;
            }
            public void SetEnd(ulong end)
            {
                this._end = end;
            }
            public void SetStep(long step)
            {
                this._step = step;
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
                    SetEnd(Convert.ToUInt64(type.GetField("MaxValue").GetValue(null)));
                }
            }

            public ulong Increase()
            {
                var last = _count;
                if (_isloop)
                {
                    if (_step > 0)
                    {
                         if (_count + Convert.ToUInt64(Math.Abs(_step)) > _end)
                        {
                            _count = _start + (_end - _count + Convert.ToUInt64(Math.Abs(_step)) - 1);
                        }
                        else
                        {
                            _count = _count + Convert.ToUInt64(Math.Abs(_step));
                        }
                    }
                    else
                    {
                        if (_count == 0 && (_count - Convert.ToUInt64(Math.Abs(_step))) > long.MaxValue)
                        {
                            _count = _end + (_count - _start - Convert.ToUInt64(Math.Abs(_step)) + 1);
                        }
                        else
                        {
                            _count = _count - Convert.ToUInt64(Math.Abs(_step));
                        }
                    }
                }
                else
                {
                    if (_step > 0)
                    {
                        if (_count - Convert.ToUInt64(Math.Abs(_step)) > _end)
                        {
                            _count = _end;
                        }
                        else
                        {
                            _count = _count + Convert.ToUInt64(Math.Abs(_step));
                        }
                    }
                    else
                    {
                        if (_count - Convert.ToUInt64(Math.Abs(_step)) < _start)
                        {
                            _count = _start;
                        }
                        else
                        {
                            _count = _count - Convert.ToUInt64(Math.Abs(_step));
                        }
                    }
                }
                return last;
            }

            
        }

        private Counter counter = null;
        private static string[] AvaliableType = { "uint", "ulong", "ushort", "byte" };

        private ulong start = 0;
        private ulong end = ulong.MaxValue;
        private Type type = TypeTable["uint"];

        public Sequence(dynamic data)
        {

            if (data.type())
            {
                string t = ((string)data.type).Trim().ToLower();
                if (AvaliableType.Contains(t))
                {
                    SetType(data.type);
                    this.type = TypeTable[data.type];
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


            counter = new Counter(this.type);

            if (data.loop() && data.loop.GetType().Name == "Boolean")
            {
                counter.SetIsLoop(data.loop);
            }

            if (data.start() && data.start.GetType().Name == "Double")
            {
                if (data.start < 0)
                {
                    throw new config.ConfigErrorException("Sequence start must bigger than 0.");
                }
                counter.SetStart(Convert.ToUInt64(data.start));
            }
            else
            {
                counter.SetStart(0);
            }

            if (data.end() && data.end.GetType().Name == "Double")
            {
                System.Reflection.FieldInfo f = this.type.GetField("MaxValue");
                if (f != null)
                {
                    ulong typemax = Convert.ToUInt64(f.GetValue(null));
                    if ((ulong)data.end > typemax)
                    {
                        throw new config.ConfigErrorException("Sequence end is bigger than type define maxmium.");
                    }
                    else
                    {
                        counter.SetEnd(Convert.ToUInt64(data.end));
                    }
                }
            }

            if (data.step() && data.step.GetType().Name == "Double")
            {
                counter.SetStep(Convert.ToInt64(data.step));
            }
            else
            {
                counter.SetStep(1);
            }


        }
        public override int GetLength()
        {
            return this.length;
        }

        public override byte[] GetValue()
        {
            var c = counter.Increase();
            switch (this.type.Name)
            {
                case "Byte":
                    SetValue((Byte)c);
                    break;
                case "UInt32":
                    SetValue((UInt32)c);
                    break;
                case "UInt16":
                    SetValue((UInt16)c);
                    break;
                case "UInt64":
                    SetValue((UInt64)c);
                    break;
                default:
                    SetValue((UInt32)c);
                    break;
            }
            return this.value;
        }
    }
}
