using PlayerConstDumper;

var fileargs = args.Select(x => new FileInfo(x)).ToArray();

foreach (var file in fileargs)
{
    if (file.Exists && file.Extension is ".bin")
    {
        using var stream = file.OpenRead();
        var consts = new PlayerConst();
        consts.Read(stream);
        var name = Path.GetFileNameWithoutExtension(file.Name);
        var dir = file.Directory.FullName;
        File.WriteAllText($"{dir}\\{name}.json", consts.ToString());
    } else if (file.Exists && file.Extension is ".json")
    {
        var consts = PlayerConst.FromFile(file);
        using MemoryStream stream = new();
        consts.Write(stream);
        var name = Path.GetFileNameWithoutExtension(file.Name);
        var dir = file.Directory.FullName;
        File.WriteAllBytes($"{dir}\\{name}.bin", stream.ToArray());
    }
}