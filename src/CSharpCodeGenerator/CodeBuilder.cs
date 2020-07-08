using System;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace KoyashiroKohaku.CSharpCodeGenerator
{
    public class CodeBuilder
    {
        private readonly StringBuilder _builder = new StringBuilder();
        private IndentStyle _indentStyle = IndentStyle.Space;
        private int _indentSize = 4;
        private int _indentDepth = 0;

        public string CurrentIndentString => GetIndentString(IndentStyle, IndentSize);

        public string CurrentIndentStringWithDepth => GetIndentStringWithDepth(CurrentIndentString, IndentDepth);

        public string CurrentEndOfLineString => GetEndOfLineString(EndOfLine);

        public IndentStyle IndentStyle
        {
            get => _indentStyle;
            set
            {
                var oldValue = IndentStyle switch
                {
                    IndentStyle.Space => GetIndentString(IndentStyle.Space, IndentSize),
                    IndentStyle.Tab => "\t",
                    _ => throw new InvalidEnumArgumentException(nameof(IndentStyle), (int)IndentStyle, typeof(IndentStyle))
                };

                var newValue = value switch
                {
                    IndentStyle.Space => GetIndentString(IndentStyle.Space, IndentSize),
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
                    var oldValue = GetIndentString(IndentStyle.Space, IndentSize);

                    var newValue = GetIndentString(IndentStyle.Space, value);

                    _builder.Replace(oldValue, newValue);
                }

                _indentSize = value < 0 ? 0 : value;
            }
        }

        public int IndentDepth
        {
            get => _indentDepth;
            set
            {
                if (value < 0)
                {
                    _indentDepth = 0;
                }
                else if (value > int.MaxValue)
                {
                    _indentDepth = int.MaxValue;
                }
                else
                {
                    _indentDepth = value;
                }
            }
        }

        public EndOfLine EndOfLine { get; set; } = EndOfLine.CRLF;

        public static string GetIndentString(IndentStyle indentStyle, int indentSize)
        {
            var indentString = indentStyle switch
            {
                IndentStyle.Space => string.Concat(Enumerable.Repeat(" ", indentSize)),
                IndentStyle.Tab => "\t",
                _ => throw new InvalidEnumArgumentException(nameof(indentStyle), (int)indentStyle, typeof(IndentStyle))
            };

            return indentString;
        }

        public static string GetIndentStringWithDepth(IndentStyle indentStyle, int indentSize, int indentDepth)
        {
            var indentString = GetIndentString(indentStyle, indentSize);

            return GetIndentStringWithDepth(indentString, indentDepth);
        }

        public static string GetIndentStringWithDepth(string indentString, int indentDepth)
        {
            if (indentString == null)
            {
                throw new ArgumentNullException(nameof(indentString));
            }

            return string.Concat(Enumerable.Repeat(indentString, indentDepth));
        }

        public static string GetEndOfLineString(EndOfLine endOfLine)
        {
            return endOfLine switch
            {
                EndOfLine.CR => "\r",
                EndOfLine.LF => "\n",
                EndOfLine.CRLF => "\r\n",
                _ => throw new InvalidEnumArgumentException(nameof(endOfLine), (int)endOfLine, typeof(EndOfLine))
            };
        }

        public CodeBuilder Indent()
        {
            IndentDepth++;

            return this;
        }

        public CodeBuilder Unindent()
        {
            IndentDepth--;

            return this;
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

        public CodeBuilder Append(ReadOnlySpan<char> value)
        {
            _builder.Append(value);

            return this;
        }

        public CodeBuilder AppendLine()
        {
            Append(CurrentEndOfLineString);

            return this;
        }

        public CodeBuilder AppendLine(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Append(value).Append(CurrentEndOfLineString);

            return this;
        }

        public CodeBuilder AppendLine(ReadOnlySpan<char> value)
        {
            Append(value).Append(CurrentEndOfLineString);

            return this;
        }

        public CodeBuilder AppendIndent()
        {
            Append(CurrentIndentStringWithDepth);

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

            var lines = ToString().Split(CurrentEndOfLineString);
            var hasIndent = lines.Any() && lines.Last() == CurrentIndentStringWithDepth;

            Append("/// <summary>").AppendLine();

            foreach (var line in xmlComment.Split(CurrentIndentString))
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

        public CodeBuilder AppendNamespaceDeclaration(string namespaceName)
        {
            if (string.IsNullOrEmpty(namespaceName))
            {
                return this;
            }

            Append("namespace ").Append(namespaceName);

            return this;
        }

        public CodeBuilder AppendClassDeclaration(string className)
        {
            if (string.IsNullOrEmpty(className))
            {
                return this;
            }

            Append("public class ").Append(className);

            return this;
        }

        public CodeBuilder AppendProperty(ClassProperty property)
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            Append("public ")
                    .Append(TypeResolver.GetTypeAlias(property.PropertyType))
                    .Append(" ")
                    .Append(property.PropertyName)
                    .Append(" { get; set; }");

            return this;
        }

        public override string ToString()
        {
            return _builder.ToString();
        }
    }
}
