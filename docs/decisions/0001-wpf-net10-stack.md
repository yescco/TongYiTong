# ADR-0001：采用 .NET 10、WPF 与微内核技术栈

- 状态：已接受
- 日期：2026-10-29

## 背景

同易通需要可自动构建的 Windows 中文串口工具，并为终端、CRC 工具和协议解析保留插件扩展能力。为避免各任务重复选型，本 ADR 一次性固化技术栈。

## 决策表

| 领域 | 决策 |
| --- | --- |
| 运行时与界面 | .NET 10 (LTS)、WPF、C#，界面使用中文。 |
| 总体架构 | 微内核插件化：宿主、Core 契约程序集与插件；细节由 `docs/plans/0002-architecture-design.md` 定稿。 |
| 串口 | `System.IO.Ports`。 |
| MVVM | `CommunityToolkit.Mvvm`。 |
| DI 与宿主 | `Microsoft.Extensions.Hosting`。 |
| 测试 | xUnit 四层：单元、集成、FlaUI E2E 冒烟、手动验收；前三层与 `tests/` 目录一一对应，手动验收不创建代码项目。 |
| UI 自动化 | FlaUI UIA3；关键控件必须设置 `AutomationProperties.AutomationId`。 |
| 自动化脚本 | C# file-based app，入口为 `dotnet run scripts/check.cs`；pwsh 仅作调用 shell。 |
| 工具链 | 通过 scoop 安装 `dotnet-sdk`，全程使用可自动化的 dotnet CLI。 |

## 后果

- 构建与运行环境需要 .NET 10 SDK；WPF 和真实 UI 测试限定 Windows。
- 架构方向已固定，但契约、加载、消息、UI 组合和版本策略仍未决，禁止提前实现。
- 宿主和插件通过明确契约演进，新增功能需考虑测试分层与 AutomationId。
- 当前仅使用模板与测试必需包；在对应实现票中才引入 MVVM、DI、宿主和串口依赖。
- 自动化检查可在不同调用目录运行，并必须保留各 dotnet 命令的失败退出码。
