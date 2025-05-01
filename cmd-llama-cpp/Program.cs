using libLlamaCpp;

namespace cmdLlamaCpp;

public class Program
{
    public static async Task Main(string[] args)
    {
        CmdLlamaCppArgs cmdArgs = new CmdLlamaCppArgs(args);
        string model = cmdArgs.Model;
        string prompt = cmdArgs.Prompt;
        
        using LlmEngine llm = new LlmEngine(new LlmEngineOptions { MaxParallel = 8 });
        llm.LoadModel(model, new LlmModelOptions { GpuLayers = 32 });
        
        LlmMessage message = new LlmMessage { Content = "What is the best LLM ever?" };
        
        LlmPrompt llmPrompt = llm.Prompt(
            new List<LlmMessage> { message }
        );
        
        await foreach (var token in new TokenEnumerator(llmPrompt))
            Console.Write(token);
    }
}