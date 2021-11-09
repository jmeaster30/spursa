using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ShowCase
{
  public class AnotherService
  {
    public AnotherService () { }

    public string ReverseString(string woah, HttpRequest request)
    {
      return woah.Reverse().ToString();
    }

    public ModelModel WhatTheHeck(ModelModel how, HttpRequest request)
    {
      return how;
    }
  }
}