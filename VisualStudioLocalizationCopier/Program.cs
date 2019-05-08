using System;
using System.IO;
using System.Linq;

namespace VisualStudioLocalizationCopier
{
    class Program
    {
        static void Main(string[] args)
        {
            var projectPath = "C:\\Users\\Spade\\Projects\\GitHub\\VisualStudio";

            var directoryInfo = new DirectoryInfo(@"\\simpleloc\drops\Drops\GitHub_VSextension_2435");
            var latestJob = directoryInfo.GetDirectories().OrderByDescending(info => info.CreationTimeUtc).First();

            var jobResultsDirectory = new DirectoryInfo(Path.Combine(latestJob.FullName, "BinDrops\\Windows\\bin"));
            foreach (var jobLanguageResult in jobResultsDirectory.GetDirectories())
            {
                if(jobLanguageResult.Name == "qps-ploc") continue;

                var source = Path.Combine(jobLanguageResult.FullName, "src\\GitHub.Resources\\Resources.resx");
                var dest = Path.Combine(projectPath, "src\\GitHub.Resources", $"Resources.{jobLanguageResult.Name}.resx");

                Console.WriteLine($"Copy '{source}' to '{dest}'");
                File.Copy(source, dest, true);

                source = Path.Combine(jobLanguageResult.FullName, $"src\\GitHub.VisualStudio\\xlf\\GitHub.VisualStudio.vsct.{jobLanguageResult.Name}.xlf");
                dest = Path.Combine(projectPath, "src\\GitHub.VisualStudio\\xlf", $"GitHub.VisualStudio.vsct.{jobLanguageResult.Name}.xlf");

                Console.WriteLine($"Copy '{source}' to '{dest}'");
                File.Copy(source, dest, true);

                source = Path.Combine(jobLanguageResult.FullName, $"src\\GitHub.VisualStudio\\xlf\\VSPackage.{jobLanguageResult.Name}.xlf");
                dest = Path.Combine(projectPath, "src\\GitHub.VisualStudio\\xlf", $"VSPackage.{jobLanguageResult.Name}.xlf");

                Console.WriteLine($"Copy '{source}' to '{dest}'");
                File.Copy(source, dest, true);
            }
        }
    }
}
