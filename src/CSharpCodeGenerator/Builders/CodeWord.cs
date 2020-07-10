using KoyashiroKohaku.CSharpCodeGenerator.Helpers;
using System;

namespace KoyashiroKohaku.CSharpCodeGenerator.Builders
{
    public struct CodeWord : IEquatable<CodeWord>
    {
        internal CodeWord(Keyword keyword)
        {
            ValueString = KeywordHelper.GetValue(keyword);
            CodeWordType = CodeWordType.Keywords;
        }

        internal CodeWord(ContextualKeyword contextualKeyword)
        {
            ValueString = ContextualKeywordHelper.GetValue(contextualKeyword);
            CodeWordType = CodeWordType.ContextualKeyword;
        }

        public CodeWord(string value)
        {
            ValueString = value;
            CodeWordType = CodeWordType.AnyString;
        }

        public string ValueString { get; }

        public CodeWordType CodeWordType { get; }

        public static CodeWord Abstract => new CodeWord(Keyword.Abstract);
        public static CodeWord As => new CodeWord(Keyword.As);
        public static CodeWord Base => new CodeWord(Keyword.Base);
        public static CodeWord Bool => new CodeWord(Keyword.Bool);
        public static CodeWord Break => new CodeWord(Keyword.Break);
        public static CodeWord Byte => new CodeWord(Keyword.Byte);
        public static CodeWord Case => new CodeWord(Keyword.Case);
        public static CodeWord Catch => new CodeWord(Keyword.Catch);
        public static CodeWord Char => new CodeWord(Keyword.Char);
        public static CodeWord Checked => new CodeWord(Keyword.Checked);
        public static CodeWord Class => new CodeWord(Keyword.Class);
        public static CodeWord Const => new CodeWord(Keyword.Const);
        public static CodeWord Continue => new CodeWord(Keyword.Continue);
        public static CodeWord Decimal => new CodeWord(Keyword.Decimal);
        public static CodeWord Default => new CodeWord(Keyword.Default);
        public static CodeWord Delegate => new CodeWord(Keyword.Delegate);
        public static CodeWord Do => new CodeWord(Keyword.Do);
        public static CodeWord Double => new CodeWord(Keyword.Double);
        public static CodeWord Else => new CodeWord(Keyword.Else);
        public static CodeWord Enum => new CodeWord(Keyword.Enum);
        public static CodeWord Event => new CodeWord(Keyword.Event);
        public static CodeWord Explicit => new CodeWord(Keyword.Explicit);
        public static CodeWord Extern => new CodeWord(Keyword.Extern);
        public static CodeWord False => new CodeWord(Keyword.False);
        public static CodeWord Finally => new CodeWord(Keyword.Finally);
        public static CodeWord Fixed => new CodeWord(Keyword.Fixed);
        public static CodeWord Float => new CodeWord(Keyword.Float);
        public static CodeWord For => new CodeWord(Keyword.For);
        public static CodeWord Foreach => new CodeWord(Keyword.Foreach);
        public static CodeWord Goto => new CodeWord(Keyword.Goto);
        public static CodeWord If => new CodeWord(Keyword.If);
        public static CodeWord Implicit => new CodeWord(Keyword.Implicit);
        public static CodeWord In => new CodeWord(Keyword.In);
        public static CodeWord Int => new CodeWord(Keyword.Int);
        public static CodeWord Interface => new CodeWord(Keyword.Interface);
        public static CodeWord Internal => new CodeWord(Keyword.Internal);
        public static CodeWord Is => new CodeWord(Keyword.Is);
        public static CodeWord Lock => new CodeWord(Keyword.Lock);
        public static CodeWord Long => new CodeWord(Keyword.Long);
        public static CodeWord Namespace => new CodeWord(Keyword.Namespace);
        public static CodeWord New => new CodeWord(Keyword.New);
        public static CodeWord Null => new CodeWord(Keyword.Null);
        public static CodeWord Object => new CodeWord(Keyword.Object);
        public static CodeWord Operator => new CodeWord(Keyword.Operator);
        public static CodeWord Out => new CodeWord(Keyword.Out);
        public static CodeWord Override => new CodeWord(Keyword.Override);
        public static CodeWord Params => new CodeWord(Keyword.Params);
        public static CodeWord Private => new CodeWord(Keyword.Private);
        public static CodeWord Protected => new CodeWord(Keyword.Protected);
        public static CodeWord Public => new CodeWord(Keyword.Public);
        public static CodeWord Readonly => new CodeWord(Keyword.Readonly);
        public static CodeWord Ref => new CodeWord(Keyword.Ref);
        public static CodeWord Return => new CodeWord(Keyword.Return);
        public static CodeWord Sbyte => new CodeWord(Keyword.Sbyte);
        public static CodeWord Sealed => new CodeWord(Keyword.Sealed);
        public static CodeWord Short => new CodeWord(Keyword.Short);
        public static CodeWord Sizeof => new CodeWord(Keyword.Sizeof);
        public static CodeWord Stackalloc => new CodeWord(Keyword.Stackalloc);
        public static CodeWord Static => new CodeWord(Keyword.Static);
        public static CodeWord String => new CodeWord(Keyword.String);
        public static CodeWord Struct => new CodeWord(Keyword.Struct);
        public static CodeWord Switch => new CodeWord(Keyword.Switch);
        public static CodeWord This => new CodeWord(Keyword.This);
        public static CodeWord Throw => new CodeWord(Keyword.Throw);
        public static CodeWord True => new CodeWord(Keyword.True);
        public static CodeWord Try => new CodeWord(Keyword.Try);
        public static CodeWord Typeof => new CodeWord(Keyword.Typeof);
        public static CodeWord Uint => new CodeWord(Keyword.Uint);
        public static CodeWord Ulong => new CodeWord(Keyword.Ulong);
        public static CodeWord Unchecked => new CodeWord(Keyword.Unchecked);
        public static CodeWord Unsafe => new CodeWord(Keyword.Unsafe);
        public static CodeWord Ushort => new CodeWord(Keyword.Ushort);
        public static CodeWord Using => new CodeWord(Keyword.Using);
        public static CodeWord Virtual => new CodeWord(Keyword.Virtual);
        public static CodeWord Void => new CodeWord(Keyword.Void);
        public static CodeWord Volatile => new CodeWord(Keyword.Volatile);
        public static CodeWord While => new CodeWord(Keyword.While);

        public static CodeWord Add => new CodeWord(ContextualKeyword.Add);
        public static CodeWord Alias => new CodeWord(ContextualKeyword.Alias);
        public static CodeWord Ascending => new CodeWord(ContextualKeyword.Ascending);
        public static CodeWord Async => new CodeWord(ContextualKeyword.Async);
        public static CodeWord Await => new CodeWord(ContextualKeyword.Await);
        public static CodeWord By => new CodeWord(ContextualKeyword.By);
        public static CodeWord Descending => new CodeWord(ContextualKeyword.Descending);
        public static CodeWord Dynamic => new CodeWord(ContextualKeyword.Dynamic);
        public static CodeWord EqualsKeyword => new CodeWord(ContextualKeyword.Equals);
        public static CodeWord From => new CodeWord(ContextualKeyword.From);
        public static CodeWord Get => new CodeWord(ContextualKeyword.Get);
        public static CodeWord Global => new CodeWord(ContextualKeyword.Global);
        public static CodeWord Group => new CodeWord(ContextualKeyword.Group);
        public static CodeWord Into => new CodeWord(ContextualKeyword.Into);
        public static CodeWord Join => new CodeWord(ContextualKeyword.Join);
        public static CodeWord Let => new CodeWord(ContextualKeyword.Let);
        public static CodeWord Nameof => new CodeWord(ContextualKeyword.Nameof);
        public static CodeWord On => new CodeWord(ContextualKeyword.On);
        public static CodeWord Orderby => new CodeWord(ContextualKeyword.Orderby);
        public static CodeWord PartialType => new CodeWord(ContextualKeyword.PartialType);
        public static CodeWord PartialMethod => new CodeWord(ContextualKeyword.PartialMethod);
        public static CodeWord Remove => new CodeWord(ContextualKeyword.Remove);
        public static CodeWord Select => new CodeWord(ContextualKeyword.Select);
        public static CodeWord Set => new CodeWord(ContextualKeyword.Set);
        public static CodeWord UnmanagedGenericTypeConstraint => new CodeWord(ContextualKeyword.UnmanagedGenericTypeConstraint);
        public static CodeWord Value => new CodeWord(ContextualKeyword.Value);
        public static CodeWord Var => new CodeWord(ContextualKeyword.Var);
        public static CodeWord WhenFilterCondition => new CodeWord(ContextualKeyword.WhenFilterCondition);
        public static CodeWord WhereGenericTypeConstraint => new CodeWord(ContextualKeyword.WhereGenericTypeConstraint);
        public static CodeWord WhereQueryClause => new CodeWord(ContextualKeyword.WhereQueryClause);
        public static CodeWord Yield => new CodeWord(ContextualKeyword.Yield);

        public override bool Equals(object obj)
        {
            if (obj is CodeWord part)
            {
                return Equals(part);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ValueString, CodeWordType);
        }

        public static bool operator ==(CodeWord left, CodeWord right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(CodeWord left, CodeWord right)
        {
            return !(left == right);
        }

        public bool Equals(CodeWord other)
        {
            return (ValueString == other.ValueString) && (CodeWordType == other.CodeWordType);
        }

        public override string ToString()
        {
            return ValueString;
        }
    }
}
