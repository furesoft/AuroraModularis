using AuroraModularis;

namespace TestGui;

[Priority(ModulePriority.Max)]
public class Module : AuroraModularis.Module
{
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

        Application.DoEvents();
        Application.Run(frm);

        return Task.CompletedTask;
    }
}