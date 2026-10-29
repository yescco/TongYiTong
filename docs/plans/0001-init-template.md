# 0001：初始化 AI 优先项目模板

- 状态：已完成
- 日期：2026-10-29

## 范围

建立 AI 优先文档入口、ADR 与任务票，提供可构建的 WPF 空解决方案和统一检查闭环；不包含架构实现或 CI。

## 验收清单

- [x] 创建 `TongYiTong.sln`，包含 4 个 `src` 项目和 3 个 `tests` 项目。
- [x] 复用现有 WPF 模板与空类库，不添加无意义占位类或业务实现。
- [x] 测试项目配置 xUnit、被测引用和占位 Fact，UI 冒烟项目引用 FlaUI UIA3 且不启动 exe。
- [x] 建立 `AGENTS.md`、分层文档、ADR-0001 和 0002 架构定稿票。
- [x] 添加 `.editorconfig`、`.gitignore` 与 `scripts/check.cs`。
- [x] 在仓库根目录运行 `dotnet run scripts/check.cs` 并全绿。
- [x] 确认 `bin/`、`obj/`、`.vs/` 不进入提交，完成首次提交。

## 验证记录

- `dotnet --version`：10.0.402。
- `dotnet build TongYiTong.sln`：7 个项目构建成功，0 错误；FlaUI 4.0.0 的传递依赖 `System.Drawing.Common` 5.0.2 报告 NU1904 已知漏洞警告。
- `dotnet run scripts/check.cs`：格式验证通过；7 个项目构建成功；单元、集成、UI 冒烟各 1 个测试通过，共 3 个，0 失败，0 跳过。
- 环境兼容：testhost 的父进程查询在当前 Windows 环境触发 Access denied；三个测试项目使用 `_MSTestEnableParentProcessQuery=false` 关闭该看门狗后，标准 VSTest 测试正常执行。
- 忽略规则：`git check-ignore` 确认 `bin/`、`obj/` 和 `TestResults/` 被忽略；暂存内容不含生成物。
- 首次提交：以 `chore: init AI-first project template` 完成，提交哈希见 Git 历史。
