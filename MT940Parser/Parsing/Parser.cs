namespace programmersdigest.MT940Parser.Parsing
{
    using programmersdigest.MT940Parser.Model;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class Parser : IParser
    {
        private string _path = null;
        private Stream _stream = null;

        public Parser()
        {
        }

        public Parser(string path)
        {
            _path = path;
        }

        public Parser(Stream stream)
        {
            _stream = stream;
        }

        public string Path { get => _path; set => _path = value; }
        public Stream Stream { get => _stream; set => _stream = value; }

        public IEnumerable<Statement> Parse()
        {
            if (string.IsNullOrEmpty(_path) && Stream == null)
            {
                throw new ArgumentNullException("Path and stream can't be both null.", null as Exception);
            }

            using (StreamReader _reader = (Stream == null) ? new StreamReader(_path) : new StreamReader(Stream))
            {
                while (!_reader.EndOfStream)
                {
                    StatementParser _statementParser = new StatementParser(_reader);

                    var statement = _statementParser.ReadStatement();

                    if (statement != null)
                    {
                        yield return statement;
                    }
                }
            }
        }

    }
}
