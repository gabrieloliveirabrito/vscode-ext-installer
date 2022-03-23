using System.Diagnostics;

var extpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "VSCode/extensions.txt");
var exts = await File.ReadAllLinesAsync(extpath);

foreach (var ext in exts)
{
    Console.WriteLine($"Installing {ext}");
    var startInfo = new ProcessStartInfo();
    startInfo.FileName = "code";
    startInfo.Arguments = $"--install-extension {ext}";
    startInfo.UseShellExecute = false;
    startInfo.RedirectStandardOutput = true;

    using (var process = Process.Start(startInfo))
    {

        if (process == null)
        {
            Console.WriteLine("Failed to create code process!");
        }
        else
        {
            process.OutputDataReceived += (s, e) => { };

            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
                Console.WriteLine($"Failed to install {ext} (or code returned {process.ExitCode}!");
            else
                Console.WriteLine("Extension installed successfully!");
        }
    }
}