using System;
using System.Collections.Generic;

namespace Spursa
{
  public class Config
  {
    public List<string> Attributes { get; set; }
    public List<string> Controllers { get; set; }
  }
  public class Controller
  {
    public string Name { get; set; }
    public string Namespace { get; set; }
    public string Service { get; set; }
    public string BasePath { get; set; }
    public List<Route> Routes { get; set; }
  }

  public class Route
  {
    public string Path { get; set; }
    public RouteMethod Get { get; set; } // can we just do a string and use reflection to look up the function ?
    public RouteMethod Post { get; set; }
    public RouteMethod Put { get; set; }
    public RouteMethod Delete { get; set; }
  }

  public class RouteMethod
  {
    public List<Param> Parameters { get; set; }
    public string Result { get; set; }
    public string Method { get; set; }
  }

  public class Param
  {
    public string Name { get; set; }
    public string Type { get; set; }
  }
}