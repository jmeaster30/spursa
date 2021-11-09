using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;


namespace Spursa
{
  [Generator]
  public class SpursaGen : ISourceGenerator
  {
    public void Initialize(GeneratorInitializationContext context)
    {
      
    }

    public void Execute(GeneratorExecutionContext context)
    {
      GeneratorConfig config = SpursaGenUtilities.LoadFromYaml();

      StringBuilder baseController = new($@"
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SpursaController
{{
  [{string.Join(", ", config.Attributes)}]
  public abstract class SpursaBaseController : ControllerBase {{}}
}}
");

      context.AddSource("SpursaController", SourceText.From(baseController.ToString(), Encoding.UTF8));

      foreach (Controller controller in config.Controllers)
      {
        StringBuilder builder = new($@"
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using {controller.Namespace};

namespace SpursaController
{{
  [Route(""{controller.BasePath}"")]
  public class {controller.Name} : SpursaBaseController
  {{
    private readonly {controller.Service} _{controller.Service.ToLower()};
    public {controller.Name}({controller.Service} {controller.Service.ToLower()})
    {{
      _{controller.Service.ToLower()} = {controller.Service.ToLower()};
    }}

");
        foreach (Route route in controller.Routes)
        {
          if (route.Delete != null)
          {
            builder.Append($@"
    [Route(""{route.Path}""), HttpDelete]
    {BuildRouteSignature(route.Delete)}
    {{
      {BuildServiceCall(controller.Service.ToLower(), route.Delete)}
    }}
    ");
          }
          if (route.Get != null)
          {
            builder.Append($@"
    [Route(""{route.Path}""), HttpGet]
    {BuildRouteSignature(route.Get)}
    {{
      {BuildServiceCall(controller.Service.ToLower(), route.Get)}
    }}
    ");
          }
          if (route.Post != null)
          {
            builder.Append($@"
    [Route(""{route.Path}""), HttpPost]
    {BuildRouteSignature(route.Post)}
    {{
      {BuildServiceCall(controller.Service.ToLower(), route.Post)}
    }}
    ");
          }
          if (route.Put != null)
          {
            builder.Append($@"
    [Route(""{route.Path}""), HttpPut]
    {BuildRouteSignature(route.Put)}
    {{
      {BuildServiceCall(controller.Service.ToLower(), route.Put)}
    }}
    ");
          }
        }

        builder.Append(@"
  }
}");

        context.AddSource(controller.Name, SourceText.From(builder.ToString(), Encoding.UTF8));
      }
    }

    private static string BuildRouteSignature(RouteMethod method)
    {
      return $"public {(string.IsNullOrWhiteSpace(method.Result) ? "void" : method.Result)} {method.Method}({string.Join(", ", method.Parameters.Select(x => $"{x.Type} {x.Name}"))})";
    }

    private static string BuildServiceCall(string service, RouteMethod method)
    {
      return $"{(IsVoidResult(method.Result) ? "" : "return ")}_{service}.{method.Method}({string.Join(", ", method.Parameters.Select(x => x.Name))}, Request);";
    }

    private static bool IsVoidResult(string result)
    {
      return result switch
      {
        "void" => true,
        null => true,
        _ => false
      };
    }

  }

  public class SpursaGenUtilities
  {
    public static GeneratorConfig LoadFromYaml()
    {
      try
      {
        string configText = File.ReadAllText("Controller.Config.yaml");
        IDeserializer deserializer = new DeserializerBuilder()
                                  .WithNamingConvention(CamelCaseNamingConvention.Instance)
                                  .Build();

        Config config = deserializer.Deserialize<Config>(configText);
        List<Controller> all_controllers = new();
        foreach (string controllerName in config.Controllers)
        {
          string controllerText = File.ReadAllText($"{controllerName}.yaml");
          all_controllers.AddRange(deserializer.Deserialize<List<Controller>>(controllerText));
        }
        return GeneratorConfig.FromYamlConfig(config, all_controllers);
      }
      catch (Exception e)
      {
        Console.WriteLine("There was an error :(");
        Console.WriteLine(e.Message);
        return new() { LoadException = e };
      }
    }
  }
}
