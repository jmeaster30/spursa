using Microsoft.AspNetCore.Http;

namespace ShowCase
{
  public class TestService
  {
    public TestService() { }

    public MyTestObject GetTest(int id, HttpRequest request)
    {
      return new() { Id = id, Name = "hehe :)" };
    }

    public MyTestObject UpdateTestModel(MyTestObject model, HttpRequest request)
    {
      return model;
    }

    public int Sum(int a, int b, HttpRequest request)
    {
      return a + b;
    }
  }
}