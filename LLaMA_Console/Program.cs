using LLama;
using LLama.Common;
using System.Text;

// Create model
string modelPath = "C:/Dev_Space/OS AI/models/llama-2-13b-chat.ggmlv3.q4_0.bin";
var model = new LLamaModel(new ModelParams(modelPath, contextSize: 1024, gpuLayerCount: 5));

// Initialize a chat session
//var executor = new StatelessExecutor(model);
var executor = new InteractiveExecutor(model);
var session = new ChatSession(executor);

string storagePath = "C:/Dev_Space/OS AI/chats";
session.LoadSession(storagePath);

// use the "chat-with-bob" prompt here.
var builder = new StringBuilder();
//builder
//	.AppendLine("Transcript of a dialog, where the User interacts with an Assistant named Bob.")
//	.AppendLine("Bob is helpful, kind, honest, good at writing, and never fails to answer the User's requests immediately and with precision.")
//	.AppendLine()
//	.AppendLine("User: Hello, Bob.")
//	.AppendLine("Bob: Hello. How may I help you today?")
//	.AppendLine("User: Please tell me the largest city in Europe.")
//	.AppendLine("Bob: Sure. The largest city in Europe is Moscow, the capital of Russia.")
//	.Append("User:");

builder
	.AppendLine("Provide a one paragraph comprehensive summary of the following article:")
	.AppendLine("####")
	.AppendLine(File.ReadAllText("C:/Dev_Space/OS AI/test.txt"))
	.Append("Summary:");

string prompt = builder.ToString();
builder.Clear();

// show the prompt
Console.WriteLine();
Console.Write(prompt);

// run the inference in a loop to chat with LLM
var inference = new InferenceParams() { MaxTokens = 256 };
//while (prompt != "stop")
//{
foreach (var text in session.Chat(prompt, inference))
{
	Console.Write(text);
}
//prompt = Console.ReadLine();
//}

// save the session
session.SaveSession(storagePath);
