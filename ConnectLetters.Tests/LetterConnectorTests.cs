namespace ConnectLetters.Tests;
public class LetterConnectorTests{
    
    [Theory]
    [InlineData("ABABABAB", 4)]
    [InlineData("AAAAAAAA", 0)]
    [InlineData("BABBYYAYAAB", 4)]
    [InlineData("XYABXYXYAB", 5)]
    [InlineData("AXXBXYABXAYB", 5)]
    [InlineData("BAXBAAXYBAXXBYYBYAAAXAYBYBXAAXYXXYAAXAYYXYYAABXAAXYBYYXBB", 23)]
    public void TestEoghans(string input, int expected){
        Assert.Equal(expected, ConnectLetters.Eoghan.ProcessInput(input));
    }

    [Theory]
    [InlineData("ABABABAB", 4)]
    [InlineData("AAAAAAAA", 0)]
    [InlineData("BABBYYAYAAB", 4)]
    [InlineData("XYABXYXYAB", 5)]
    [InlineData("AXXBXYABXAYB", 5)]
    [InlineData("BAXBAAXYBAXXBYYBYAAAXAYBYBXAAXYXXYAAXAYYXYYAABXAAXYBYYXBB", 23)]
    public void TestMatts(string input, int expected)
    {
        Assert.Equal(expected, ConnectLetters.Matt.ProcessInput(input));
    }
}
