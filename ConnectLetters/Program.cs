var input = File.ReadAllLines("./letters");

foreach(var inputs in input){
    Console.WriteLine(ConnectLetters.Eoghan.ProcessInput(inputs));
}
foreach (var inputs in input)
{
    Console.WriteLine(ConnectLetters.Matt.ProcessInput(inputs));
}