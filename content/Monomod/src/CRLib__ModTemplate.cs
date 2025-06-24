using BepInEx;
using BepInEx.Logging;
using System.Reflection;
#if (MMHOOKLocation == "")
using System.Collections.Generic;
using MonoMod.RuntimeDetour;
#endif
using UnityEngine;
using CodeRebirthLib;
using CodeRebirthLib.Extensions;

namespace CRLib._ModTemplate;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class CRLib__ModTemplate : BaseUnityPlugin
{
    internal new static ManualLogSource Logger { get; private set; } = null!;
    public static CRMod Mod { get; private set; } = null!;

    private void Awake()
    {
        Logger = base.Logger;

        NetcodePatcher();

        AssetBundle mainBundle = CRLib.LoadBundle(Assembly.GetExecutingAssembly(), "main_bundle");
        Mod = CRLib.RegisterMod(this, mainBundle);
        Mod.Logger = Logger;

        // Register Content
        Mod.RegisterContentHandlers();

        Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
    }

    private void NetcodePatcher()
    {
        var types = Assembly.GetExecutingAssembly().GetLoadableTypes();
        foreach (var type in types)
        {
            if (type.IsNested) continue;

            var methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes(typeof(RuntimeInitializeOnLoadMethodAttribute), false);
                if (attributes.Length > 0)
                {
                    method.Invoke(null, null);
                }
            }
        }
    }
}
