using Xunit;

namespace Tests
{
    public class LetterConnectorTests{
        
        [Theory]
        [InlineData("ABABABAB", 4)]
        [InlineData("AAAAAAAA", 0)]
        [InlineData("BABBYYAYAAB", 4)]
        [InlineData("XYABXYXYAB", 5)]
        [InlineData("AXXBXYABXAYB", 4)]
        [InlineData("BAXBAAXYBAXXBYYBYAAAXAYBYBXAAXYXXYAAXAYYXYYAABXAAXYBYYXBB", 23)]
        public void Test(string input, int expected){
            Assert.Equal(expected, LetterConnector.Utilities.ProcessInput(input));
        }
    }
}