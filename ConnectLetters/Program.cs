var inputs = File.ReadAllLines("./letters");

foreach(var input in inputs)
{
    Console.WriteLine($"ChatGPT: {input} {ConnectLetters.ChatGPT.ProcessInput(input)}");
}
foreach (var input in inputs)
{
    Console.WriteLine($"Answers: {input} {ConnectLetters.Matt.ProcessInput(input)}");
}