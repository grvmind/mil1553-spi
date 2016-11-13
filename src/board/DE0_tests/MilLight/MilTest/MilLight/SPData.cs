﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilLight
{
    public class SPData : SPFrame, ISPData
    {
        public override bool IsActual
        {
            get { return base.IsActual && !Data.Exists(a => !((ExpirableObject)a).IsActual); }
        }

        private List<IMilFrame> data = new List<IMilFrame>();
        public List<IMilFrame> Data
        {
            get
            {
                Actualize();
                return data;
            }
            set
            {
                data = value;
                Expire();
            }
        }

        protected override ushort PayloadDataSize()
        {
            return (UInt16)(data.Sum(a => a.Size));
        }

        protected override ushort PayloadCheckSum()
        {
            return (UInt16)(data.Sum(a => a.CheckSum));
        }

        protected override bool PayloadEquals(object obj)
        {
            SPData o = obj as SPData;
            if (o == null)
                return false;

            return Enumerable.SequenceEqual(o.Data, Data);
        }

        protected override int PayloadHashCode()
        {
            return Data.GetHashCode();
        }

        protected override ushort PayloadSerialize(Stream stream)
        {
            ushort size = 0;
            Data.ForEach(a => size += ((IBinaryFrame)a).Serialize(stream));
            return size;
        }

        protected override ushort PayloadDeserialize(Stream stream, ushort payloadSize)
        {
            Data = new List<IMilFrame>();

            for (int i = 0; i < payloadSize;)
            {
                MilFrame mp = new MilFrame();
                UInt16 s = mp.Deserialize(stream);
                i += s;
                Data.Add(mp);
            }

            return payloadSize;
        }
    }
}