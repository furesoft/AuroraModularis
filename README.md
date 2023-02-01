# AuroraModularis
A framework for building modular monoliths.

## Architecture

A main application bootstraps the modules. A module consists of two librarys: One for the the module logic and the other for the models that are used to expose interfaces and DTOs to other modules. A module has to have an implementation of the `Module` class. This implementation can register services to make logic available to other modules. A module can also send/receive messages to other modules through the `Inbox`/`Outbox` properties. The bootstrapper has a messagebroker to find the modules which are subscribing to a specific kind of messages. A module can expect module specific settings. The settings are automaticly serialized/deserialized to the `Settings` object. 

## Bootstrapping the main application

The main application has to start a bootstrapper. A bootstrapper can be configured with the `BootstrapperBuilder`. Modules can be loaded from any path.

### Extending the behavior of the bootstrapper

The bootstrapper has mechanisms called `Hooks` to affect the behavior. The `IModuleLoadingHook` can decide if modules should be loaded or do other stuff on load, like register resources globally for UI. The `ISettingsLoadingHook` can inject specific custom settings to a module. This behavior can be useful if you want dynamic settings. A good example for dynamic settings are paths. Per default module settings are serialized as json file but you can implement your own `SettingsProvider` to use other formats. There are also some formats available at nuget.

## Examples

You can find two examples in this repository. The [console](https://github.com/furesoft/AuroraModularis/tree/main/Src/TestApp/TestConsole) example is a really basic example. The [GUI](https://github.com/furesoft/AuroraModularis/tree/main/Src/TestApp/TestGui) example shows the usage of AuroraModularis with Windows Forms but any UI library can be used.

The developer can decide if the whole UI is seperated into a specific module and the application is only the entrypoint. But some UI designers have troubles with that architecture. So a direct UI integration into the main application is also possible. 

If you want to see a more advanced sample with mostly all parts of the ui made extendable you can look at my other project [Slithin](https://github.com/furesoft/Slithin/tree/develop/Source/New). 

Slithin can be extended with:
- custom pages
- custom settings sections
- custom submenu items with the ability for custom controls
- custom context menu items based on a specific selection
- show custom dialogs/notifications
- load custom icons for custom ui elements per module

## Services

AuroraModularis uses a custom IOC-Container to work with dependencies. It supports DI injection for constructors and lifetimes. 

### Default provided services 

AuroraModularis also provides some default services to work better with extensibility and get some information about other modules that are currently loaded. 

The `ITypeFinder` service helps to locate specific types that are implementing a specific interface. This is useful for example to generate an extendable menu.

The module loader is also provided to have access to other modules. Like for example enumerating all modules and display it in the UI. 

## Scheduling

A module can schedule jobs using the Quartz library. The module specific scheduler is registered in the IOC container. 
