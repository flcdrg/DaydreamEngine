using Markdig;
using Markdig.Renderers;
using Microsoft.Extensions.Logging;
using YamlDotNet.Core; // Requires NuGet package

class MyService
{
    private readonly ILogger<MyService> _logger;

    public MyService(ILogger<MyService> logger)
    {
        _logger = logger;
    }

private static readonly MarkdownPipeline Pipeline 
    = new MarkdownPipelineBuilder()
    // YAML Front Matter extension registered
    .UseAbbreviations()
    .UseYamlFrontMatter()
    .Build();


    public async Task ExecuteAsync(CancellationToken stoppingToken = default)
    {
        // Get all files in the current directory and subdirectories
        var files = Directory.GetFiles(@"C:\dev\git\flcdrg.github.io\_posts", "*.md", SearchOption.AllDirectories);
        
        foreach (var file in files)
        {
            _logger.LogInformation($"Processing {file}");
            var markdown = await File.ReadAllTextAsync(file, stoppingToken);
            var markdownDocument = Markdown.Parse(markdown, Pipeline);

            var htmlFile = Path.ChangeExtension(file.Replace(@"C:\dev\git\flcdrg.github.io\_posts\", @"C:\dev\git\flcdrg.github.io\output\"), ".html");

            try
            {
                var frontMatter = markdownDocument.GetFrontMatter<BlogFrontMatter>();
            }
            catch (YamlException ex)
            {
                
                throw;
            }
            

            // frontMatter.Title
            var html = Markdown.ToHtml(markdownDocument, Pipeline);

            // ensure output directory exists
            var outputDirectory = Path.GetDirectoryName(htmlFile);
            if (outputDirectory != null && !Directory.Exists(outputDirectory)) {
                Directory.CreateDirectory(outputDirectory);
            }

            await File.WriteAllTextAsync(htmlFile, html, stoppingToken);
        }

        _logger.LogInformation("Doing something");
    }
}
