### 简介
RestSharpHelper是一个使用RestSharp项目，作为二次封装，方便集成Api调用的辅助类，同时提供在Api调用各阶段事件回调和返回数据二次处理。

### 使用方法
下面以网站 [jsonplaceholder.typicode.com/](https://jsonplaceholder.typicode.com/) 的todo数据为例示例：

```csharp
class Program
{
    public static void Main(string[] args)
    {
        var restSharpHelper = GetRestSharpHelper();
        var result = restSharpHelper.GetAsync<Todo>("/todos/1");
        result.ContinueWith(p =>
        {
            Assert.AreEqual(1, p.Result.id);
        });
        result.Wait();
    }
    private static RestSharpHelper GetRestSharpHelper()
    {
        return new RestSharpHelper("https://jsonplaceholder.typicode.com");
    }
}
public class Todo
{
    public int userId { get; set; }
    public int id { get; set; }
    public string title { get; set; }
    public bool completed { get; set; }
}
 
```