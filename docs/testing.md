# 测试策略

测试分为四层，前三层对应 `tests/` 下的项目，手动验收不创建代码项目。

| 层 | 位置或记录 | 职责 |
| --- | --- | --- |
| 单元测试 | `tests/TongYiTong.Core.Tests` | 验证独立逻辑、边界和错误分支，快速且无外部设备依赖。 |
| 集成测试 | `tests/TongYiTong.IntegrationTests` | 验证 Core、Host 及插件边界的协作，替代真实外设时使用受控替身。 |
| E2E 冒烟 | `tests/TongYiTong.UiSmokeTests` | 通过 FlaUI UIA3 启动真实 WPF 进程并验证关键交互。 |
| 手动验收 | 对应任务票的验收记录 | 验证真实串口设备、视觉体验、长时间运行等不适合自动化的场景。 |

`dotnet run scripts/check.cs` 统一执行前三层。当前每层只有占位 Fact；UI 占位不启动 exe，P1 再实现真实冒烟用例。

三个测试项目通过测试平台的 `_MSTestEnableParentProcessQuery` 配置关闭父进程查询，兼容不允许 testhost 打开其父进程的受限环境；测试发现与执行仍由标准 VSTest 完成。

## FlaUI UIA3 做法

- 使用 `FlaUI.UIA3`，按 `AutomationProperties.AutomationId` 定位关键控件。
- 每个用例独立启动和关闭宿主，等待明确 UI 状态，避免固定延时与坐标点击。
- 冒烟只覆盖启动、核心收发路径等关键流程；详细规则交给单元和集成测试。
- 用例失败后保留必要的窗口状态或诊断信息，确保可定位。

## 会话限制

UIA 需要可交互的 Windows 桌面会话。锁屏、无窗口站或普通无界面 CI runner 可能无法启动或操作 WPF；真实 E2E 必须运行在具备交互会话的执行环境。当前不创建 CI。

## 目录映射

测试项目按测试层命名，新增测试放入对应目录，不另建重叠项目。手动验收以任务票中的步骤、环境和结果留痕，与三层代码测试共同构成完整验证。
