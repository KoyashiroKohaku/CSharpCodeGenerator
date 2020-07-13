using KoyashiroKohaku.CSharpCodeGenerator.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace KoyashiroKohaku.CSharpCodeGenerator.Builders
{
    public abstract class CodeBuilderBase : ICodeBuilder
    {
        private readonly List<Token> _tokens = new List<Token>();
        private IndentStyle _indentStyle = IndentStyle.Space;
        private int _indentSize = 4;
        private int _indentDepth = 0;
        private EndOfLine _endOfLine = EndOfLine.CRLF;

        public string CurrentIndentString => GetIndentString(IndentStyle, IndentSize);

        public string CurrentIndentStringWithDepth => GetIndentStringWithDepth(CurrentIndentString, IndentDepth);

        public string CurrentEndOfLineString => GetEndOfLineString(EndOfLine);

        public IndentStyle IndentStyle
        {
            get => _indentStyle;
            set
            {
                if (!Enum.IsDefined(typeof(IndentStyle), value))
                {
                    throw new InvalidEnumArgumentException(nameof(IndentStyle), (int)IndentStyle, typeof(IndentStyle));
                }

                _indentStyle = value;
            }
        }

        public int IndentSize
        {
            get => _indentSize;
            set => _indentSize = value < 0 ? 0 : value;
        }

        public int IndentDepth
        {
            get => _indentDepth;
            set => _indentDepth = value < 0 ? 0 : value;
        }

        public EndOfLine EndOfLine
        {
            get => _endOfLine;
            set
            {
                if (!Enum.IsDefined(typeof(EndOfLine), value))
                {
                    throw new InvalidEnumArgumentException(nameof(EndOfLine), (int)EndOfLine, typeof(EndOfLine));
                }

                _endOfLine = value;
            }
        }

        public IReadOnlyList<Token> Tokens => _tokens;

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
            if (indentString is null)
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

        public ICodeBuilder Indent()
        {
            if (IndentDepth < int.MaxValue)
            {
                _indentDepth++;
            }

            return this;
        }

        public ICodeBuilder Unindent()
        {
            if (IndentDepth > 0)
            {
                _indentDepth--;
            }

            return this;
        }

        public ICodeBuilder Append(TokenType tokenType)
        {
            if (!Enum.IsDefined(typeof(TokenType), tokenType))
            {
                throw new InvalidEnumArgumentException(nameof(tokenType), (int)tokenType, typeof(TokenType));
            }

            _tokens.Add(new Token(tokenType));

            return this;
        }

        public ICodeBuilder Append(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return this;
            }

            _tokens.Add(new Token(value));

            return this;
        }

        public ICodeBuilder AppendLine()
        {
            Append(TokenType.EndOfLine);

            return this;
        }

        public ICodeBuilder AppendLine(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return this;
            }

            Append(value).AppendLine();

            return this;
        }

        public abstract ICodeBuilder AppendIndent();

        public abstract ICodeBuilder AppendCurlyBracket(CurlyBracket curlyBracket);

        public abstract ICodeBuilder AppendDocumentationComment();

        public abstract ICodeBuilder AppendXmlCommentTag(string tagName, XmlCommentTag xmlCommentTag);

        public abstract ICodeBuilder AppendUsingDirective(string namespaceString);

        public abstract ICodeBuilder AppendNamespaceDeclaration(string namespaceName);

        public override string ToString()
        {
            var builder = new StringBuilder();

            foreach (var token in Tokens)
            {
                // TODO: to switch
                if (token.IsEndOfLine)
                {
                    builder.Append(CurrentEndOfLineString);
                    continue;
                }

                if (token.IsIndent)
                {
                    builder.Append(CurrentIndentString);
                    continue;
                }

                if (token.IsKeyword)
                {
                    builder.Append(KeywordHelper.GetValue(token.GetKeyword()));
                    continue;
                }

                if (token.IsContextualKeyword)
                {
                    builder.Append(ContextualKeywordHelper.GetValue(token.GetContextualKeyword()));
                    continue;
                }

                if (token.IsASCIIChar)
                {
                    builder.Append(ASCIICharHelper.GetValue(token.GetASCIIChar()));
                    continue;
                }

                if (token.IsAnyString)
                {
                    builder.Append(token.GetAnyString());
                    continue;
                }
            }

            return builder.ToString();
        }
    }
}
