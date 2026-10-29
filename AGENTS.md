# AGENTS.md

同易通（TongYiTong）是基于 .NET 10、WPF 和微内核插件化架构的中文串口工具。

## 工作入口

1. 先读 [docs/README.md](docs/README.md)，按任务选择下一层文档。
2. 实施任务必须对应 [docs/plans](docs/plans) 中的任务票；不实现未决架构。
3. 技术栈以 [ADR-0001](docs/decisions/0001-wpf-net10-stack.md) 为准。
4. 修改后必须运行 `dotnet run scripts/check.cs`，全绿后再提交。

## 常用命令

```sh
scoop install dotnet-sdk
dotnet restore TongYiTong.sln
dotnet build TongYiTong.sln
dotnet test TongYiTong.sln
dotnet run --project src/TongYiTong.Host
dotnet run scripts/check.cs
```

## 硬规则

- 目标框架为 .NET 10；界面与文档使用中文；WPF 采用 MVVM。
- 架构方向是宿主、Core 契约与插件，定稿前不写架构实现。
- 串口使用 `System.IO.Ports`；MVVM 使用 `CommunityToolkit.Mvvm`。
- DI/宿主使用 `Microsoft.Extensions.Hosting`；当前 Host 保持模板，不预接 DI/MVVM/串口。
- 关键控件必须设置 `AutomationProperties.AutomationId`，E2E 使用 FlaUI UIA3。
- 声明处写一行该语言惯用的文档注释，说明“是什么”；其他注释默认不写。
- 自动化逻辑用 C# file-based app 实现；pwsh 只负责调用 shell。
- 不为占位类库添加 `Class1.cs`；测试占位只验证测试流程。
- 不提交 `bin/`、`obj/`、`.vs/` 等生成物。

## 目录地图

| 路径 | 用途 |
| --- | --- |
| `src/TongYiTong.Core` | 未来契约、消息与串口抽象 |
| `src/TongYiTong.Host` | WPF 宿主 shell，目前仅默认模板 |
| `src/plugins/TongYiTong.Plugin.Terminal` | 未来终端插件 |
| `src/plugins/TongYiTong.Plugin.CrcTool` | 未来 CRC 工具插件 |
| `tests/TongYiTong.Core.Tests` | 单元测试 |
| `tests/TongYiTong.IntegrationTests` | 集成测试 |
| `tests/TongYiTong.UiSmokeTests` | FlaUI E2E 冒烟测试 |
| `scripts/check.cs` | 格式、构建与测试闭环 |
| `docs/decisions` | 已接受的架构决策 |
| `docs/plans` | 可领取的任务票 |
