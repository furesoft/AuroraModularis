namespace AuroraModularis;

[AttributeUsage(AttributeTargets.Class)]
public class PriorityAttribute : Attribute
{
    public PriorityAttribute(ModulePriority priority)
    {
        Priority = priority;
    }

    public PriorityAttribute()
    {
    }

    public ModulePriority Priority { get; set; }
}