using System.Diagnostics;
using System.Runtime.CompilerServices;

var scriptDirectory = Path.GetDirectoryName(GetScriptPath())!;
var repositoryRoot = Directory.GetParent(scriptDirectory)!.FullName;
var solutionPath = Path.Combine(repositoryRoot, "TongYiTong.sln");

var checks = new (string Name, string[] Arguments)[]
{
    ("格式验证", ["format", solutionPath, "--verify-no-changes"]),
    ("构建", ["build", solutionPath]),
    ("测试", ["test", solutionPath, "--no-build"]),
};

foreach (var (name, arguments) in checks)
{
    Console.WriteLine($"\n===== {name} =====");
    var exitCode = await RunDotNetAsync(arguments, repositoryRoot);
    if (exitCode != 0)
    {
        Console.Error.WriteLine($"{name}失败，退出码：{exitCode}。");
        return exitCode;
    }
}

Console.WriteLine("\n格式、构建与测试全部通过。");
return 0;

/// <summary>
/// 获取当前脚本路径。
/// </summary>
static string GetScriptPath([CallerFilePath] string path = "") => path;

/// <summary>
/// 在指定工作目录运行 dotnet 并返回退出码。
/// </summary>
static async Task<int> RunDotNetAsync(IEnumerable<string> arguments, string workingDirectory)
{
    var startInfo = new ProcessStartInfo
    {
        FileName = "dotnet",
        WorkingDirectory = workingDirectory,
        UseShellExecute = false,
    };

    foreach (var argument in arguments)
    {
        startInfo.ArgumentList.Add(argument);
    }

    using var process = Process.Start(startInfo)
        ?? throw new InvalidOperationException("无法启动 dotnet 进程。");
    await process.WaitForExitAsync();
    return process.ExitCode;
}
