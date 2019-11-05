namespace programmersdigest.MT940Parser.Parsing
{
    using programmersdigest.MT940Parser.Model;
    using System.Collections.Generic;
    using System.IO;

    public interface IParser
    {
        string Path { get; set; }
        Stream Stream { get; set; }
        IEnumerable<Statement> Parse();
    }
}
