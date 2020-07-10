using KoyashiroKohaku.CSharpCodeGenerator.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace KoyashiroKohaku.CSharpCodeGenerator.Builders
{
    public struct CodeWord : IEquatable<CodeWord>
    {
        private static Dictionary<Keyword, CodeWordType> Keywords => new Dictionary<Keyword, CodeWordType>
        {
            { Keyword.Abstract, CodeWordType.Abstract },
            { Keyword.As, CodeWordType.As },
            { Keyword.Base, CodeWordType.Base },
            { Keyword.Bool, CodeWordType.Bool },
            { Keyword.Break, CodeWordType.Break },
            { Keyword.Byte, CodeWordType.Byte },
            { Keyword.Case, CodeWordType.Case },
            { Keyword.Catch, CodeWordType.Catch },
            { Keyword.Char, CodeWordType.Char },
            { Keyword.Checked, CodeWordType.Checked },
            { Keyword.Class, CodeWordType.Class },
            { Keyword.Const, CodeWordType.Const },
            { Keyword.Continue, CodeWordType.Continue },
            { Keyword.Decimal, CodeWordType.Decimal },
            { Keyword.Default, CodeWordType.Default },
            { Keyword.Delegate, CodeWordType.Delegate },
            { Keyword.Do, CodeWordType.Do },
            { Keyword.Double, CodeWordType.Double },
            { Keyword.Else, CodeWordType.Else },
            { Keyword.Enum, CodeWordType.Enum },
            { Keyword.Event, CodeWordType.Event },
            { Keyword.Explicit, CodeWordType.Explicit },
            { Keyword.Extern, CodeWordType.Extern },
            { Keyword.False, CodeWordType.False },
            { Keyword.Finally, CodeWordType.Finally },
            { Keyword.Fixed, CodeWordType.Fixed },
            { Keyword.Float, CodeWordType.Float },
            { Keyword.For, CodeWordType.For },
            { Keyword.Foreach, CodeWordType.Foreach },
            { Keyword.Goto, CodeWordType.Goto },
            { Keyword.If, CodeWordType.If },
            { Keyword.Implicit, CodeWordType.Implicit },
            { Keyword.In, CodeWordType.In },
            { Keyword.Int, CodeWordType.Int },
            { Keyword.Interface, CodeWordType.Interface },
            { Keyword.Internal, CodeWordType.Internal },
            { Keyword.Is, CodeWordType.Is },
            { Keyword.Lock, CodeWordType.Lock },
            { Keyword.Long, CodeWordType.Long },
            { Keyword.Namespace, CodeWordType.Namespace },
            { Keyword.New, CodeWordType.New },
            { Keyword.Null, CodeWordType.Null },
            { Keyword.Object, CodeWordType.Object },
            { Keyword.Operator, CodeWordType.Operator },
            { Keyword.Out, CodeWordType.Out },
            { Keyword.Override, CodeWordType.Override },
            { Keyword.Params, CodeWordType.Params },
            { Keyword.Private, CodeWordType.Private },
            { Keyword.Protected, CodeWordType.Protected },
            { Keyword.Public, CodeWordType.Public },
            { Keyword.Readonly, CodeWordType.Readonly },
            { Keyword.Ref, CodeWordType.Ref },
            { Keyword.Return, CodeWordType.Return },
            { Keyword.Sbyte, CodeWordType.Sbyte },
            { Keyword.Sealed, CodeWordType.Sealed },
            { Keyword.Short, CodeWordType.Short },
            { Keyword.Sizeof, CodeWordType.Sizeof },
            { Keyword.Stackalloc, CodeWordType.Stackalloc },
            { Keyword.Static, CodeWordType.Static },
            { Keyword.String, CodeWordType.String },
            { Keyword.Struct, CodeWordType.Struct },
            { Keyword.Switch, CodeWordType.Switch },
            { Keyword.This, CodeWordType.This },
            { Keyword.Throw, CodeWordType.Throw },
            { Keyword.True, CodeWordType.True },
            { Keyword.Try, CodeWordType.Try },
            { Keyword.Typeof, CodeWordType.Typeof },
            { Keyword.Uint, CodeWordType.Uint },
            { Keyword.Ulong, CodeWordType.Ulong },
            { Keyword.Unchecked, CodeWordType.Unchecked },
            { Keyword.Unsafe, CodeWordType.Unsafe },
            { Keyword.Ushort, CodeWordType.Ushort },
            { Keyword.Using, CodeWordType.Using },
            { Keyword.Virtual, CodeWordType.Virtual },
            { Keyword.Void, CodeWordType.Void },
            { Keyword.Volatile, CodeWordType.Volatile },
            { Keyword.While, CodeWordType.While }
        };

        private static Dictionary<ContextualKeyword, CodeWordType> ContextualKeywords => new Dictionary<ContextualKeyword, CodeWordType>
        {
            { ContextualKeyword.Add, CodeWordType.Add },
            { ContextualKeyword.Alias, CodeWordType.Alias },
            { ContextualKeyword.Ascending, CodeWordType.Ascending },
            { ContextualKeyword.Async, CodeWordType.Async },
            { ContextualKeyword.Await, CodeWordType.Await },
            { ContextualKeyword.By, CodeWordType.By },
            { ContextualKeyword.Descending, CodeWordType.Descending },
            { ContextualKeyword.Dynamic, CodeWordType.Dynamic },
            { ContextualKeyword.Equals, CodeWordType.Equals },
            { ContextualKeyword.From, CodeWordType.From },
            { ContextualKeyword.Get, CodeWordType.Get },
            { ContextualKeyword.Global, CodeWordType.Global },
            { ContextualKeyword.Group, CodeWordType.Group },
            { ContextualKeyword.Into, CodeWordType.Into },
            { ContextualKeyword.Join, CodeWordType.Join },
            { ContextualKeyword.Let, CodeWordType.Let },
            { ContextualKeyword.Nameof, CodeWordType.Nameof },
            { ContextualKeyword.On, CodeWordType.On },
            { ContextualKeyword.Orderby, CodeWordType.Orderby },
            { ContextualKeyword.PartialType, CodeWordType.PartialType },
            { ContextualKeyword.PartialMethod, CodeWordType.PartialMethod },
            { ContextualKeyword.Remove, CodeWordType.Remove },
            { ContextualKeyword.Select, CodeWordType.Select },
            { ContextualKeyword.Set, CodeWordType.Set },
            { ContextualKeyword.UnmanagedGenericTypeConstraint, CodeWordType.UnmanagedGenericTypeConstraint },
            { ContextualKeyword.Value, CodeWordType.Value },
            { ContextualKeyword.Var, CodeWordType.Var },
            { ContextualKeyword.WhenFilterCondition, CodeWordType.WhenFilterCondition },
            { ContextualKeyword.WhereGenericTypeConstraint, CodeWordType.WhereGenericTypeConstraint },
            { ContextualKeyword.WhereQueryClause, CodeWordType.WhereQueryClause },
            { ContextualKeyword.Yield, CodeWordType.Yield }
        };

        private readonly string _valueString;

        public CodeWord(string value)
        {
            _valueString = value ?? throw new ArgumentNullException(nameof(value));
            CodeWordType = CodeWordType.AnyString;
        }

        internal CodeWord(Keyword keyword)
        {
            if (!System.Enum.IsDefined(typeof(Keyword), keyword))
            {
                throw new InvalidEnumArgumentException(nameof(keyword), (int)keyword, typeof(Keyword));
            }

            _valueString = string.Empty;
            CodeWordType = ToCodeWordType(keyword);
        }

        internal CodeWord(ContextualKeyword contextualKeyword)
        {
            if (!System.Enum.IsDefined(typeof(ContextualKeyword), contextualKeyword))
            {
                throw new InvalidEnumArgumentException(nameof(contextualKeyword), (int)contextualKeyword, typeof(ContextualKeyword));
            }

            _valueString = string.Empty;
            CodeWordType = ToCodeWordType(contextualKeyword);
        }

        internal CodeWord(CodeWordType codeWordType)
        {
            if (!System.Enum.IsDefined(typeof(CodeWordType), codeWordType))
            {
                throw new InvalidEnumArgumentException(nameof(codeWordType), (int)codeWordType, typeof(CodeWordType));
            }

            if (codeWordType == CodeWordType.AnyString)
            {
                throw new ArgumentException("", nameof(codeWordType));
            }

            _valueString = string.Empty;
            CodeWordType = codeWordType;
        }

        public CodeWordType CodeWordType { get; }

        #region Keywords
        public static CodeWord Space => new CodeWord(" ");
        public static CodeWord Abstract => new CodeWord(CodeWordType.Abstract);
        public static CodeWord As => new CodeWord(CodeWordType.As);
        public static CodeWord Base => new CodeWord(CodeWordType.Base);
        public static CodeWord Bool => new CodeWord(CodeWordType.Bool);
        public static CodeWord Break => new CodeWord(CodeWordType.Break);
        public static CodeWord Byte => new CodeWord(CodeWordType.Byte);
        public static CodeWord Case => new CodeWord(CodeWordType.Case);
        public static CodeWord Catch => new CodeWord(CodeWordType.Catch);
        public static CodeWord Char => new CodeWord(CodeWordType.Char);
        public static CodeWord Checked => new CodeWord(CodeWordType.Checked);
        public static CodeWord Class => new CodeWord(CodeWordType.Class);
        public static CodeWord Const => new CodeWord(CodeWordType.Const);
        public static CodeWord Continue => new CodeWord(CodeWordType.Continue);
        public static CodeWord Decimal => new CodeWord(CodeWordType.Decimal);
        public static CodeWord Default => new CodeWord(CodeWordType.Default);
        public static CodeWord Delegate => new CodeWord(CodeWordType.Delegate);
        public static CodeWord Do => new CodeWord(CodeWordType.Do);
        public static CodeWord Double => new CodeWord(CodeWordType.Double);
        public static CodeWord Else => new CodeWord(CodeWordType.Else);
        public static CodeWord Enum => new CodeWord(CodeWordType.Enum);
        public static CodeWord Event => new CodeWord(CodeWordType.Event);
        public static CodeWord Explicit => new CodeWord(CodeWordType.Explicit);
        public static CodeWord Extern => new CodeWord(CodeWordType.Extern);
        public static CodeWord False => new CodeWord(CodeWordType.False);
        public static CodeWord Finally => new CodeWord(CodeWordType.Finally);
        public static CodeWord Fixed => new CodeWord(CodeWordType.Fixed);
        public static CodeWord Float => new CodeWord(CodeWordType.Float);
        public static CodeWord For => new CodeWord(CodeWordType.For);
        public static CodeWord Foreach => new CodeWord(CodeWordType.Foreach);
        public static CodeWord Goto => new CodeWord(CodeWordType.Goto);
        public static CodeWord If => new CodeWord(CodeWordType.If);
        public static CodeWord Implicit => new CodeWord(CodeWordType.Implicit);
        public static CodeWord In => new CodeWord(CodeWordType.In);
        public static CodeWord Int => new CodeWord(CodeWordType.Int);
        public static CodeWord Interface => new CodeWord(CodeWordType.Interface);
        public static CodeWord Internal => new CodeWord(CodeWordType.Internal);
        public static CodeWord Is => new CodeWord(CodeWordType.Is);
        public static CodeWord Lock => new CodeWord(CodeWordType.Lock);
        public static CodeWord Long => new CodeWord(CodeWordType.Long);
        public static CodeWord Namespace => new CodeWord(CodeWordType.Namespace);
        public static CodeWord New => new CodeWord(CodeWordType.New);
        public static CodeWord Null => new CodeWord(CodeWordType.Null);
        public static CodeWord Object => new CodeWord(CodeWordType.Object);
        public static CodeWord Operator => new CodeWord(CodeWordType.Operator);
        public static CodeWord Out => new CodeWord(CodeWordType.Out);
        public static CodeWord Override => new CodeWord(CodeWordType.Override);
        public static CodeWord Params => new CodeWord(CodeWordType.Params);
        public static CodeWord Private => new CodeWord(CodeWordType.Private);
        public static CodeWord Protected => new CodeWord(CodeWordType.Protected);
        public static CodeWord Public => new CodeWord(CodeWordType.Public);
        public static CodeWord Readonly => new CodeWord(CodeWordType.Readonly);
        public static CodeWord Ref => new CodeWord(CodeWordType.Ref);
        public static CodeWord Return => new CodeWord(CodeWordType.Return);
        public static CodeWord Sbyte => new CodeWord(CodeWordType.Sbyte);
        public static CodeWord Sealed => new CodeWord(CodeWordType.Sealed);
        public static CodeWord Short => new CodeWord(CodeWordType.Short);
        public static CodeWord Sizeof => new CodeWord(CodeWordType.Sizeof);
        public static CodeWord Stackalloc => new CodeWord(CodeWordType.Stackalloc);
        public static CodeWord Static => new CodeWord(CodeWordType.Static);
        public static CodeWord String => new CodeWord(CodeWordType.String);
        public static CodeWord Struct => new CodeWord(CodeWordType.Struct);
        public static CodeWord Switch => new CodeWord(CodeWordType.Switch);
        public static CodeWord This => new CodeWord(CodeWordType.This);
        public static CodeWord Throw => new CodeWord(CodeWordType.Throw);
        public static CodeWord True => new CodeWord(CodeWordType.True);
        public static CodeWord Try => new CodeWord(CodeWordType.Try);
        public static CodeWord Typeof => new CodeWord(CodeWordType.Typeof);
        public static CodeWord Uint => new CodeWord(CodeWordType.Uint);
        public static CodeWord Ulong => new CodeWord(CodeWordType.Ulong);
        public static CodeWord Unchecked => new CodeWord(CodeWordType.Unchecked);
        public static CodeWord Unsafe => new CodeWord(CodeWordType.Unsafe);
        public static CodeWord Ushort => new CodeWord(CodeWordType.Ushort);
        public static CodeWord Using => new CodeWord(CodeWordType.Using);
        public static CodeWord Virtual => new CodeWord(CodeWordType.Virtual);
        public static CodeWord Void => new CodeWord(CodeWordType.Void);
        public static CodeWord Volatile => new CodeWord(CodeWordType.Volatile);
        public static CodeWord While => new CodeWord(CodeWordType.While);
        #endregion

        #region ContextualKeywords
        public static CodeWord Add => new CodeWord(CodeWordType.Add);
        public static CodeWord Alias => new CodeWord(CodeWordType.Alias);
        public static CodeWord Ascending => new CodeWord(CodeWordType.Ascending);
        public static CodeWord Async => new CodeWord(CodeWordType.Async);
        public static CodeWord Await => new CodeWord(CodeWordType.Await);
        public static CodeWord By => new CodeWord(CodeWordType.By);
        public static CodeWord Descending => new CodeWord(CodeWordType.Descending);
        public static CodeWord Dynamic => new CodeWord(CodeWordType.Dynamic);
        public static CodeWord EqualsKeyword => new CodeWord(CodeWordType.Equals);
        public static CodeWord From => new CodeWord(CodeWordType.From);
        public static CodeWord Get => new CodeWord(CodeWordType.Get);
        public static CodeWord Global => new CodeWord(CodeWordType.Global);
        public static CodeWord Group => new CodeWord(CodeWordType.Group);
        public static CodeWord Into => new CodeWord(CodeWordType.Into);
        public static CodeWord Join => new CodeWord(CodeWordType.Join);
        public static CodeWord Let => new CodeWord(CodeWordType.Let);
        public static CodeWord Nameof => new CodeWord(CodeWordType.Nameof);
        public static CodeWord On => new CodeWord(CodeWordType.On);
        public static CodeWord Orderby => new CodeWord(CodeWordType.Orderby);
        public static CodeWord PartialType => new CodeWord(CodeWordType.PartialType);
        public static CodeWord PartialMethod => new CodeWord(CodeWordType.PartialMethod);
        public static CodeWord Remove => new CodeWord(CodeWordType.Remove);
        public static CodeWord Select => new CodeWord(CodeWordType.Select);
        public static CodeWord Set => new CodeWord(CodeWordType.Set);
        public static CodeWord UnmanagedGenericTypeConstraint => new CodeWord(CodeWordType.UnmanagedGenericTypeConstraint);
        public static CodeWord Value => new CodeWord(CodeWordType.Value);
        public static CodeWord Var => new CodeWord(CodeWordType.Var);
        public static CodeWord WhenFilterCondition => new CodeWord(CodeWordType.WhenFilterCondition);
        public static CodeWord WhereGenericTypeConstraint => new CodeWord(CodeWordType.WhereGenericTypeConstraint);
        public static CodeWord WhereQueryClause => new CodeWord(CodeWordType.WhereQueryClause);
        public static CodeWord Yield => new CodeWord(CodeWordType.Yield);
        #endregion

        public string GetValueString()
        {
            switch (CodeWordType)
            {
                case CodeWordType.AnyString:
                    return _valueString;
                case CodeWordType.Abstract:
                case CodeWordType.As:
                case CodeWordType.Base:
                case CodeWordType.Bool:
                case CodeWordType.Break:
                case CodeWordType.Byte:
                case CodeWordType.Case:
                case CodeWordType.Catch:
                case CodeWordType.Char:
                case CodeWordType.Checked:
                case CodeWordType.Class:
                case CodeWordType.Const:
                case CodeWordType.Continue:
                case CodeWordType.Decimal:
                case CodeWordType.Default:
                case CodeWordType.Delegate:
                case CodeWordType.Do:
                case CodeWordType.Double:
                case CodeWordType.Else:
                case CodeWordType.Enum:
                case CodeWordType.Event:
                case CodeWordType.Explicit:
                case CodeWordType.Extern:
                case CodeWordType.False:
                case CodeWordType.Finally:
                case CodeWordType.Fixed:
                case CodeWordType.Float:
                case CodeWordType.For:
                case CodeWordType.Foreach:
                case CodeWordType.Goto:
                case CodeWordType.If:
                case CodeWordType.Implicit:
                case CodeWordType.In:
                case CodeWordType.Int:
                case CodeWordType.Interface:
                case CodeWordType.Internal:
                case CodeWordType.Is:
                case CodeWordType.Lock:
                case CodeWordType.Long:
                case CodeWordType.Namespace:
                case CodeWordType.New:
                case CodeWordType.Null:
                case CodeWordType.Object:
                case CodeWordType.Operator:
                case CodeWordType.Out:
                case CodeWordType.Override:
                case CodeWordType.Params:
                case CodeWordType.Private:
                case CodeWordType.Protected:
                case CodeWordType.Public:
                case CodeWordType.Readonly:
                case CodeWordType.Ref:
                case CodeWordType.Return:
                case CodeWordType.Sbyte:
                case CodeWordType.Sealed:
                case CodeWordType.Short:
                case CodeWordType.Sizeof:
                case CodeWordType.Stackalloc:
                case CodeWordType.Static:
                case CodeWordType.String:
                case CodeWordType.Struct:
                case CodeWordType.Switch:
                case CodeWordType.This:
                case CodeWordType.Throw:
                case CodeWordType.True:
                case CodeWordType.Try:
                case CodeWordType.Typeof:
                case CodeWordType.Uint:
                case CodeWordType.Ulong:
                case CodeWordType.Unchecked:
                case CodeWordType.Unsafe:
                case CodeWordType.Ushort:
                case CodeWordType.Using:
                case CodeWordType.Virtual:
                case CodeWordType.Void:
                case CodeWordType.Volatile:
                case CodeWordType.While:
                    return KeywordHelper.GetValue(ToKeyword(CodeWordType));
                case CodeWordType.Add:
                case CodeWordType.Alias:
                case CodeWordType.Ascending:
                case CodeWordType.Async:
                case CodeWordType.Await:
                case CodeWordType.By:
                case CodeWordType.Descending:
                case CodeWordType.Dynamic:
                case CodeWordType.Equals:
                case CodeWordType.From:
                case CodeWordType.Get:
                case CodeWordType.Global:
                case CodeWordType.Group:
                case CodeWordType.Into:
                case CodeWordType.Join:
                case CodeWordType.Let:
                case CodeWordType.Nameof:
                case CodeWordType.On:
                case CodeWordType.Orderby:
                case CodeWordType.PartialType:
                case CodeWordType.PartialMethod:
                case CodeWordType.Remove:
                case CodeWordType.Select:
                case CodeWordType.Set:
                case CodeWordType.UnmanagedGenericTypeConstraint:
                case CodeWordType.Value:
                case CodeWordType.Var:
                case CodeWordType.WhenFilterCondition:
                case CodeWordType.WhereGenericTypeConstraint:
                case CodeWordType.WhereQueryClause:
                case CodeWordType.Yield:
                    return ContextualKeywordHelper.GetValue(ToContextualKeyword(CodeWordType));
                default:
                    throw new InvalidEnumArgumentException(nameof(CodeWordType), (int)CodeWordType, typeof(CodeWordType));
            }
        }

        private static CodeWordType ToCodeWordType(Keyword keyword)
        {
            return Keywords[keyword];
        }

        private static Keyword ToKeyword(CodeWordType codeWordType)
        {
            return Keywords.FirstOrDefault(k => k.Value == codeWordType).Key;
        }

        private static CodeWordType ToCodeWordType(ContextualKeyword contextualKeyword)
        {
            return ContextualKeywords[contextualKeyword];
        }

        private static ContextualKeyword ToContextualKeyword(CodeWordType codeWordType)
        {
            return ContextualKeywords.FirstOrDefault(k => k.Value == codeWordType).Key;
        }

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
            return HashCode.Combine(_valueString, CodeWordType);
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
            return (_valueString == other._valueString) && (CodeWordType == other.CodeWordType);
        }

        public override string ToString()
        {
            return GetValueString();
        }
    }
}
