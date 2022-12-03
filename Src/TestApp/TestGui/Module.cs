using AuroraModularis;

namespace TestGui;

[Priority(ModulePriority.Max)]
public class Module : AuroraModularis.Module
{
    private ModuleLoader moduleLoader;

    public override string Name => "Host";

    public override Task OnStart()
    {
        var frm = new Form1();
        frm.FormClosing += (s, e) =>
        {
            Environment.Exit(0);
        };

        Inbox.Subscribe<Button>(_ =>
        {
            frm.Controls.Add(_);
        });

        var flowLayout = new FlowLayoutPanel();
        flowLayout.Dock = DockStyle.Fill;

        foreach (var module in moduleLoader.Modules)
        {
            flowLayout.Controls.Add(new Label { Text = module.Name });
        }
        frm.Controls.Add(flowLayout);

        Application.DoEvents();
        Application.Run(frm);

        return Task.CompletedTask;
    }

    public override void RegisterServices(TinyIoCContainer container)
    {
        moduleLoader = container.Resolve<ModuleLoader>();
    }
}