using System.Diagnostics;

var extpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "VSCode/extensions.txt");
var exts = await File.ReadAllLinesAsync(extpath);

int success = 0, failed = 0;

foreach (var ext in exts)
{
    Console.WriteLine($"Installing {ext}");

    try
    {
        var startInfo = new ProcessStartInfo();
        startInfo.FileName = "code";
        startInfo.Arguments = $"--install-extension {ext}";

        using (var process = Process.Start(startInfo))
        {

            if (process == null)
            {
                Console.WriteLine("Failed to create code process!");
            }
            else
            {
                await process.WaitForExitAsync();

                if (process.ExitCode != 0)
                {
                    Console.WriteLine($"Failed to install {ext} (or code returned {process.ExitCode}!");
                    failed++;
                }
                else
                {
                    Console.WriteLine("Extension installed successfully!");
                    success++;
                }
            }
        }
    }
    catch (Exception)
    {
        Console.WriteLine($"Failed to install extension {ext}");
        failed++;
    }
}

Console.WriteLine($"Installed {success} extensions, {failed} failed!");