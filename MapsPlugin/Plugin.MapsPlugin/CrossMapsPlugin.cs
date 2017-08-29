using Plugin.MapsPlugin.Abstractions;
using System;

namespace Plugin.MapsPlugin
{
  /// <summary>
  /// Cross platform MapsPlugin implemenations
  /// </summary>
  public class CrossMapsPlugin
  {
    static Lazy<IMapsPlugin> Implementation = new Lazy<IMapsPlugin>(() => CreateMapsPlugin(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

    /// <summary>
    /// Current settings to use
    /// </summary>
    public static IMapsPlugin Current
    {
      get
      {
        var ret = Implementation.Value;
        if (ret == null)
        {
          throw NotImplementedInReferenceAssembly();
        }
        return ret;
      }
    }

    static IMapsPlugin CreateMapsPlugin()
    {
#if PORTABLE
        return null;
#else
        return new MapsPluginImplementation();
#endif
    }

    internal static Exception NotImplementedInReferenceAssembly()
    {
      return new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
    }
  }
}
