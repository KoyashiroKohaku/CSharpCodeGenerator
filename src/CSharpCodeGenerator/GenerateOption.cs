namespace KoyashiroKohaku.CSharpCodeGenerator
{
    public class GenerateOption
    {
        public IndentStyle IndentStyle { get; set; } = IndentStyle.Space;

        public int IndentSize { get; set; } = 4;

        public EndOfLine EndOfLine { get; set; } = EndOfLine.CRLF;
    }
}
