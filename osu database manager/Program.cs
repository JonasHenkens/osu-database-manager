using CommandLine;
using System;
using System.Collections.Generic;
using osu_database_processor.Databases;
using System.IO;
using osu_database_processor;
using CommandLine.Text;
using osu_database_processor.Components;
using osu_database_manager.Tools;

namespace osu_database_manager
{
    class Program
    {
        [Verb("merge", HelpText = "Merge collection files")]
        class MergeOptions
        {
            [Option('i', "input",
                Required = true,
                HelpText = "Collection files to be merged.")]
            public IList<string> InputFiles { get; set; }

            [Option('o', "output",
                Required = true,
                HelpText = "Output file.")]
            public string OutputFile { get; set; }

        }

        [Verb("collectionsacc", HelpText = "Generate collections based on accuracy")]
        class GenColByAccOptions
        {
            [Usage(ApplicationAlias = "OsuDatabaseManager")]
            public static IEnumerable<Example> Examples
            {
                get
                {
                    return new List<Example>() {
                        new Example("Generate collections based on accuracy (0 - <90, 90 - <100, 100)", new GenColByAccOptions {Seperators = new[] { 90, 100 }, OsuPath = @"C:\osu!" , OutputFile = @"C:\collection.db"})
                    };
                }
            }

            [Option('i', "input",
                Required = true,
                HelpText = "Path to osu! folder.")]
            public string OsuPath { get; set; }

            [Option('o', "output",
                Required = true,
                HelpText = "Output file.")]
            public string OutputFile { get; set; }

            [Option("seperators",
                Default = new[] { 88, 92, 95, 97, 98, 99, 100 },
                HelpText = "Values that seperate each collection. Collections go from value to smaller then next value.")]
            public IList<int> Seperators { get; set; }

            [Option("prefix",
                Default = "",
                HelpText = "The prefix will be added in front of each generated collection.")]
            public string Prefix { get; set; }

            [Option("online",
                Default = false,
                HelpText = "Checks locally beaten maps against online scores, api key and name is required if set to true. This is slower and not always necessary.")]
            public bool Online { get; set; }

            [Option('k', "key",
                HelpText = "Api key to use if online is true")]
            public string Key { get; set; }

            [Option("name",
                HelpText = "Only check scores for specific username.")]
            public string Name { get; set; }

            [Option("ratelimit",
                HelpText = "When online is true, the limit of requests per minite to the osu api.")]
            public string RateLimit { get; set; }

            [Option("mergeexisting",
                Default = false,
                HelpText = "Merge generated collection with an existing one.")]
            public bool MergeWithExisting { get; set; }
        }

        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<MergeOptions, GenColByAccOptions>(args)
                .MapResult(
                  (MergeOptions opts) => RunMergeAndReturnExitCode(opts),
                  (GenColByAccOptions opts) => RunGenColByAccAndReturnExitCode(opts),
                  errs => 1);
        }

        // generate collections by accuracy
        private static object RunGenColByAccAndReturnExitCode(GenColByAccOptions opts)
        {

            // if online:
            //      check if osu!.db present
            //      check if key and name present

            // if offline:
            //      check if scores.db present


            // if online: 
            //      collect list of all maps with scores that are ranked, loved or approved
            //      collect list of all maps in osu!.db where
            //          Grade achieved in standard
            //          Grade achieved in taiko
            //          Grade achieved in CTB
            //          Grade achieved in mania
            //       are not unplayed
            //      use api to get scores
            // can use ranked_score to see if all maps

            // if local: get scores from scores.db
            // if name specified: can use ranked_score to see if all maps

            CollectionDb colDb = null;
            if (opts.Online)
            {
                if (string.IsNullOrWhiteSpace(opts.Key)) throw new ArgumentException("Key is empty.");
                if (string.IsNullOrWhiteSpace(opts.Name)) throw new ArgumentException("Name is empty.");
                if (!File.Exists(Path.Combine(opts.OsuPath, "osu!.db"))) throw new FileNotFoundException("Missing osu!.db in selected folder.");

                throw new NotImplementedException();
            }
            else
            {
                string scoresDbPath = Path.Combine(opts.OsuPath, "scores.db");
                if (!File.Exists(scoresDbPath)) throw new FileNotFoundException("Missing scores.db in selected folder.");

                ScoresDb scoresDb = new ScoresDb(scoresDbPath);
                List<int> seperators = new List<int>(opts.Seperators);
                colDb = CollectionTools.GenerateCollectionDbByAccuracy(scoresDb, seperators, opts.Name, opts.Prefix);
            }

            if (opts.MergeWithExisting)
            {
                string existingPath = Path.Combine(opts.OsuPath, "collection.db");
                if (!File.Exists(existingPath))
                {
                    Console.WriteLine("Missing collection.db in selected folder. Skipping merge operation");
                }
                else
                {
                    CollectionDb existing = new CollectionDb(existingPath);
                    existing.Merge(colDb, AddMode.Overwrite);
                    colDb = existing;
                }
            }

            colDb.WriteToFile(opts.OutputFile);
            return 0;
        }

        private static object RunMergeAndReturnExitCode(MergeOptions opts)
        {
            if (opts.InputFiles.Count == 0)
            {
                Console.WriteLine("No input files, skipping operation.");
            }
            else
            {
                try
                {
                    CollectionDb result = null;
                    foreach (string path in opts.InputFiles)
                    {
                        if (result == null)
                        {
                            result = new CollectionDb(path);
                        }
                        else
                        {
                            result.Merge(new CollectionDb(path), AddMode.Merge);
                        }
                    }
                    result.WriteToFile(opts.OutputFile);
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(FileNotFoundException))
                    {
                        Console.WriteLine("One or more inputfiles are not found.");
                    }
                    else if (e.GetType() == typeof(InvalidDataException))
                    {
                        Console.WriteLine("One or more inputfiles contain wrong data.");
                    }
                    else
                    {
                        Console.WriteLine("An error has occurred, make sure all parameters are correct.");
                    }
                    return 1;
                }
            }

            return 0;
        }

        private static void HandleParseError(IEnumerable<Error> errs)
        {
            foreach (var item in errs)
            {
                Console.WriteLine(item);
            }
        }

        public static Collection GenerateCollectionByAccuracy(List<int> seperators, List<Score> scores)
        {

            throw new NotImplementedException();
        }

    }
}
