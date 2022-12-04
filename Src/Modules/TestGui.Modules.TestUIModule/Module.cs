namespace TestGui.Modules.TestUIModule;

public class Module : AuroraModularis.Module
{
    public override Task OnStart()
    {
        Button button = new Button() { Text = "btn " };
        button.Height = 25;
        button.Click += (s, e) =>
        {
            MessageBox.Show("yay: ");
        };

        Outbox.Post<Button>(button, true);

        return Task.CompletedTask;
    }
}