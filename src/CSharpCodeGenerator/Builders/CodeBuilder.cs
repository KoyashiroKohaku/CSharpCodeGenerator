using System;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace KoyashiroKohaku.CSharpCodeGenerator.Builders
{
    public class CodeBuilder
    {
        private readonly POCOClass _pocoClass;
        private readonly StringBuilder _builder = new StringBuilder();
        private IndentStyle _indentStyle = IndentStyle.Space;
        private int _indentSize = 4;

        public CodeBuilder(POCOClass pocoClass)
        {
            _pocoClass = pocoClass;
        }

        public IndentStyle IndentStyle
        {
            get => _indentStyle;
            set
            {
                var oldValue = IndentStyle switch
                {
                    IndentStyle.Space => string.Join(string.Empty, Enumerable.Range(0, IndentSize).Select(x => " ")),
                    IndentStyle.Tab => "\t",
                    _ => throw new InvalidEnumArgumentException(nameof(IndentStyle), (int)IndentStyle, typeof(IndentStyle))
                };

                var newValue = value switch
                {
                    IndentStyle.Space => string.Join(string.Empty, Enumerable.Range(0, IndentSize).Select(x => " ")),
                    IndentStyle.Tab => "\t",
                    _ => throw new InvalidEnumArgumentException(nameof(IndentStyle), (int)IndentStyle, typeof(IndentStyle))
                };

                _builder.Replace(oldValue, newValue);

                _indentStyle = value;
            }
        }

        public int IndentSize
        {
            get => _indentSize;
            set
            {
                if (IndentStyle == IndentStyle.Space)
                {
                    var oldValue = string.Join(string.Empty, Enumerable.Range(0, IndentSize).Select(x => " "));

                    var newValue = string.Join(string.Empty, Enumerable.Range(0, value).Select(x => " "));

                    _builder.Replace(oldValue, newValue);
                }

                _indentSize = value < 0 ? 0 : value;
            }
        }

        public int IndentDepth { get; private set; } = 0;

        public EndOfLine EndOfLine { get; set; } = EndOfLine.CRLF;

        public CodeBuilder DownIndent()
        {
            if (IndentDepth < int.MaxValue)
            {
                IndentDepth++;
            }

            return this;
        }

        public CodeBuilder UpIndent()
        {
            if (IndentDepth > 0)
            {
                IndentDepth--;
            }

            return this;
        }

        public string GetIndentString()
        {
            var indentString = IndentStyle switch
            {
                IndentStyle.Space => string.Join(string.Empty, Enumerable.Range(0, IndentSize).Select(x => " ")),
                IndentStyle.Tab => "\t",
                _ => throw new InvalidEnumArgumentException(nameof(IndentStyle), (int)IndentStyle, typeof(IndentStyle))
            };

            return string.Join(string.Empty, Enumerable.Range(0, IndentDepth).Select(x => indentString));
        }

        public string GetEndOfLineString()
        {
            return EndOfLine switch
            {
                EndOfLine.CR => "\r",
                EndOfLine.LF => "\n",
                EndOfLine.CRLF => "\r\n",
                _ => throw new InvalidEnumArgumentException(nameof(EndOfLine), (int)EndOfLine, typeof(EndOfLine))
            };
        }

        public CodeBuilder Append(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            _builder.Append(value);

            return this;
        }

        public CodeBuilder AppendLine()
        {
            _builder.Append(GetEndOfLineString());

            return this;
        }

        public CodeBuilder AppendLine(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            _builder.Append(value).Append(GetEndOfLineString());

            return this;
        }

        public CodeBuilder AppendIndent()
        {
            _builder.Append(GetIndentString());

            return this;
        }

        public CodeBuilder AppendCurlyBracket(CurlyBracket curlyBracket)
        {
            if (!Enum.IsDefined(typeof(CurlyBracket), curlyBracket))
            {
                throw new InvalidEnumArgumentException(nameof(curlyBracket), (int)curlyBracket, typeof(CurlyBracket));
            }

            _ = curlyBracket switch
            {
                CurlyBracket.Left => Append("{"),
                CurlyBracket.Right => Append("}"),
                _ => throw new InvalidEnumArgumentException(nameof(curlyBracket), (int)curlyBracket, typeof(CurlyBracket))
            };

            return this;
        }

        public CodeBuilder AppendXmlComment(string xmlComment)
        {
            if (xmlComment == null)
            {
                throw new ArgumentNullException(nameof(xmlComment));
            }

            var lines = _builder.ToString().Split(GetEndOfLineString());
            var hasIndent = lines.Any() && lines.Last() == GetIndentString();

            Append("/// <summary>").AppendLine();

            foreach (var line in xmlComment.Split(GetIndentString()))
            {
                if (hasIndent)
                {
                    AppendIndent();
                }

                Append("/// ").Append(line).AppendLine();
            }

            if (hasIndent)
            {
                AppendIndent();
            }

            Append("/// </summary>");

            return this;
        }

        public CodeBuilder AppendNamespaceDeclaration()
        {
            if (string.IsNullOrEmpty(_pocoClass.Namepace))
            {
                return this;
            }

            Append("namespace ").Append(_pocoClass.Namepace);

            return this;
        }

        public CodeBuilder AppendClassXmlComment()
        {
            if (!string.IsNullOrEmpty(_pocoClass.XmlComment))
            {
                AppendXmlComment(_pocoClass.XmlComment);
            }

            return this;
        }

        public CodeBuilder AppendClassDeclaration()
        {
            Append("public class ").Append(_pocoClass.ClassName);

            return this;
        }

        public CodeBuilder AppendProperties()
        {
            if (!_pocoClass.Properties.Any())
            {
                return this;
            }

            var lines = _builder.ToString().Split(GetEndOfLineString());
            var hasIndent = lines.Any() && lines.Last() == GetIndentString();

            int count = _pocoClass.Properties.Count;

            foreach ((var property, int index) in _pocoClass.Properties.Select((p, i) => (p, i)))
            {
                if (index == 0)
                {
                    if (!string.IsNullOrEmpty(property.XmlComment))
                    {
                        AppendXmlComment(property.XmlComment).AppendLine();

                        if (hasIndent)
                        {
                            AppendIndent();
                        }
                    }
                }
                else
                {
                    if (hasIndent)
                    {
                        AppendIndent();
                    }

                    if (!string.IsNullOrEmpty(property.XmlComment))
                    {
                        AppendXmlComment(property.XmlComment).AppendLine();

                        if (hasIndent)
                        {
                            AppendIndent();
                        }
                    }
                }

                Append("public ")
                    .Append(TypeResolver.GetTypeAlias(property.PropertyType))
                    .Append(" ")
                    .Append(property.PropertyName)
                    .Append(" { get; set; }");

                if (index + 1 < count)
                {
                    AppendLine().AppendLine();
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
