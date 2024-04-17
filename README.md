# ASP.net string array memory issue
This is a reproduction repository of a memory issue when returning large string lists in an **ApiController**.

## The problem
In a controller I'm creating a list, adding 1.000.000 strings to it, and then returning it. When navigating to the web page, I'm getting a JSON array with a million strings. This code is located in [`WebApplication/Controllers/ExampleController.cs`](./WebApplication/Controllers/ExampleController.cs).

```cs
[HttpGet]
[OutputCache(Duration = 0, NoStore = true)]
public IEnumerable<string> Memory()
{
	List<string> list = new();

	for (var i = 0; i < 1_000_000; i++)
	{
		list.Add(i.ToString());
	}

	return list;
}
```

The garbage collector should free the memory when I'm done with the request. However, the memory keeps climbing (see screenshot).
![image](https://github.com/dotnet/aspnetcore/assets/31313717/0cd74f37-73e6-4fc8-9f58-fb99f817e96d)

## Expected behaviour
When writing a simple console application, the memory falls back to a level you can expect (see screenshot). This code is located in [`ConsoleApp/Program.cs`](./ConsoleApp/Program.cs).
![image](https://github.com/dotnet/aspnetcore/assets/31313717/0560b360-81ba-4631-986c-3300272fc2bd)
```cs
while (true)
{
    GC.Collect();
    GC.WaitForPendingFinalizers();
    Console.ReadLine();

    var list = GetList();
    Console.WriteLine(list.Count);
}

List<string> GetList()
{
    List<string> list = new();
    for (int i = 0; i < 1_000_000; i++)
    {
        list.Add(i.ToString());
    }

    return list;
}
```