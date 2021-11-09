using System;
using System.Collections.Generic;

namespace Spursa
{
  public class GeneratorConfig
  {
    public List<string> Attributes { get; set; }
    public List<Controller> Controllers { get; set; }
    public Exception LoadException { get; set; }

    public static GeneratorConfig FromYamlConfig(Config yamlConfig, List<Controller> controllers)
    {
      return new()
      {
        Attributes = yamlConfig.Attributes,
        Controllers = controllers
      };
    }
  }
}