using osu_database_processor.DataTypes;
using System;
using System.IO;

namespace osu_database_processor
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream fs = new FileStream("D:\\collection.db", FileMode.Open);
            OsuReader OR = new OsuReader(fs);
            CollectionDb Cdb = new CollectionDb(OR);
        }
    }
}
