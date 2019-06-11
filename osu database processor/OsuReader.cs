using osu_database_processor.DataTypes;
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
                byte Byte = ReadByte();
                result |= (ulong)(0x7F & Byte) << shift;
                if ((0x80 & Byte) == 0) break;
                shift += 7;
            }
            return result;
        }

        public override string ReadString()
        {
            byte Byte = ReadByte();
            switch (Byte)
            {
                case 0x0b:
                    return base.ReadString();
                case 0x00:
                    Console.WriteLine("ReadString: first byte is empty @" + (BaseStream.Position - 1));
                    return "";
            }
            Console.WriteLine("ReadString: first byte wrong @" + (BaseStream.Position - 1));
            return null;
        }

        public IntDoublePair ReadIntDoublePair()
        {
            if (!ReadByte().Equals(0x08)) Console.WriteLine("ReadIntDoublePair: first byte wrong @" + (BaseStream.Position - 1));
            int Int = ReadInt32();
            if (!ReadByte().Equals(0x0b)) Console.WriteLine("ReadIntDoublePair: second byte wrong @" + (BaseStream.Position - 1));
            double Double = ReadDouble();
            return new IntDoublePair(Int, Double);
        }

        public Timingpoint ReadTimingpoint()
        {
            double BPM = ReadDouble();
            double Offset = ReadDouble();
            bool Bool = ReadBoolean();
            return new Timingpoint(BPM, Offset, Bool);
        }

        public DateTime ReadDateTime()
        {
            return new DateTime(ReadInt64(), DateTimeKind.Utc);
        }
        
        public bool AssertByte(byte correctByte, string failMessage)
        {
            if (ReadByte() != correctByte)
            {
                Console.WriteLine("AssertByte failed: " + failMessage + " @" + (BaseStream.Position - 1));
                return false;
            }
            return true;
        }

        public bool AssertString(string correctString, string failMessage)
        {
            if (!ReadString().Equals(correctString))
            {
                Console.WriteLine("AssertString failed: " + failMessage + " @" + (BaseStream.Position - 1));
                return false;
            }
            return true;
        }

        public bool AssertInt(int correctInt, string failMessage)
        {
            if (ReadInt32() != correctInt)
            {
                Console.WriteLine("AssertInt failed: " + failMessage + " @" + (BaseStream.Position - 1));
                return false;
            }
            return true;
        }
    }
}
