using BenchmarkDotNet.Attributes;

namespace ConnectLetters.Benchmarks
{
    [MemoryDiagnoser]
    public class ConnectLettersBenchmark
    {
        [Arguments("ABA", 1)]
        [Arguments("AXBY", 1)]
        [Arguments("ABBA", 2)]
        [Arguments("AABB", 2)]
        [Arguments("ABABABAB", 4)]
        [Arguments("AAAAAAAA", 0)]
        [Arguments("BABBYYAYAAB", 4)]
        [Arguments("XYABXYXYAB", 5)]
        [Arguments("AXXBXYABXAYB", 5)]
        [Arguments("ABBAXYYXXXBA", 5)]
        [Arguments("XBXAYABXBYAY", 6)]
        [Arguments("XYABXYXYABABBAXYYXXXBAXBXAYABXBYAY", 16)]
        [Arguments("YAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAX", 1)]
        [Arguments("BAXBAAXYBAXXBYYBYAAAXAYBYBXAAXYXXYAAXAYYXYYAABXAAXYBYYXBBBBBYXYAXXXXBXXAAYXYYYYXXYYAYYYAXBXABYAAXXXA", 42)]
        public void RunChatGPT(string connections, int matches)
        {
            ChatGPT.ProcessInput(connections);
        }

        [Arguments("ABA",  1)]
        [Arguments("AXBY", 1)]
        [Arguments("ABBA",  2)]
        [Arguments("AABB",  2)]
        [Arguments("ABABABAB",  4)]
        [Arguments("AAAAAAAA",  0)]
        [Arguments("BABBYYAYAAB",  4)]
        [Arguments("XYABXYXYAB",  5)]
        [Arguments("AXXBXYABXAYB",  5)]
        [Arguments("ABBAXYYXXXBA",  5)]
        [Arguments("XBXAYABXBYAY",  6)]
        [Arguments("XYABXYXYABABBAXYYXXXBAXBXAYABXBYAY",  16)]
        [Arguments("YAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAX",  1)]
        [Arguments("BAXBAAXYBAXXBYYBYAAAXAYBYBXAAXYXXYAAXAYYXYYAABXAAXYBYYXBBBBBYXYAXXXXBXXAAYXYYYYXXYYAYYYAXBXABYAAXXXA",  42)]
        [Benchmark(Description = "Matt's Connect Letters")]
        public void RunMatt(string connections, int matches)
        {
            Matt.ProcessInput(connections);
        }
    }
}
