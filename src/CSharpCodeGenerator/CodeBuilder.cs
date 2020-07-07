using System;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace KoyashiroKohaku.CSharpCodeGenerator
{
    public class CodeBuilder
    {
        private readonly POCOClass _pocoClass;
        private readonly StringBuilder _builder = new StringBuilder();
        private int _indentSize = 4;
        private int _indentCount = 0;

        public IndentStyle IndentStyle { get; set; } = IndentStyle.Space;
        public int IndentSize
        {
            get => _indentSize;
            set => _indentSize = value < 0 ? 0 : value;
        }
        public EndOfLine EndOfLine { get; set; } = EndOfLine.CRLF;

        internal CodeBuilder(POCOClass pocoClass)
        {
            _pocoClass = pocoClass;
        }

        private string GetIndentString(int indentCount)
        {
            string? indentString = IndentStyle switch
            {
                IndentStyle.Space => string.Join(string.Empty, Enumerable.Range(0, IndentSize).Select(x => " ")),
                IndentStyle.Tab => "\t",
                _ => throw new InvalidEnumArgumentException(nameof(IndentStyle), (int)IndentStyle, typeof(IndentStyle))
            };

            return string.Join(string.Empty, Enumerable.Range(0, indentCount).Select(x => indentString));
        }

        private string GetEndOfLineString()
        {
            return EndOfLine switch
            {
                EndOfLine.CR => "\r",
                EndOfLine.LF => "\n",
                EndOfLine.CRLF => "\r\n",
                _ => throw new InvalidEnumArgumentException(nameof(EndOfLine), (int)EndOfLine, typeof(EndOfLine))
            };
        }

        private CodeBuilder AppendXmlComment(string xmlComment)
        {
            _builder
                .Append(GetIndentString(_indentCount))
                .Append("/// <summary>")
                .Append(GetEndOfLineString());

            foreach (string? line in xmlComment.Replace("\r\n", "\r", StringComparison.Ordinal).Split("\r"))
            {
                _builder
                    .Append(GetIndentString(_indentCount))
                    .Append("/// ")
                    .Append(line)
                    .Append(GetEndOfLineString());
            }

            _builder
                .Append(GetIndentString(_indentCount))
                .Append("/// </summary>")
                .Append(GetEndOfLineString());

            return this;
        }

        public void DownIndent()
        {
            if (_indentCount < int.MaxValue)
            {
                _indentCount++;
            }
        }

        public void UpIndent()
        {
            if (_indentCount > 0)
            {
                _indentCount--;
            }
        }

        public CodeBuilder AppendCurlyBracket(CurlyBracket curlyBracket)
        {
            if (!Enum.IsDefined(typeof(CurlyBracket), curlyBracket))
            {
                throw new InvalidEnumArgumentException(nameof(curlyBracket), (int)curlyBracket, typeof(CurlyBracket));
            }

            _ = curlyBracket switch
            {
                CurlyBracket.Left => _builder.Append(GetIndentString(_indentCount)).Append("{").Append(GetEndOfLineString()),
                CurlyBracket.Right => _builder.Append(GetIndentString(_indentCount)).Append("}").Append(GetEndOfLineString()),
                _ => throw new InvalidEnumArgumentException(nameof(curlyBracket), (int)curlyBracket, typeof(CurlyBracket))
            };

            return this;
        }

        public CodeBuilder AppendNamespaceDeclaration()
        {
            if (string.IsNullOrEmpty(_pocoClass.Namepace))
            {
                return this;
            }

            _builder
                .Append(GetIndentString(_indentCount))
                .Append("namespace ")
                .Append(_pocoClass.Namepace)
                .Append(GetEndOfLineString());

            return this;
        }

        public CodeBuilder AppendClassDeclaration()
        {
            if (!string.IsNullOrEmpty(_pocoClass.XmlComment))
            {
                AppendXmlComment(_pocoClass.XmlComment);
            }

            _builder
                .Append(GetIndentString(_indentCount))
                .Append("public class ")
                .Append(_pocoClass.ClassName)
                .Append(GetEndOfLineString());

            return this;
        }

        public CodeBuilder AppendProperties()
        {
            if (!_pocoClass.Properties.Any())
            {
                return this;
            }

            int count = _pocoClass.Properties.Count;

            foreach ((ClassProperty property, int index) in _pocoClass.Properties.Select((p, i) => (p, i)))
            {
                if (!string.IsNullOrEmpty(property.XmlComment))
                {
                    AppendXmlComment(property.XmlComment);
                }

                _builder
                    .Append(GetIndentString(_indentCount))
                    .Append("public ")
                    .Append(property.PropertyType)
                    .Append(" ")
                    .Append(property.PropertyName)
                    .Append(" { get; set; }")
                    .Append(GetEndOfLineString());

                if (index + 1 < count)
                {
                    _builder.Append(GetEndOfLineString());
                }
            }

            return this;
        }

        public override string ToString()
        {
            return _builder.ToString();
        }
    }
}
