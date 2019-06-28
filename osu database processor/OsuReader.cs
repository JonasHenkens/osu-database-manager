using osu_database_processor.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace osu_database_processor
{
    class OsuReader : BinaryReader
    {
        public OsuReader(Stream input) : base(input) { }

        public void PrintPosition()
        {
            Console.WriteLine(BaseStream.Position);
        }

        public ulong ReadULEB128()
        {
            ulong result = 0;
            int shift = 0;
            while (true)
            {
                byte byteVal = ReadByte();
                result |= (ulong)(0x7F & byteVal) << shift;
                if ((0x80 & byteVal) == 0) break;
                shift += 7;
            }
            return result;
        }

        public override string ReadString()
        {
            byte byteVal = ReadByte();
            switch (byteVal)
            {
                case 0x0b:
                    return base.ReadString();
                case 0x00:
                    return null;
            }
            throw new InvalidDataException("ReadString: first byte wrong @" + (BaseStream.Position - 1));
        }

        public ModsDoublePair ReadIntDoublePair()
        {
            if (!ReadByte().Equals(0x08)) throw new InvalidDataException("ReadIntDoublePair: first byte wrong @" + (BaseStream.Position - 1));
            Mods mods = (Mods)ReadInt32();
            if (!ReadByte().Equals(0x0d)) throw new InvalidDataException("ReadIntDoublePair: second byte wrong @" + (BaseStream.Position - 1));
            double doubleValue = ReadDouble();
            return new ModsDoublePair(mods, doubleValue);
        }

        public Timingpoint ReadTimingpoint()
        {
            double bpm = ReadDouble();
            double offset = ReadDouble();
            bool notInherited = ReadBoolean();
            return new Timingpoint(bpm, offset, notInherited);
        }

        public DateTime ReadDateTime()
        {
            return new DateTime(ReadInt64(), DateTimeKind.Utc);
        }
        
        public bool AssertByte(byte correctByte, string failMessage)
        {
            if (ReadByte() != correctByte)
            {
                throw new InvalidDataException("AssertByte failed: " + failMessage + " @" + (BaseStream.Position - 1));
            }
            return true;
        }

        public bool AssertStringIsNullOrEmpty(string failMessage)
        {
            if (!string.IsNullOrEmpty((ReadString())))
            {
                throw new InvalidDataException("AssertStringIsNullOrEmpty failed: " + failMessage + " @" + (BaseStream.Position - 1));
            }
            return true;
        }

        public bool AssertInt(int correctInt, string failMessage)
        {
            if (ReadInt32() != correctInt)
            {
                throw new InvalidDataException("AssertInt failed: " + failMessage + " @" + (BaseStream.Position - 1));
            }
            return true;
        }
    }
}
