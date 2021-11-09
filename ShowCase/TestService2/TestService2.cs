using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace ShowCase
{
  public class TestService2
  {
    public TestService2() { }

    public WoahModel GetWoahModel(int woahId, HttpRequest request)
    {
      return new() { Id = woahId, Length = 16, Sound = "woooooaaaahhh!!!!! :O" };
    }

    public void DeleteWoahModel(int woahId, HttpRequest request)
    {
      int a = woahId;
    }

  }
}