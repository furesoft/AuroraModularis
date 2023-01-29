using AuroraModularis;
using AuroraModularis.Core;
using System.Text.Json;

namespace TestGui;

[Priority(ModulePriority.Max)]
public class Module : AuroraModularis.Module
{
    private ModuleLoader moduleLoader;

    public override Task OnStart(ServiceContainer serviceContainer)
    {
        var frm = new Form1();
        frm.FormClosing += (s, e) =>
        {
            Environment.Exit(0);
        };

        var flowLayout = new FlowLayoutPanel();
        flowLayout.Dock = DockStyle.Fill;

        Inbox.Subscribe<Button>(_ =>
        {
            flowLayout.Controls.Add(_);
        });

        foreach (var module in moduleLoader.Modules)
        {
            Button btn = new Button { Text = module.Name, AutoSize = true };
            btn.Click += (s, e) =>
            {
                MessageBox.Show(JsonSerializer.Serialize(module.Settings));
            };
            flowLayout.Controls.Add(btn);
        }
        frm.Controls.Add(flowLayout);

        Application.DoEvents();
        Application.Run(frm);

        return Task.CompletedTask;
    }

    public override void RegisterServices(ServiceContainer serviceContainer)
    {
        moduleLoader = serviceContainer.Resolve<ModuleLoader>();
    }
}