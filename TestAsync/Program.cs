using CG.Web.MegaApiClient;
using Microsoft.Win32;
using TestAsync;
using Hangfire;
using Hangfire.SqlServer;
public class Program
{
    public static void Main()
    {
        MegaApiClient client = new MegaApiClient();
        client.Login("yamchahuyvs@gmail.com", "Minh.2004");
        //IEnumerable<INode> nodes = client.GetNodes();
        //INode parent = nodes.Single(n => n.Type == NodeType.Root);
        //var clientUser = client.CreateFolder("MinhFolder",parent);
        // GetNodes retrieves all files/folders metadata from Mega
        // so this method can be time consuming


        //DisplayNodesRecursive(nodes, parent);


        //foreach (INode node in nodes.Where(x => x.Type == NodeType.File))
        //{
        //    string parents = GetParents(node, nodes);
        //    Directory.CreateDirectory(parents);
        //    Console.WriteLine($"Downloading {parents}\\{node.Name}");
        //    client.DownloadFile(node, Path.Combine(parents, node.Name));
        //}

        //Uri folderLink = new Uri("https://mega.nz/folder/e4diDZ7T#iJnegBO_m6OXBQp27lHCrg");
        //IEnumerable<INode> nodes = client.GetNodesFromLink(folderLink);

        //Uri imgLink = new Uri("https://mega.nz/file/tidgBTLR#BXRmoHfKhQjXpLHvni-HfIq_Dw3Si2it_rC-VwMtCHk");
        //INode file = client.GetNodeFromLink(imgLink);
        //bool loop = true;
        //Progress<Double> progress = new Progress<Double>();
        //progress.ProgressChanged += (sender, e) => Console.WriteLine(e.ToString());

        //Task loopMain = new Task(async () =>
        //{
        //    while(loop)
        //    {
        //        Console.WriteLine("...");
        //        Task.Delay(1000).Wait();
        //    }
        //});


        //IEnumerable<INode> nodes = client.GetNodes();
        //INode myFile = nodes.Single(x => x.Type == NodeType.File && x.Name == "Screenshot.png");

        string downloadsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
        string pathChrome = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows\CurrentVersion\App Paths\chrome.exe", "", null);
        string filePath = downloadsFolderPath + "\\" + "Screenshot.png";

        //Task downloadTask = new Task(async () =>
        //{

        //    await client.DeleteAsync(myFile);
        //    //client.DownloadFileAsync(file,filePath , progress).Wait();
        //    Console.WriteLine("Done delete");
        //    //loop = false;


        //    // Get the file type
        //    //System.Diagnostics.Process.Start(pathChrome, filePath);
        //    client.Logout();
        //});
        //loopMain.Start();
        //downloadTask.Start();




        //IEnumerable<INode> nodes = client.GetNodes();

        //INode myFolder = nodes.Single(x => x.Type == NodeType.Directory && x.Name == "Minh");
        ////INode minhFolder = nodes.
        ////INode myFolder = client.CreateFolder("Upload", root);
        //Progress<Double> progress = new Progress<Double>();
        //progress.ProgressChanged += (sender, e) => Console.WriteLine(e.ToString());
        //INode myFile = null;
        //Task Upload = new Task(async () =>
        //{
        //    Task<INode> task = client.UploadFileAsync("E:\\C#\\TestAsync\\TestAsync\\bin\\Debug\\net6.0\\SharedFolder\\SharedFile.jpg", myFolder, progress);
        //    myFile = await task;
        //    Uri downloadLink = client.GetDownloadLink(myFile);
        //    //Console.WriteLine(downloadLink);


        //    // Get the file type
        //    var fileType = File.
        //});
        //Upload.Start();

        Task t = new Task(() => { Test(); });
        t.Start();

        GlobalConfiguration.Configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseColouredConsoleLogProvider()
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage("Data Source=sql.bsite.net\\MSSQL2016;Initial Catalog=minhdtbz_TaskManager;User ID=minhdtbz_TaskManager;Password=minh;Persist Security Info=True;Encrypt=false;");
        BackgroundJobClient backgroundJobClient = new BackgroundJobClient();
        //backgroundJobClient.Schedule(() => System.Diagnostics.Process.Start(pathChrome, filePath) ,TimeSpan.FromSeconds(10));
        backgroundJobClient.Schedule(() => Console.WriteLine("MinhDOne") ,TimeSpan.FromSeconds(10));
        using (var server = new BackgroundJobServer())
        {
            Console.ReadLine();
        }
        Console.WriteLine("Done");
        while (true)
        {
        }
        
    }

    static async Task Test()
    {
        for (int i = 0; i < 10; i++)
        {
            Task.Delay(1000).Wait();
            Console.WriteLine("...");
        }
    }
    static void DisplayNodesRecursive(IEnumerable<INode> nodes, INode parent, int level = 0)
    {
        IEnumerable<INode> children = nodes.Where(x => x.ParentId == parent.Id);
        foreach (INode child in children)
        {
            string infos = $"- {child.Name} - {child.Size} bytes - {child.CreationDate}";
            Console.WriteLine(infos.PadLeft(infos.Length + level, '\t'));
            if (child.Type == NodeType.Directory)
            {
                DisplayNodesRecursive(nodes, child, level + 1);
            }
        }
    }
    static string GetParents(INode node, IEnumerable<INode> nodes)
    {
        List<string> parents = new List<string>();
        while (node.ParentId != null)
        {
            INode parentNode = nodes.Single(x => x.Id == node.ParentId);
            parents.Insert(0, parentNode.Name);
            node = parentNode;
        }

        return string.Join('\\', parents);
    }
}