using System.Collections.Generic;

namespace Yapper.Config;

public class YapperConfigData
{
    public Dictionary<string, Dictionary<string, string>> PlayerVariants { get; set; } = new();
}
