using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace KoyashiroKohaku.CSharpCodeGenerator.Builders
{
    public struct CodeWord : IEquatable<CodeWord>
    {
        private readonly string _value;

        private readonly CodeWordType _codeWordType;

        public CodeWord(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("", nameof(value));
            }

            _value = value;
            _codeWordType = CodeWordType.AnyString;
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

            _value = string.Empty;
            _codeWordType = codeWordType;
        }

        /// <summary>
        /// Keywords dictionary.
        /// </summary>
        private static Dictionary<CodeWordType, Keyword> Keywords => new Dictionary<CodeWordType, Keyword>
        {
            { CodeWordType.Abstract, Keyword.Abstract },
            { CodeWordType.As, Keyword.As },
            { CodeWordType.Base, Keyword.Base },
            { CodeWordType.Bool, Keyword.Bool },
            { CodeWordType.Break, Keyword.Break },
            { CodeWordType.Byte, Keyword.Byte },
            { CodeWordType.Case, Keyword.Case },
            { CodeWordType.Catch, Keyword.Catch },
            { CodeWordType.Char, Keyword.Char },
            { CodeWordType.Checked, Keyword.Checked },
            { CodeWordType.Class, Keyword.Class },
            { CodeWordType.Const, Keyword.Const },
            { CodeWordType.Continue, Keyword.Continue },
            { CodeWordType.Decimal, Keyword.Decimal },
            { CodeWordType.Default, Keyword.Default },
            { CodeWordType.Delegate, Keyword.Delegate },
            { CodeWordType.Do, Keyword.Do },
            { CodeWordType.Double, Keyword.Double },
            { CodeWordType.Else, Keyword.Else },
            { CodeWordType.Enum, Keyword.Enum },
            { CodeWordType.Event, Keyword.Event },
            { CodeWordType.Explicit, Keyword.Explicit },
            { CodeWordType.Extern, Keyword.Extern },
            { CodeWordType.False, Keyword.False },
            { CodeWordType.Finally, Keyword.Finally },
            { CodeWordType.Fixed, Keyword.Fixed },
            { CodeWordType.Float, Keyword.Float },
            { CodeWordType.For, Keyword.For },
            { CodeWordType.Foreach, Keyword.Foreach },
            { CodeWordType.Goto, Keyword.Goto },
            { CodeWordType.If, Keyword.If },
            { CodeWordType.Implicit, Keyword.Implicit },
            { CodeWordType.In, Keyword.In },
            { CodeWordType.Int, Keyword.Int },
            { CodeWordType.Interface, Keyword.Interface },
            { CodeWordType.Internal, Keyword.Internal },
            { CodeWordType.Is, Keyword.Is },
            { CodeWordType.Lock, Keyword.Lock },
            { CodeWordType.Long, Keyword.Long },
            { CodeWordType.Namespace, Keyword.Namespace },
            { CodeWordType.New, Keyword.New },
            { CodeWordType.Null, Keyword.Null },
            { CodeWordType.Object, Keyword.Object },
            { CodeWordType.Operator, Keyword.Operator },
            { CodeWordType.Out, Keyword.Out },
            { CodeWordType.Override, Keyword.Override },
            { CodeWordType.Params, Keyword.Params },
            { CodeWordType.Private, Keyword.Private },
            { CodeWordType.Protected, Keyword.Protected },
            { CodeWordType.Public, Keyword.Public },
            { CodeWordType.Readonly, Keyword.Readonly },
            { CodeWordType.Ref, Keyword.Ref },
            { CodeWordType.Return, Keyword.Return },
            { CodeWordType.Sbyte, Keyword.Sbyte },
            { CodeWordType.Sealed, Keyword.Sealed },
            { CodeWordType.Short, Keyword.Short },
            { CodeWordType.Sizeof, Keyword.Sizeof },
            { CodeWordType.Stackalloc, Keyword.Stackalloc },
            { CodeWordType.Static, Keyword.Static },
            { CodeWordType.String, Keyword.String },
            { CodeWordType.Struct, Keyword.Struct },
            { CodeWordType.Switch, Keyword.Switch },
            { CodeWordType.This, Keyword.This },
            { CodeWordType.Throw, Keyword.Throw },
            { CodeWordType.True, Keyword.True },
            { CodeWordType.Try, Keyword.Try },
            { CodeWordType.Typeof, Keyword.Typeof },
            { CodeWordType.Uint, Keyword.Uint },
            { CodeWordType.Ulong, Keyword.Ulong },
            { CodeWordType.Unchecked, Keyword.Unchecked },
            { CodeWordType.Unsafe, Keyword.Unsafe },
            { CodeWordType.Ushort, Keyword.Ushort },
            { CodeWordType.Using, Keyword.Using },
            { CodeWordType.Virtual, Keyword.Virtual },
            { CodeWordType.Void, Keyword.Void },
            { CodeWordType.Volatile, Keyword.Volatile },
            { CodeWordType.While, Keyword.While }
        };

        /// <summary>
        /// ContextualKeywords dictionary.
        /// </summary>
        private static Dictionary<CodeWordType, ContextualKeyword> ContextualKeywords => new Dictionary<CodeWordType, ContextualKeyword>
        {
            { CodeWordType.Add, ContextualKeyword.Add },
            { CodeWordType.Alias, ContextualKeyword.Alias },
            { CodeWordType.Ascending, ContextualKeyword.Ascending },
            { CodeWordType.Async, ContextualKeyword.Async },
            { CodeWordType.Await, ContextualKeyword.Await },
            { CodeWordType.By, ContextualKeyword.By },
            { CodeWordType.Descending, ContextualKeyword.Descending },
            { CodeWordType.Dynamic, ContextualKeyword.Dynamic },
            { CodeWordType.Equals, ContextualKeyword.Equals },
            { CodeWordType.From, ContextualKeyword.From },
            { CodeWordType.Get, ContextualKeyword.Get },
            { CodeWordType.Global, ContextualKeyword.Global },
            { CodeWordType.Group, ContextualKeyword.Group },
            { CodeWordType.Into, ContextualKeyword.Into },
            { CodeWordType.Join, ContextualKeyword.Join },
            { CodeWordType.Let, ContextualKeyword.Let },
            { CodeWordType.Nameof, ContextualKeyword.Nameof },
            { CodeWordType.On, ContextualKeyword.On },
            { CodeWordType.Orderby, ContextualKeyword.Orderby },
            { CodeWordType.PartialType, ContextualKeyword.PartialType },
            { CodeWordType.PartialMethod, ContextualKeyword.PartialMethod },
            { CodeWordType.Remove, ContextualKeyword.Remove },
            { CodeWordType.Select, ContextualKeyword.Select },
            { CodeWordType.Set, ContextualKeyword.Set },
            { CodeWordType.UnmanagedGenericTypeConstraint, ContextualKeyword.UnmanagedGenericTypeConstraint },
            { CodeWordType.Value, ContextualKeyword.Value },
            { CodeWordType.Var, ContextualKeyword.Var },
            { CodeWordType.WhenFilterCondition, ContextualKeyword.WhenFilterCondition },
            { CodeWordType.WhereGenericTypeConstraint, ContextualKeyword.WhereGenericTypeConstraint },
            { CodeWordType.WhereQueryClause, ContextualKeyword.WhereQueryClause },
            { CodeWordType.Yield, ContextualKeyword.Yield }
        };

        /// <summary>
        /// ASCII chars set
        /// </summary>
        private static HashSet<CodeWordType> ASCIIChars => new HashSet<CodeWordType>
        {
            CodeWordType.Space,
            CodeWordType.ExclamationMark,
            CodeWordType.QuotationMark,
            CodeWordType.NumberSign,
            CodeWordType.DollarSign,
            CodeWordType.PercentSign,
            CodeWordType.Ampersand,
            CodeWordType.Apostrophe,
            CodeWordType.LeftParentheses,
            CodeWordType.RightParentheses,
            CodeWordType.Asterisk,
            CodeWordType.PlusSign,
            CodeWordType.Commma,
            CodeWordType.HyphenMinus,
            CodeWordType.FullStop,
            CodeWordType.Slash,
            CodeWordType.Colon,
            CodeWordType.Semicolon,
            CodeWordType.LessThanSign,
            CodeWordType.Equals,
            CodeWordType.GreaterThanSign,
            CodeWordType.QuestionMark,
            CodeWordType.AtSign,
            CodeWordType.LeftSquareBracket,
            CodeWordType.Backslash,
            CodeWordType.RightSquareBracket,
            CodeWordType.CircumflexAccent,
            CodeWordType.LowLine,
            CodeWordType.GraveAccent,
            CodeWordType.LeftCurlyBracket,
            CodeWordType.VerticalBar,
            CodeWordType.RightCurlyBracket,
            CodeWordType.Tilde,
        };

        public bool IsKeyword => Keywords.ContainsKey(_codeWordType);

        public bool IsContextualKeyword => ContextualKeywords.ContainsKey(_codeWordType);

        public bool IsASCIIChar => ASCIIChars.Contains(_codeWordType);

        public bool IsAnyString => _codeWordType == CodeWordType.AnyString;

        #region ASCII Chars
        /// <summary>
        /// &nbsp;
        /// </summary>
        public static CodeWord Space => new CodeWord(CodeWordType.Space);

        /// <summary>
        /// !
        /// </summary>
        public static CodeWord ExclamationMark => new CodeWord(CodeWordType.ExclamationMark);

        /// <summary>
        /// "
        /// </summary>
        public static CodeWord QuotationMark => new CodeWord(CodeWordType.QuotationMark);

        /// <summary>
        /// #
        /// </summary>
        public static CodeWord NumberSign => new CodeWord(CodeWordType.NumberSign);

        /// <summary>
        /// $
        /// </summary>
        public static CodeWord DollarSign => new CodeWord(CodeWordType.DollarSign);

        /// <summary>
        /// %
        /// </summary>
        public static CodeWord PercentSign => new CodeWord(CodeWordType.PercentSign);

        /// <summary>
        /// &amp;
        /// </summary>
        public static CodeWord Ampersand => new CodeWord(CodeWordType.Ampersand);

        /// <summary>
        /// '
        /// </summary>
        public static CodeWord Apostrophe => new CodeWord(CodeWordType.Apostrophe);

        /// <summary>
        /// (
        /// </summary>
        public static CodeWord LeftParentheses => new CodeWord(CodeWordType.LeftParentheses);

        /// <summary>
        /// )
        /// </summary>
        public static CodeWord RightParentheses => new CodeWord(CodeWordType.RightParentheses);

        /// <summary>
        /// *
        /// </summary>
        public static CodeWord Asterisk => new CodeWord(CodeWordType.Asterisk);

        /// <summary>
        /// +
        /// </summary>
        public static CodeWord PlusSign => new CodeWord(CodeWordType.PlusSign);

        /// <summary>
        /// ,
        /// </summary>
        public static CodeWord Commma => new CodeWord(CodeWordType.Commma);

        /// <summary>
        /// -
        /// </summary>
        public static CodeWord HyphenMinus => new CodeWord(CodeWordType.HyphenMinus);

        /// <summary>
        /// .
        /// </summary>
        public static CodeWord FullStop => new CodeWord(CodeWordType.FullStop);

        /// <summary>
        /// /
        /// </summary>
        public static CodeWord Slash => new CodeWord(CodeWordType.Slash);

        /// <summary>
        /// :
        /// </summary>
        public static CodeWord Colon => new CodeWord(CodeWordType.Colon);

        /// <summary>
        /// ;
        /// </summary>
        public static CodeWord Semicolon => new CodeWord(CodeWordType.Semicolon);

        /// <summary>
        /// &lt;
        /// </summary>
        public static CodeWord LessThanSign => new CodeWord(CodeWordType.LessThanSign);

        /// <summary>
        /// =
        /// </summary>
        public static CodeWord EqualSign => new CodeWord(CodeWordType.EqualSign);

        /// <summary>
        /// &gt;
        /// </summary>
        public static CodeWord GreaterThanSign => new CodeWord(CodeWordType.GreaterThanSign);

        /// <summary>
        /// ?
        /// </summary>
        public static CodeWord QuestionMark => new CodeWord(CodeWordType.QuestionMark);

        /// <summary>
        /// @
        /// </summary>
        public static CodeWord AtSign => new CodeWord(CodeWordType.AtSign);

        /// <summary>
        /// [
        /// </summary>
        public static CodeWord LeftSquareBracket => new CodeWord(CodeWordType.LeftSquareBracket);

        /// <summary>
        /// \
        /// </summary>
        public static CodeWord Backslash => new CodeWord(CodeWordType.Backslash);

        /// <summary>
        /// ]
        /// </summary>
        public static CodeWord RightSquareBracket => new CodeWord(CodeWordType.RightSquareBracket);

        /// <summary>
        /// ^
        /// </summary>
        public static CodeWord CircumflexAccent => new CodeWord(CodeWordType.CircumflexAccent);

        /// <summary>
        /// _
        /// </summary>
        public static CodeWord LowLine => new CodeWord(CodeWordType.LowLine);

        /// <summary>
        /// `
        /// </summary>
        public static CodeWord GraveAccent => new CodeWord(CodeWordType.GraveAccent);

        /// <summary>
        /// {
        /// </summary>
        public static CodeWord LeftCurlyBracket => new CodeWord(CodeWordType.LeftCurlyBracket);

        /// <summary>
        /// |
        /// </summary>
        public static CodeWord VerticalBar => new CodeWord(CodeWordType.VerticalBar);

        /// <summary>
        /// }
        /// </summary>
        public static CodeWord RightCurlyBracket => new CodeWord(CodeWordType.RightCurlyBracket);

        /// <summary>
        /// ~
        /// </summary>
        public static CodeWord Tilde => new CodeWord(CodeWordType.Tilde);
        #endregion

        #region Separator other than spaces
        /// <summary>
        /// End of line.
        /// </summary>
        public static CodeWord EndOfLine => new CodeWord(CodeWordType.EndOfLine);

        /// <summary>
        /// Indent.
        /// </summary>
        public static CodeWord Indent => new CodeWord(CodeWordType.Indent);
        #endregion

        #region Keywords
        /// <summary>
        /// abstract
        /// </summary>
        public static CodeWord Abstract => new CodeWord(CodeWordType.Abstract);

        /// <summary>
        /// as
        /// </summary>
        public static CodeWord As => new CodeWord(CodeWordType.As);

        /// <summary>
        /// base
        /// </summary>
        public static CodeWord Base => new CodeWord(CodeWordType.Base);

        /// <summary>
        /// bool
        /// </summary>
        public static CodeWord Bool => new CodeWord(CodeWordType.Bool);

        /// <summary>
        /// break
        /// </summary>
        public static CodeWord Break => new CodeWord(CodeWordType.Break);

        /// <summary>
        /// byte
        /// </summary>
        public static CodeWord Byte => new CodeWord(CodeWordType.Byte);

        /// <summary>
        /// case
        /// </summary>
        public static CodeWord Case => new CodeWord(CodeWordType.Case);

        /// <summary>
        /// catch
        /// </summary>
        public static CodeWord Catch => new CodeWord(CodeWordType.Catch);

        /// <summary>
        /// char
        /// </summary>
        public static CodeWord Char => new CodeWord(CodeWordType.Char);

        /// <summary>
        /// checked
        /// </summary>
        public static CodeWord Checked => new CodeWord(CodeWordType.Checked);

        /// <summary>
        /// class
        /// </summary>
        public static CodeWord Class => new CodeWord(CodeWordType.Class);

        /// <summary>
        /// const
        /// </summary>
        public static CodeWord Const => new CodeWord(CodeWordType.Const);

        /// <summary>
        /// continue
        /// </summary>
        public static CodeWord Continue => new CodeWord(CodeWordType.Continue);

        /// <summary>
        /// decimal
        /// </summary>
        public static CodeWord Decimal => new CodeWord(CodeWordType.Decimal);

        /// <summary>
        /// default
        /// </summary>
        public static CodeWord Default => new CodeWord(CodeWordType.Default);

        /// <summary>
        /// delegate
        /// </summary>
        public static CodeWord Delegate => new CodeWord(CodeWordType.Delegate);

        /// <summary>
        /// do
        /// </summary>
        public static CodeWord Do => new CodeWord(CodeWordType.Do);

        /// <summary>
        /// double
        /// </summary>
        public static CodeWord Double => new CodeWord(CodeWordType.Double);

        /// <summary>
        /// else
        /// </summary>
        public static CodeWord Else => new CodeWord(CodeWordType.Else);

        /// <summary>
        /// enum
        /// </summary>
        public static CodeWord Enum => new CodeWord(CodeWordType.Enum);

        /// <summary>
        /// event
        /// </summary>
        public static CodeWord Event => new CodeWord(CodeWordType.Event);

        /// <summary>
        /// explicit
        /// </summary>
        public static CodeWord Explicit => new CodeWord(CodeWordType.Explicit);

        /// <summary>
        /// extern
        /// </summary>
        public static CodeWord Extern => new CodeWord(CodeWordType.Extern);

        /// <summary>
        /// false
        /// </summary>
        public static CodeWord False => new CodeWord(CodeWordType.False);

        /// <summary>
        /// finally
        /// </summary>
        public static CodeWord Finally => new CodeWord(CodeWordType.Finally);

        /// <summary>
        /// fixed
        /// </summary>
        public static CodeWord Fixed => new CodeWord(CodeWordType.Fixed);

        /// <summary>
        /// float
        /// </summary>
        public static CodeWord Float => new CodeWord(CodeWordType.Float);

        /// <summary>
        /// for
        /// </summary>
        public static CodeWord For => new CodeWord(CodeWordType.For);

        /// <summary>
        /// foreach
        /// </summary>
        public static CodeWord Foreach => new CodeWord(CodeWordType.Foreach);

        /// <summary>
        /// goto
        /// </summary>
        public static CodeWord Goto => new CodeWord(CodeWordType.Goto);

        /// <summary>
        /// if
        /// </summary>
        public static CodeWord If => new CodeWord(CodeWordType.If);

        /// <summary>
        /// implicit
        /// </summary>
        public static CodeWord Implicit => new CodeWord(CodeWordType.Implicit);

        /// <summary>
        /// in
        /// </summary>
        public static CodeWord In => new CodeWord(CodeWordType.In);

        /// <summary>
        /// int
        /// </summary>
        public static CodeWord Int => new CodeWord(CodeWordType.Int);

        /// <summary>
        /// interface
        /// </summary>
        public static CodeWord Interface => new CodeWord(CodeWordType.Interface);

        /// <summary>
        /// internal
        /// </summary>
        public static CodeWord Internal => new CodeWord(CodeWordType.Internal);

        /// <summary>
        /// is
        /// </summary>
        public static CodeWord Is => new CodeWord(CodeWordType.Is);

        /// <summary>
        /// lock
        /// </summary>
        public static CodeWord Lock => new CodeWord(CodeWordType.Lock);

        /// <summary>
        /// long
        /// </summary>
        public static CodeWord Long => new CodeWord(CodeWordType.Long);

        /// <summary>
        /// namespace
        /// </summary>
        public static CodeWord Namespace => new CodeWord(CodeWordType.Namespace);

        /// <summary>
        /// new
        /// </summary>
        public static CodeWord New => new CodeWord(CodeWordType.New);

        /// <summary>
        /// null
        /// </summary>
        public static CodeWord Null => new CodeWord(CodeWordType.Null);

        /// <summary>
        /// object
        /// </summary>
        public static CodeWord Object => new CodeWord(CodeWordType.Object);

        /// <summary>
        /// operator
        /// </summary>
        public static CodeWord Operator => new CodeWord(CodeWordType.Operator);

        /// <summary>
        /// out
        /// </summary>
        public static CodeWord Out => new CodeWord(CodeWordType.Out);

        /// <summary>
        /// override
        /// </summary>
        public static CodeWord Override => new CodeWord(CodeWordType.Override);

        /// <summary>
        /// params
        /// </summary>
        public static CodeWord Params => new CodeWord(CodeWordType.Params);

        /// <summary>
        /// private
        /// </summary>
        public static CodeWord Private => new CodeWord(CodeWordType.Private);

        /// <summary>
        /// protected
        /// </summary>
        public static CodeWord Protected => new CodeWord(CodeWordType.Protected);

        /// <summary>
        /// public
        /// </summary>
        public static CodeWord Public => new CodeWord(CodeWordType.Public);

        /// <summary>
        /// readonly
        /// </summary>
        public static CodeWord Readonly => new CodeWord(CodeWordType.Readonly);

        /// <summary>
        /// ref
        /// </summary>
        public static CodeWord Ref => new CodeWord(CodeWordType.Ref);

        /// <summary>
        /// return
        /// </summary>
        public static CodeWord Return => new CodeWord(CodeWordType.Return);

        /// <summary>
        /// sbyte
        /// </summary>
        public static CodeWord Sbyte => new CodeWord(CodeWordType.Sbyte);

        /// <summary>
        /// sealed
        /// </summary>
        public static CodeWord Sealed => new CodeWord(CodeWordType.Sealed);

        /// <summary>
        /// short
        /// </summary>
        public static CodeWord Short => new CodeWord(CodeWordType.Short);

        /// <summary>
        /// sizeof
        /// </summary>
        public static CodeWord Sizeof => new CodeWord(CodeWordType.Sizeof);

        /// <summary>
        /// stackalloc
        /// </summary>
        public static CodeWord Stackalloc => new CodeWord(CodeWordType.Stackalloc);

        /// <summary>
        /// static
        /// </summary>
        public static CodeWord Static => new CodeWord(CodeWordType.Static);

        /// <summary>
        /// string
        /// </summary>
        public static CodeWord String => new CodeWord(CodeWordType.String);

        /// <summary>
        /// struct
        /// </summary>
        public static CodeWord Struct => new CodeWord(CodeWordType.Struct);

        /// <summary>
        /// switch
        /// </summary>
        public static CodeWord Switch => new CodeWord(CodeWordType.Switch);

        /// <summary>
        /// this
        /// </summary>
        public static CodeWord This => new CodeWord(CodeWordType.This);

        /// <summary>
        /// throw
        /// </summary>
        public static CodeWord Throw => new CodeWord(CodeWordType.Throw);

        /// <summary>
        /// true
        /// </summary>
        public static CodeWord True => new CodeWord(CodeWordType.True);

        /// <summary>
        /// try
        /// </summary>
        public static CodeWord Try => new CodeWord(CodeWordType.Try);

        /// <summary>
        /// typeof
        /// </summary>
        public static CodeWord Typeof => new CodeWord(CodeWordType.Typeof);

        /// <summary>
        /// uint
        /// </summary>
        public static CodeWord Uint => new CodeWord(CodeWordType.Uint);

        /// <summary>
        /// ulong
        /// </summary>
        public static CodeWord Ulong => new CodeWord(CodeWordType.Ulong);

        /// <summary>
        /// unchecked
        /// </summary>
        public static CodeWord Unchecked => new CodeWord(CodeWordType.Unchecked);

        /// <summary>
        /// unsafe
        /// </summary>
        public static CodeWord Unsafe => new CodeWord(CodeWordType.Unsafe);

        /// <summary>
        /// ushort
        /// </summary>
        public static CodeWord Ushort => new CodeWord(CodeWordType.Ushort);

        /// <summary>
        /// using
        /// </summary>
        public static CodeWord Using => new CodeWord(CodeWordType.Using);

        /// <summary>
        /// virtual
        /// </summary>
        public static CodeWord Virtual => new CodeWord(CodeWordType.Virtual);

        /// <summary>
        /// void
        /// </summary>
        public static CodeWord Void => new CodeWord(CodeWordType.Void);

        /// <summary>
        /// volatile
        /// </summary>
        public static CodeWord Volatile => new CodeWord(CodeWordType.Volatile);

        /// <summary>
        /// while
        /// </summary>
        public static CodeWord While => new CodeWord(CodeWordType.While);
        #endregion

        #region ContextualKeywords
        /// <summary>
        /// add
        /// </summary>
        public static CodeWord Add => new CodeWord(CodeWordType.Add);

        /// <summary>
        /// alias
        /// </summary>
        public static CodeWord Alias => new CodeWord(CodeWordType.Alias);

        /// <summary>
        /// ascending
        /// </summary>
        public static CodeWord Ascending => new CodeWord(CodeWordType.Ascending);

        /// <summary>
        /// async
        /// </summary>
        public static CodeWord Async => new CodeWord(CodeWordType.Async);

        /// <summary>
        /// await
        /// </summary>
        public static CodeWord Await => new CodeWord(CodeWordType.Await);

        /// <summary>
        /// by
        /// </summary>
        public static CodeWord By => new CodeWord(CodeWordType.By);

        /// <summary>
        /// descending
        /// </summary>
        public static CodeWord Descending => new CodeWord(CodeWordType.Descending);

        /// <summary>
        /// dynamic
        /// </summary>
        public static CodeWord Dynamic => new CodeWord(CodeWordType.Dynamic);

        /// <summary>
        /// equals
        /// </summary>
        public static CodeWord EqualsKeyword => new CodeWord(CodeWordType.Equals);

        /// <summary>
        /// from
        /// </summary>
        public static CodeWord From => new CodeWord(CodeWordType.From);

        /// <summary>
        /// get
        /// </summary>
        public static CodeWord Get => new CodeWord(CodeWordType.Get);

        /// <summary>
        /// global
        /// </summary>
        public static CodeWord Global => new CodeWord(CodeWordType.Global);

        /// <summary>
        /// group
        /// </summary>
        public static CodeWord Group => new CodeWord(CodeWordType.Group);

        /// <summary>
        /// into
        /// </summary>
        public static CodeWord Into => new CodeWord(CodeWordType.Into);

        /// <summary>
        /// join
        /// </summary>
        public static CodeWord Join => new CodeWord(CodeWordType.Join);

        /// <summary>
        /// let
        /// </summary>
        public static CodeWord Let => new CodeWord(CodeWordType.Let);

        /// <summary>
        /// nameof
        /// </summary>
        public static CodeWord Nameof => new CodeWord(CodeWordType.Nameof);

        /// <summary>
        /// on
        /// </summary>
        public static CodeWord On => new CodeWord(CodeWordType.On);

        /// <summary>
        /// orderby
        /// </summary>
        public static CodeWord Orderby => new CodeWord(CodeWordType.Orderby);

        /// <summary>
        /// partial (type)
        /// </summary>
        public static CodeWord PartialType => new CodeWord(CodeWordType.PartialType);

        /// <summary>
        /// partial (method)
        /// </summary>
        public static CodeWord PartialMethod => new CodeWord(CodeWordType.PartialMethod);

        /// <summary>
        /// remove
        /// </summary>
        public static CodeWord Remove => new CodeWord(CodeWordType.Remove);

        /// <summary>
        /// select
        /// </summary>
        public static CodeWord Select => new CodeWord(CodeWordType.Select);

        /// <summary>
        /// set
        /// </summary>
        public static CodeWord Set => new CodeWord(CodeWordType.Set);

        /// <summary>
        /// unmanaged (generic type constraint)
        /// </summary>
        public static CodeWord UnmanagedGenericTypeConstraint => new CodeWord(CodeWordType.UnmanagedGenericTypeConstraint);

        /// <summary>
        /// value
        /// </summary>
        public static CodeWord Value => new CodeWord(CodeWordType.Value);

        /// <summary>
        /// var
        /// </summary>
        public static CodeWord Var => new CodeWord(CodeWordType.Var);

        /// <summary>
        /// when (filter condition)
        /// </summary>
        public static CodeWord WhenFilterCondition => new CodeWord(CodeWordType.WhenFilterCondition);

        /// <summary>
        /// when where (generic type constraint)
        /// </summary>
        public static CodeWord WhereGenericTypeConstraint => new CodeWord(CodeWordType.WhereGenericTypeConstraint);

        /// <summary>
        /// where (query clause)
        /// </summary>
        public static CodeWord WhereQueryClause => new CodeWord(CodeWordType.WhereQueryClause);

        /// <summary>
        /// yield
        /// </summary>
        public static CodeWord Yield => new CodeWord(CodeWordType.Yield);
        #endregion

        public bool TryGetAnyString([MaybeNullWhen(false)] out string value)
        {
            if (IsAnyString)
            {
                value = _value;
                return true;
            }
            else
            {
                value = default;
                return false;
            }
        }

        public string GetAnyString()
        {
            if (!IsAnyString)
            {
                throw new ArgumentException();
            }

            return _value;
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
            return HashCode.Combine(_value, _codeWordType);
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
            return (_value == other._value) && (_codeWordType == other._codeWordType);
        }

        internal enum CodeWordType
        {
            #region ASCIIChars
            /// <summary>
            /// &nbsp;
            /// </summary>
            Space,

            /// <summary>
            /// !
            /// </summary>
            ExclamationMark,

            /// <summary>
            /// "
            /// </summary>
            QuotationMark,

            /// <summary>
            /// #
            /// </summary>
            NumberSign,

            /// <summary>
            /// $
            /// </summary>
            DollarSign,

            /// <summary>
            /// %
            /// </summary>
            PercentSign,

            /// <summary>
            /// &amp;
            /// </summary>
            Ampersand,

            /// <summary>
            /// '
            /// </summary>
            Apostrophe,

            /// <summary>
            /// (
            /// </summary>
            LeftParentheses,

            /// <summary>
            /// )
            /// </summary>
            RightParentheses,

            /// <summary>
            /// *
            /// </summary>
            Asterisk,

            /// <summary>
            /// +
            /// </summary>
            PlusSign,

            /// <summary>
            /// ,
            /// </summary>
            Commma,

            /// <summary>
            /// -
            /// </summary>
            HyphenMinus,

            /// <summary>
            /// .
            /// </summary>
            FullStop,

            /// <summary>
            /// /
            /// </summary>
            Slash,

            /// <summary>
            /// :
            /// </summary>
            Colon,

            /// <summary>
            /// ;
            /// </summary>
            Semicolon,

            /// <summary>
            /// &lt;
            /// </summary>
            LessThanSign,

            /// <summary>
            /// =
            /// </summary>
            EqualSign,

            /// <summary>
            /// &gt;
            /// </summary>
            GreaterThanSign,

            /// <summary>
            /// ?
            /// </summary>
            QuestionMark,

            /// <summary>
            /// @
            /// </summary>
            AtSign,

            /// <summary>
            /// [
            /// </summary>
            LeftSquareBracket,

            /// <summary>
            /// \
            /// </summary>
            Backslash,

            /// <summary>
            /// ]
            /// </summary>
            RightSquareBracket,

            /// <summary>
            /// ^
            /// </summary>
            CircumflexAccent,

            /// <summary>
            /// _
            /// </summary>
            LowLine,

            /// <summary>
            /// `
            /// </summary>
            GraveAccent,

            /// <summary>
            /// {
            /// </summary>
            LeftCurlyBracket,

            /// <summary>
            /// |
            /// </summary>
            VerticalBar,

            /// <summary>
            /// }
            /// </summary>
            RightCurlyBracket,

            /// <summary>
            /// ~
            /// </summary>
            Tilde,
            #endregion

            #region Separator other than spaces
            /// <summary>
            /// End of line
            /// </summary>
            EndOfLine,

            /// <summary>
            /// Indent.
            /// </summary>
            Indent,
            #endregion

            #region Keywords
            /// <summary>
            /// abstract
            /// </summary>
            Abstract,

            /// <summary>
            /// as
            /// </summary>
            As,

            /// <summary>
            /// base
            /// </summary>
            Base,

            /// <summary>
            /// bool
            /// </summary>
            Bool,

            /// <summary>
            /// break
            /// </summary>
            Break,

            /// <summary>
            /// byte
            /// </summary>
            Byte,

            /// <summary>
            /// case
            /// </summary>
            Case,

            /// <summary>
            /// catch
            /// </summary>
            Catch,

            /// <summary>
            /// char
            /// </summary>
            Char,

            /// <summary>
            /// checked
            /// </summary>
            Checked,

            /// <summary>
            /// class
            /// </summary>
            Class,

            /// <summary>
            /// const
            /// </summary>
            Const,

            /// <summary>
            /// continue
            /// </summary>
            Continue,

            /// <summary>
            /// decimal
            /// </summary>
            Decimal,

            /// <summary>
            /// default
            /// </summary>
            Default,

            /// <summary>
            /// delegate
            /// </summary>
            Delegate,

            /// <summary>
            /// do
            /// </summary>
            Do,

            /// <summary>
            /// double
            /// </summary>
            Double,

            /// <summary>
            /// else
            /// </summary>
            Else,

            /// <summary>
            /// enum
            /// </summary>
            Enum,

            /// <summary>
            /// event
            /// </summary>
            Event,

            /// <summary>
            /// explicit
            /// </summary>
            Explicit,

            /// <summary>
            /// extern
            /// </summary>
            Extern,

            /// <summary>
            /// false
            /// </summary>
            False,

            /// <summary>
            /// finally
            /// </summary>
            Finally,

            /// <summary>
            /// fixed
            /// </summary>
            Fixed,

            /// <summary>
            /// float
            /// </summary>
            Float,

            /// <summary>
            /// for
            /// </summary>
            For,

            /// <summary>
            /// foreach
            /// </summary>
            Foreach,

            /// <summary>
            /// goto
            /// </summary>
            Goto,

            /// <summary>
            /// if
            /// </summary>
            If,

            /// <summary>
            /// implicit
            /// </summary>
            Implicit,

            /// <summary>
            /// in
            /// </summary>
            In,

            /// <summary>
            /// int
            /// </summary>
            Int,

            /// <summary>
            /// interface
            /// </summary>
            Interface,

            /// <summary>
            /// internal
            /// </summary>
            Internal,

            /// <summary>
            /// is
            /// </summary>
            Is,

            /// <summary>
            /// lock
            /// </summary>
            Lock,

            /// <summary>
            /// long
            /// </summary>
            Long,

            /// <summary>
            /// namespace
            /// </summary>
            Namespace,

            /// <summary>
            /// new
            /// </summary>
            New,

            /// <summary>
            /// null
            /// </summary>
            Null,

            /// <summary>
            /// object
            /// </summary>
            Object,

            /// <summary>
            /// operator
            /// </summary>
            Operator,

            /// <summary>
            /// out
            /// </summary>
            Out,

            /// <summary>
            /// override
            /// </summary>
            Override,

            /// <summary>
            /// params
            /// </summary>
            Params,

            /// <summary>
            /// private
            /// </summary>
            Private,

            /// <summary>
            /// protected
            /// </summary>
            Protected,

            /// <summary>
            /// public
            /// </summary>
            Public,

            /// <summary>
            /// readonly
            /// </summary>
            Readonly,

            /// <summary>
            /// ref
            /// </summary>
            Ref,

            /// <summary>
            /// return
            /// </summary>
            Return,

            /// <summary>
            /// sbyte
            /// </summary>
            Sbyte,

            /// <summary>
            /// sealed
            /// </summary>
            Sealed,

            /// <summary>
            /// short
            /// </summary>
            Short,

            /// <summary>
            /// sizeof
            /// </summary>
            Sizeof,

            /// <summary>
            /// stackalloc
            /// </summary>
            Stackalloc,

            /// <summary>
            /// static
            /// </summary>
            Static,

            /// <summary>
            /// string
            /// </summary>
            String,

            /// <summary>
            /// struct
            /// </summary>
            Struct,

            /// <summary>
            /// switch
            /// </summary>
            Switch,

            /// <summary>
            /// this
            /// </summary>
            This,

            /// <summary>
            /// throw
            /// </summary>
            Throw,

            /// <summary>
            /// true
            /// </summary>
            True,

            /// <summary>
            /// try
            /// </summary>
            Try,

            /// <summary>
            /// typeof
            /// </summary>
            Typeof,

            /// <summary>
            /// uint
            /// </summary>
            Uint,

            /// <summary>
            /// ulong
            /// </summary>
            Ulong,

            /// <summary>
            /// unchecked
            /// </summary>
            Unchecked,

            /// <summary>
            /// unsafe
            /// </summary>
            Unsafe,

            /// <summary>
            /// ushort
            /// </summary>
            Ushort,

            /// <summary>
            /// using
            /// </summary>
            Using,

            /// <summary>
            /// virtual
            /// </summary>
            Virtual,

            /// <summary>
            /// void
            /// </summary>
            Void,

            /// <summary>
            /// volatile
            /// </summary>
            Volatile,

            /// <summary>
            /// while
            /// </summary>
            While,
            #endregion

            #region ContextualKeyword
            /// <summary>
            /// add
            /// </summary>
            Add,

            /// <summary>
            /// alias
            /// </summary>
            Alias,

            /// <summary>
            /// ascending
            /// </summary>
            Ascending,

            /// <summary>
            /// async
            /// </summary>
            Async,

            /// <summary>
            /// await
            /// </summary>
            Await,

            /// <summary>
            /// by
            /// </summary>
            By,

            /// <summary>
            /// descending
            /// </summary>
            Descending,

            /// <summary>
            /// dynamic
            /// </summary>
            Dynamic,

            /// <summary>
            /// equals
            /// </summary>
            Equals,

            /// <summary>
            /// from
            /// </summary>
            From,

            /// <summary>
            /// get
            /// </summary>
            Get,

            /// <summary>
            /// global
            /// </summary>
            Global,

            /// <summary>
            /// group
            /// </summary>
            Group,

            /// <summary>
            /// into
            /// </summary>
            Into,

            /// <summary>
            /// join
            /// </summary>
            Join,

            /// <summary>
            /// let
            /// </summary>
            Let,

            /// <summary>
            /// nameof
            /// </summary>
            Nameof,

            /// <summary>
            /// on
            /// </summary>
            On,

            /// <summary>
            /// orderby
            /// </summary>
            Orderby,

            /// <summary>
            /// partial (type)
            /// </summary>
            PartialType,

            /// <summary>
            /// partial (method)
            /// </summary>
            PartialMethod,

            /// <summary>
            /// remove
            /// </summary>
            Remove,

            /// <summary>
            /// select
            /// </summary>
            Select,

            /// <summary>
            /// set
            /// </summary>
            Set,

            /// <summary>
            /// unmanaged (generic type constraint)
            /// </summary>
            UnmanagedGenericTypeConstraint,

            /// <summary>
            /// value
            /// </summary>
            Value,

            /// <summary>
            /// var
            /// </summary>
            Var,

            /// <summary>
            /// when (filter condition)
            /// </summary>
            WhenFilterCondition,

            /// <summary>
            /// when where (generic type constraint)
            /// </summary>
            WhereGenericTypeConstraint,

            /// <summary>
            /// where (query clause)
            /// </summary>
            WhereQueryClause,

            /// <summary>
            /// yield
            /// </summary>
            Yield,
            #endregion

            /// <summary>
            /// any string.
            /// </summary>
            AnyString
        }
    }
}
