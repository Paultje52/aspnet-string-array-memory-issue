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