using osu_database_processor.DataTypes;
using System;
using System.IO;

namespace osu_database_processor
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream fs = new FileStream("D:\\osu!.db", FileMode.Open);
            OsuReader OR = new OsuReader(fs);
            OsuDb Db = new OsuDb(OR);
        }
    }
}
