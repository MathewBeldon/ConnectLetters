var input = File.ReadAllLines("./letters");
foreach(var inputs in input){
    Console.WriteLine(LetterConnector.Utilities.ProcessInput(inputs));
}