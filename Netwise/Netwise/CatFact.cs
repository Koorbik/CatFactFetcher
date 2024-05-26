namespace Netwise
{
    internal class CatFact
    {
        public string Fact { get; set; } = string.Empty;
        public int Length { get; set; } = 0;

        public override string ToString()
        {
            return $"Fact: {Fact} Length: {Length}\n";
        }
    }
}
