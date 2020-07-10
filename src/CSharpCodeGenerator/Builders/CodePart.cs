using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace KoyashiroKohaku.CSharpCodeGenerator.Builders
{
    public struct CodePart
    {
        public CodePart(CodePartType codePartType)
        {
            CodePartType = codePartType;
            Value = null;
            Childs = new Collection<CodePart>();
        }

        public CodePart(CodePartType codePartType, IEnumerable<CodePart> childs)
        {
            CodePartType = codePartType;
            Value = null;
            Childs = childs.ToList();
        }

        public CodePart(string value)
        {
            CodePartType = CodePartType.AnyString;
            Value = value;
            Childs = new Collection<CodePart>();
        }

        public CodePartType CodePartType { get; }

        public string? Value { get; }

        public IReadOnlyCollection<CodePart> Childs { get; }
    }
}
