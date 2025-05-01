using System.CommandLine;
using System.CommandLine.Invocation;

namespace cmdLlamaCpp;

public class CmdLlamaCppArgs
{
    public string Model;
    public string Prompt;

    public CmdLlamaCppArgs(string[] args)
    {
        this.ParseCommandLineArguments(args);
    }

    public void ParseCommandLineArguments(string[] args)
    {
        Option<string> modelOption = new Option<string>(
            "--model",
            getDefaultValue: () => "https://huggingface.co/dranger003/e5-mistral-7b-instruct-GGUF",
            "The url of model from hugging face"
        );

        Option<string> promptOption = new Option<string>(
            "--prompt",
            getDefaultValue: () => "Hello!",
            "User prompt"
        );

        RootCommand rootCommand = new RootCommand("Llama Cpp")
        {
            modelOption,
            promptOption
        };
        
        rootCommand.SetHandler((string model, string prompt) =>
        {
            if (!string.IsNullOrEmpty(model))
            {
                this.Model = model;
            }
            Console.WriteLine($"Model: {model}");

            if (!string.IsNullOrEmpty(prompt))
            {
                this.Prompt = prompt;
            }
            Console.WriteLine($"User Prompt: {prompt}");
        }, modelOption, promptOption);
        
        rootCommand.InvokeAsync(args);
    }
}