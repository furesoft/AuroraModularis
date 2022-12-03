namespace TestGui.Modules.TestUIModule;

public class Module : AuroraModularis.Module
{
    public override string Name => "UI";

    public override Task OnStart()
    {
        Button button = new Button() { Text = "Hello World" };
        button.Click += (s, e) =>
        {
            MessageBox.Show("yay");
        };

        Outbox.Post<Button>(button, true);

        return Task.CompletedTask;
    }
}