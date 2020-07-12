using KoyashiroKohaku.CSharpCodeGenerator.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace KoyashiroKohaku.CSharpCodeGenerator.Builders
{
    public struct Token : IEquatable<Token>
    {
        private readonly string _value;

        private readonly TokendType _tokenType;

        public Token(string value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                // TODO: exception message
                throw new ArgumentException(string.Empty, nameof(value));
            }

            _value = value;
            _tokenType = TokendType.AnyString;
        }

        internal Token(TokendType tokenType)
        {
            if (!System.Enum.IsDefined(typeof(TokendType), tokenType))
            {
                throw new InvalidEnumArgumentException(nameof(tokenType), (int)tokenType, typeof(TokendType));
            }

            if (tokenType == TokendType.AnyString)
            {
                // TODO: exception message
                throw new ArgumentException(string.Empty, nameof(tokenType));
            }

            _value = string.Empty;
            _tokenType = tokenType;
        }

        /// <summary>
        /// ASCII chars set
        /// </summary>
        private static Dictionary<TokendType, ASCIIChar> ASCIIChars => new Dictionary<TokendType, ASCIIChar>
        {
            { TokendType.Space, ASCIIChar.Space },
            { TokendType.ExclamationMark, ASCIIChar.ExclamationMark },
            { TokendType.QuotationMark, ASCIIChar.QuotationMark },
            { TokendType.NumberSign, ASCIIChar.NumberSign },
            { TokendType.DollarSign, ASCIIChar.DollarSign },
            { TokendType.PercentSign, ASCIIChar.PercentSign },
            { TokendType.Ampersand, ASCIIChar.Ampersand },
            { TokendType.Apostrophe, ASCIIChar.Apostrophe },
            { TokendType.LeftParentheses, ASCIIChar.LeftParentheses },
            { TokendType.RightParentheses, ASCIIChar.RightParentheses },
            { TokendType.Asterisk, ASCIIChar.Asterisk },
            { TokendType.PlusSign, ASCIIChar.PlusSign },
            { TokendType.Commma, ASCIIChar.Commma },
            { TokendType.HyphenMinus, ASCIIChar.HyphenMinus },
            { TokendType.FullStop, ASCIIChar.FullStop },
            { TokendType.Slash, ASCIIChar.Slash },
            { TokendType.Colon, ASCIIChar.Colon },
            { TokendType.Semicolon, ASCIIChar.Semicolon },
            { TokendType.LessThanSign, ASCIIChar.LessThanSign },
            { TokendType.Equals, ASCIIChar.Equals },
            { TokendType.GreaterThanSign, ASCIIChar.GreaterThanSign },
            { TokendType.QuestionMark, ASCIIChar.QuestionMark },
            { TokendType.AtSign, ASCIIChar.AtSign },
            { TokendType.LeftSquareBracket, ASCIIChar.LeftSquareBracket },
            { TokendType.Backslash, ASCIIChar.Backslash },
            { TokendType.RightSquareBracket, ASCIIChar.RightSquareBracket },
            { TokendType.CircumflexAccent, ASCIIChar.CircumflexAccent },
            { TokendType.LowLine, ASCIIChar.LowLine },
            { TokendType.GraveAccent, ASCIIChar.GraveAccent },
            { TokendType.LeftCurlyBracket, ASCIIChar.LeftCurlyBracket },
            { TokendType.VerticalBar, ASCIIChar.VerticalBar },
            { TokendType.RightCurlyBracket, ASCIIChar.RightCurlyBracket },
            { TokendType.Tilde, ASCIIChar.Tilde }
        };

        /// <summary>
        /// Keywords dictionary.
        /// </summary>
        private static Dictionary<TokendType, Keyword> Keywords => new Dictionary<TokendType, Keyword>
        {
            { TokendType.Abstract, Keyword.Abstract },
            { TokendType.As, Keyword.As },
            { TokendType.Base, Keyword.Base },
            { TokendType.Bool, Keyword.Bool },
            { TokendType.Break, Keyword.Break },
            { TokendType.Byte, Keyword.Byte },
            { TokendType.Case, Keyword.Case },
            { TokendType.Catch, Keyword.Catch },
            { TokendType.Char, Keyword.Char },
            { TokendType.Checked, Keyword.Checked },
            { TokendType.Class, Keyword.Class },
            { TokendType.Const, Keyword.Const },
            { TokendType.Continue, Keyword.Continue },
            { TokendType.Decimal, Keyword.Decimal },
            { TokendType.Default, Keyword.Default },
            { TokendType.Delegate, Keyword.Delegate },
            { TokendType.Do, Keyword.Do },
            { TokendType.Double, Keyword.Double },
            { TokendType.Else, Keyword.Else },
            { TokendType.Enum, Keyword.Enum },
            { TokendType.Event, Keyword.Event },
            { TokendType.Explicit, Keyword.Explicit },
            { TokendType.Extern, Keyword.Extern },
            { TokendType.False, Keyword.False },
            { TokendType.Finally, Keyword.Finally },
            { TokendType.Fixed, Keyword.Fixed },
            { TokendType.Float, Keyword.Float },
            { TokendType.For, Keyword.For },
            { TokendType.Foreach, Keyword.Foreach },
            { TokendType.Goto, Keyword.Goto },
            { TokendType.If, Keyword.If },
            { TokendType.Implicit, Keyword.Implicit },
            { TokendType.In, Keyword.In },
            { TokendType.Int, Keyword.Int },
            { TokendType.Interface, Keyword.Interface },
            { TokendType.Internal, Keyword.Internal },
            { TokendType.Is, Keyword.Is },
            { TokendType.Lock, Keyword.Lock },
            { TokendType.Long, Keyword.Long },
            { TokendType.Namespace, Keyword.Namespace },
            { TokendType.New, Keyword.New },
            { TokendType.Null, Keyword.Null },
            { TokendType.Object, Keyword.Object },
            { TokendType.Operator, Keyword.Operator },
            { TokendType.Out, Keyword.Out },
            { TokendType.Override, Keyword.Override },
            { TokendType.Params, Keyword.Params },
            { TokendType.Private, Keyword.Private },
            { TokendType.Protected, Keyword.Protected },
            { TokendType.Public, Keyword.Public },
            { TokendType.Readonly, Keyword.Readonly },
            { TokendType.Ref, Keyword.Ref },
            { TokendType.Return, Keyword.Return },
            { TokendType.Sbyte, Keyword.Sbyte },
            { TokendType.Sealed, Keyword.Sealed },
            { TokendType.Short, Keyword.Short },
            { TokendType.Sizeof, Keyword.Sizeof },
            { TokendType.Stackalloc, Keyword.Stackalloc },
            { TokendType.Static, Keyword.Static },
            { TokendType.String, Keyword.String },
            { TokendType.Struct, Keyword.Struct },
            { TokendType.Switch, Keyword.Switch },
            { TokendType.This, Keyword.This },
            { TokendType.Throw, Keyword.Throw },
            { TokendType.True, Keyword.True },
            { TokendType.Try, Keyword.Try },
            { TokendType.Typeof, Keyword.Typeof },
            { TokendType.Uint, Keyword.Uint },
            { TokendType.Ulong, Keyword.Ulong },
            { TokendType.Unchecked, Keyword.Unchecked },
            { TokendType.Unsafe, Keyword.Unsafe },
            { TokendType.Ushort, Keyword.Ushort },
            { TokendType.Using, Keyword.Using },
            { TokendType.Virtual, Keyword.Virtual },
            { TokendType.Void, Keyword.Void },
            { TokendType.Volatile, Keyword.Volatile },
            { TokendType.While, Keyword.While }
        };

        /// <summary>
        /// ContextualKeywords dictionary.
        /// </summary>
        private static Dictionary<TokendType, ContextualKeyword> ContextualKeywords => new Dictionary<TokendType, ContextualKeyword>
        {
            { TokendType.Add, ContextualKeyword.Add },
            { TokendType.Alias, ContextualKeyword.Alias },
            { TokendType.Ascending, ContextualKeyword.Ascending },
            { TokendType.Async, ContextualKeyword.Async },
            { TokendType.Await, ContextualKeyword.Await },
            { TokendType.By, ContextualKeyword.By },
            { TokendType.Descending, ContextualKeyword.Descending },
            { TokendType.Dynamic, ContextualKeyword.Dynamic },
            { TokendType.Equals, ContextualKeyword.Equals },
            { TokendType.From, ContextualKeyword.From },
            { TokendType.Get, ContextualKeyword.Get },
            { TokendType.Global, ContextualKeyword.Global },
            { TokendType.Group, ContextualKeyword.Group },
            { TokendType.Into, ContextualKeyword.Into },
            { TokendType.Join, ContextualKeyword.Join },
            { TokendType.Let, ContextualKeyword.Let },
            { TokendType.Nameof, ContextualKeyword.Nameof },
            { TokendType.On, ContextualKeyword.On },
            { TokendType.Orderby, ContextualKeyword.Orderby },
            { TokendType.PartialType, ContextualKeyword.PartialType },
            { TokendType.PartialMethod, ContextualKeyword.PartialMethod },
            { TokendType.Remove, ContextualKeyword.Remove },
            { TokendType.Select, ContextualKeyword.Select },
            { TokendType.Set, ContextualKeyword.Set },
            { TokendType.UnmanagedGenericTypeConstraint, ContextualKeyword.UnmanagedGenericTypeConstraint },
            { TokendType.Value, ContextualKeyword.Value },
            { TokendType.Var, ContextualKeyword.Var },
            { TokendType.WhenFilterCondition, ContextualKeyword.WhenFilterCondition },
            { TokendType.WhereGenericTypeConstraint, ContextualKeyword.WhereGenericTypeConstraint },
            { TokendType.WhereQueryClause, ContextualKeyword.WhereQueryClause },
            { TokendType.Yield, ContextualKeyword.Yield }
        };

        public bool IsKeyword => Keywords.ContainsKey(_tokenType);

        public bool IsContextualKeyword => ContextualKeywords.ContainsKey(_tokenType);

        public bool IsASCIIChar => ASCIIChars.ContainsKey(_tokenType);

        public bool IsAnyString => _tokenType == TokendType.AnyString;

        #region ASCII Chars
        /// <summary>
        /// &nbsp;
        /// </summary>
        public static Token Space => new Token(TokendType.Space);

        /// <summary>
        /// !
        /// </summary>
        public static Token ExclamationMark => new Token(TokendType.ExclamationMark);

        /// <summary>
        /// "
        /// </summary>
        public static Token QuotationMark => new Token(TokendType.QuotationMark);

        /// <summary>
        /// #
        /// </summary>
        public static Token NumberSign => new Token(TokendType.NumberSign);

        /// <summary>
        /// $
        /// </summary>
        public static Token DollarSign => new Token(TokendType.DollarSign);

        /// <summary>
        /// %
        /// </summary>
        public static Token PercentSign => new Token(TokendType.PercentSign);

        /// <summary>
        /// &amp;
        /// </summary>
        public static Token Ampersand => new Token(TokendType.Ampersand);

        /// <summary>
        /// '
        /// </summary>
        public static Token Apostrophe => new Token(TokendType.Apostrophe);

        /// <summary>
        /// (
        /// </summary>
        public static Token LeftParentheses => new Token(TokendType.LeftParentheses);

        /// <summary>
        /// )
        /// </summary>
        public static Token RightParentheses => new Token(TokendType.RightParentheses);

        /// <summary>
        /// *
        /// </summary>
        public static Token Asterisk => new Token(TokendType.Asterisk);

        /// <summary>
        /// +
        /// </summary>
        public static Token PlusSign => new Token(TokendType.PlusSign);

        /// <summary>
        /// ,
        /// </summary>
        public static Token Commma => new Token(TokendType.Commma);

        /// <summary>
        /// -
        /// </summary>
        public static Token HyphenMinus => new Token(TokendType.HyphenMinus);

        /// <summary>
        /// .
        /// </summary>
        public static Token FullStop => new Token(TokendType.FullStop);

        /// <summary>
        /// /
        /// </summary>
        public static Token Slash => new Token(TokendType.Slash);

        /// <summary>
        /// :
        /// </summary>
        public static Token Colon => new Token(TokendType.Colon);

        /// <summary>
        /// ;
        /// </summary>
        public static Token Semicolon => new Token(TokendType.Semicolon);

        /// <summary>
        /// &lt;
        /// </summary>
        public static Token LessThanSign => new Token(TokendType.LessThanSign);

        /// <summary>
        /// =
        /// </summary>
        public static Token EqualSign => new Token(TokendType.EqualSign);

        /// <summary>
        /// &gt;
        /// </summary>
        public static Token GreaterThanSign => new Token(TokendType.GreaterThanSign);

        /// <summary>
        /// ?
        /// </summary>
        public static Token QuestionMark => new Token(TokendType.QuestionMark);

        /// <summary>
        /// @
        /// </summary>
        public static Token AtSign => new Token(TokendType.AtSign);

        /// <summary>
        /// [
        /// </summary>
        public static Token LeftSquareBracket => new Token(TokendType.LeftSquareBracket);

        /// <summary>
        /// \
        /// </summary>
        public static Token Backslash => new Token(TokendType.Backslash);

        /// <summary>
        /// ]
        /// </summary>
        public static Token RightSquareBracket => new Token(TokendType.RightSquareBracket);

        /// <summary>
        /// ^
        /// </summary>
        public static Token CircumflexAccent => new Token(TokendType.CircumflexAccent);

        /// <summary>
        /// _
        /// </summary>
        public static Token LowLine => new Token(TokendType.LowLine);

        /// <summary>
        /// `
        /// </summary>
        public static Token GraveAccent => new Token(TokendType.GraveAccent);

        /// <summary>
        /// {
        /// </summary>
        public static Token LeftCurlyBracket => new Token(TokendType.LeftCurlyBracket);

        /// <summary>
        /// |
        /// </summary>
        public static Token VerticalBar => new Token(TokendType.VerticalBar);

        /// <summary>
        /// }
        /// </summary>
        public static Token RightCurlyBracket => new Token(TokendType.RightCurlyBracket);

        /// <summary>
        /// ~
        /// </summary>
        public static Token Tilde => new Token(TokendType.Tilde);
        #endregion

        #region Separator other than spaces
        /// <summary>
        /// End of line.
        /// </summary>
        public static Token EndOfLine => new Token(TokendType.EndOfLine);

        /// <summary>
        /// Indent.
        /// </summary>
        public static Token Indent => new Token(TokendType.Indent);
        #endregion

        #region Keywords
        /// <summary>
        /// abstract
        /// </summary>
        public static Token Abstract => new Token(TokendType.Abstract);

        /// <summary>
        /// as
        /// </summary>
        public static Token As => new Token(TokendType.As);

        /// <summary>
        /// base
        /// </summary>
        public static Token Base => new Token(TokendType.Base);

        /// <summary>
        /// bool
        /// </summary>
        public static Token Bool => new Token(TokendType.Bool);

        /// <summary>
        /// break
        /// </summary>
        public static Token Break => new Token(TokendType.Break);

        /// <summary>
        /// byte
        /// </summary>
        public static Token Byte => new Token(TokendType.Byte);

        /// <summary>
        /// case
        /// </summary>
        public static Token Case => new Token(TokendType.Case);

        /// <summary>
        /// catch
        /// </summary>
        public static Token Catch => new Token(TokendType.Catch);

        /// <summary>
        /// char
        /// </summary>
        public static Token Char => new Token(TokendType.Char);

        /// <summary>
        /// checked
        /// </summary>
        public static Token Checked => new Token(TokendType.Checked);

        /// <summary>
        /// class
        /// </summary>
        public static Token Class => new Token(TokendType.Class);

        /// <summary>
        /// const
        /// </summary>
        public static Token Const => new Token(TokendType.Const);

        /// <summary>
        /// continue
        /// </summary>
        public static Token Continue => new Token(TokendType.Continue);

        /// <summary>
        /// decimal
        /// </summary>
        public static Token Decimal => new Token(TokendType.Decimal);

        /// <summary>
        /// default
        /// </summary>
        public static Token Default => new Token(TokendType.Default);

        /// <summary>
        /// delegate
        /// </summary>
        public static Token Delegate => new Token(TokendType.Delegate);

        /// <summary>
        /// do
        /// </summary>
        public static Token Do => new Token(TokendType.Do);

        /// <summary>
        /// double
        /// </summary>
        public static Token Double => new Token(TokendType.Double);

        /// <summary>
        /// else
        /// </summary>
        public static Token Else => new Token(TokendType.Else);

        /// <summary>
        /// enum
        /// </summary>
        public static Token Enum => new Token(TokendType.Enum);

        /// <summary>
        /// event
        /// </summary>
        public static Token Event => new Token(TokendType.Event);

        /// <summary>
        /// explicit
        /// </summary>
        public static Token Explicit => new Token(TokendType.Explicit);

        /// <summary>
        /// extern
        /// </summary>
        public static Token Extern => new Token(TokendType.Extern);

        /// <summary>
        /// false
        /// </summary>
        public static Token False => new Token(TokendType.False);

        /// <summary>
        /// finally
        /// </summary>
        public static Token Finally => new Token(TokendType.Finally);

        /// <summary>
        /// fixed
        /// </summary>
        public static Token Fixed => new Token(TokendType.Fixed);

        /// <summary>
        /// float
        /// </summary>
        public static Token Float => new Token(TokendType.Float);

        /// <summary>
        /// for
        /// </summary>
        public static Token For => new Token(TokendType.For);

        /// <summary>
        /// foreach
        /// </summary>
        public static Token Foreach => new Token(TokendType.Foreach);

        /// <summary>
        /// goto
        /// </summary>
        public static Token Goto => new Token(TokendType.Goto);

        /// <summary>
        /// if
        /// </summary>
        public static Token If => new Token(TokendType.If);

        /// <summary>
        /// implicit
        /// </summary>
        public static Token Implicit => new Token(TokendType.Implicit);

        /// <summary>
        /// in
        /// </summary>
        public static Token In => new Token(TokendType.In);

        /// <summary>
        /// int
        /// </summary>
        public static Token Int => new Token(TokendType.Int);

        /// <summary>
        /// interface
        /// </summary>
        public static Token Interface => new Token(TokendType.Interface);

        /// <summary>
        /// internal
        /// </summary>
        public static Token Internal => new Token(TokendType.Internal);

        /// <summary>
        /// is
        /// </summary>
        public static Token Is => new Token(TokendType.Is);

        /// <summary>
        /// lock
        /// </summary>
        public static Token Lock => new Token(TokendType.Lock);

        /// <summary>
        /// long
        /// </summary>
        public static Token Long => new Token(TokendType.Long);

        /// <summary>
        /// namespace
        /// </summary>
        public static Token Namespace => new Token(TokendType.Namespace);

        /// <summary>
        /// new
        /// </summary>
        public static Token New => new Token(TokendType.New);

        /// <summary>
        /// null
        /// </summary>
        public static Token Null => new Token(TokendType.Null);

        /// <summary>
        /// object
        /// </summary>
        public static Token Object => new Token(TokendType.Object);

        /// <summary>
        /// operator
        /// </summary>
        public static Token Operator => new Token(TokendType.Operator);

        /// <summary>
        /// out
        /// </summary>
        public static Token Out => new Token(TokendType.Out);

        /// <summary>
        /// override
        /// </summary>
        public static Token Override => new Token(TokendType.Override);

        /// <summary>
        /// params
        /// </summary>
        public static Token Params => new Token(TokendType.Params);

        /// <summary>
        /// private
        /// </summary>
        public static Token Private => new Token(TokendType.Private);

        /// <summary>
        /// protected
        /// </summary>
        public static Token Protected => new Token(TokendType.Protected);

        /// <summary>
        /// public
        /// </summary>
        public static Token Public => new Token(TokendType.Public);

        /// <summary>
        /// readonly
        /// </summary>
        public static Token Readonly => new Token(TokendType.Readonly);

        /// <summary>
        /// ref
        /// </summary>
        public static Token Ref => new Token(TokendType.Ref);

        /// <summary>
        /// return
        /// </summary>
        public static Token Return => new Token(TokendType.Return);

        /// <summary>
        /// sbyte
        /// </summary>
        public static Token Sbyte => new Token(TokendType.Sbyte);

        /// <summary>
        /// sealed
        /// </summary>
        public static Token Sealed => new Token(TokendType.Sealed);

        /// <summary>
        /// short
        /// </summary>
        public static Token Short => new Token(TokendType.Short);

        /// <summary>
        /// sizeof
        /// </summary>
        public static Token Sizeof => new Token(TokendType.Sizeof);

        /// <summary>
        /// stackalloc
        /// </summary>
        public static Token Stackalloc => new Token(TokendType.Stackalloc);

        /// <summary>
        /// static
        /// </summary>
        public static Token Static => new Token(TokendType.Static);

        /// <summary>
        /// string
        /// </summary>
        public static Token String => new Token(TokendType.String);

        /// <summary>
        /// struct
        /// </summary>
        public static Token Struct => new Token(TokendType.Struct);

        /// <summary>
        /// switch
        /// </summary>
        public static Token Switch => new Token(TokendType.Switch);

        /// <summary>
        /// this
        /// </summary>
        public static Token This => new Token(TokendType.This);

        /// <summary>
        /// throw
        /// </summary>
        public static Token Throw => new Token(TokendType.Throw);

        /// <summary>
        /// true
        /// </summary>
        public static Token True => new Token(TokendType.True);

        /// <summary>
        /// try
        /// </summary>
        public static Token Try => new Token(TokendType.Try);

        /// <summary>
        /// typeof
        /// </summary>
        public static Token Typeof => new Token(TokendType.Typeof);

        /// <summary>
        /// uint
        /// </summary>
        public static Token Uint => new Token(TokendType.Uint);

        /// <summary>
        /// ulong
        /// </summary>
        public static Token Ulong => new Token(TokendType.Ulong);

        /// <summary>
        /// unchecked
        /// </summary>
        public static Token Unchecked => new Token(TokendType.Unchecked);

        /// <summary>
        /// unsafe
        /// </summary>
        public static Token Unsafe => new Token(TokendType.Unsafe);

        /// <summary>
        /// ushort
        /// </summary>
        public static Token Ushort => new Token(TokendType.Ushort);

        /// <summary>
        /// using
        /// </summary>
        public static Token Using => new Token(TokendType.Using);

        /// <summary>
        /// virtual
        /// </summary>
        public static Token Virtual => new Token(TokendType.Virtual);

        /// <summary>
        /// void
        /// </summary>
        public static Token Void => new Token(TokendType.Void);

        /// <summary>
        /// volatile
        /// </summary>
        public static Token Volatile => new Token(TokendType.Volatile);

        /// <summary>
        /// while
        /// </summary>
        public static Token While => new Token(TokendType.While);
        #endregion

        #region ContextualKeywords
        /// <summary>
        /// add
        /// </summary>
        public static Token Add => new Token(TokendType.Add);

        /// <summary>
        /// alias
        /// </summary>
        public static Token Alias => new Token(TokendType.Alias);

        /// <summary>
        /// ascending
        /// </summary>
        public static Token Ascending => new Token(TokendType.Ascending);

        /// <summary>
        /// async
        /// </summary>
        public static Token Async => new Token(TokendType.Async);

        /// <summary>
        /// await
        /// </summary>
        public static Token Await => new Token(TokendType.Await);

        /// <summary>
        /// by
        /// </summary>
        public static Token By => new Token(TokendType.By);

        /// <summary>
        /// descending
        /// </summary>
        public static Token Descending => new Token(TokendType.Descending);

        /// <summary>
        /// dynamic
        /// </summary>
        public static Token Dynamic => new Token(TokendType.Dynamic);

        /// <summary>
        /// equals
        /// </summary>
        public static Token EqualsKeyword => new Token(TokendType.Equals);

        /// <summary>
        /// from
        /// </summary>
        public static Token From => new Token(TokendType.From);

        /// <summary>
        /// get
        /// </summary>
        public static Token Get => new Token(TokendType.Get);

        /// <summary>
        /// global
        /// </summary>
        public static Token Global => new Token(TokendType.Global);

        /// <summary>
        /// group
        /// </summary>
        public static Token Group => new Token(TokendType.Group);

        /// <summary>
        /// into
        /// </summary>
        public static Token Into => new Token(TokendType.Into);

        /// <summary>
        /// join
        /// </summary>
        public static Token Join => new Token(TokendType.Join);

        /// <summary>
        /// let
        /// </summary>
        public static Token Let => new Token(TokendType.Let);

        /// <summary>
        /// nameof
        /// </summary>
        public static Token Nameof => new Token(TokendType.Nameof);

        /// <summary>
        /// on
        /// </summary>
        public static Token On => new Token(TokendType.On);

        /// <summary>
        /// orderby
        /// </summary>
        public static Token Orderby => new Token(TokendType.Orderby);

        /// <summary>
        /// partial (type)
        /// </summary>
        public static Token PartialType => new Token(TokendType.PartialType);

        /// <summary>
        /// partial (method)
        /// </summary>
        public static Token PartialMethod => new Token(TokendType.PartialMethod);

        /// <summary>
        /// remove
        /// </summary>
        public static Token Remove => new Token(TokendType.Remove);

        /// <summary>
        /// select
        /// </summary>
        public static Token Select => new Token(TokendType.Select);

        /// <summary>
        /// set
        /// </summary>
        public static Token Set => new Token(TokendType.Set);

        /// <summary>
        /// unmanaged (generic type constraint)
        /// </summary>
        public static Token UnmanagedGenericTypeConstraint => new Token(TokendType.UnmanagedGenericTypeConstraint);

        /// <summary>
        /// value
        /// </summary>
        public static Token Value => new Token(TokendType.Value);

        /// <summary>
        /// var
        /// </summary>
        public static Token Var => new Token(TokendType.Var);

        /// <summary>
        /// when (filter condition)
        /// </summary>
        public static Token WhenFilterCondition => new Token(TokendType.WhenFilterCondition);

        /// <summary>
        /// when where (generic type constraint)
        /// </summary>
        public static Token WhereGenericTypeConstraint => new Token(TokendType.WhereGenericTypeConstraint);

        /// <summary>
        /// where (query clause)
        /// </summary>
        public static Token WhereQueryClause => new Token(TokendType.WhereQueryClause);

        /// <summary>
        /// yield
        /// </summary>
        public static Token Yield => new Token(TokendType.Yield);
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
            if (obj is Token part)
            {
                return Equals(part);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_value, _tokenType);
        }

        public static bool operator ==(Token left, Token right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Token left, Token right)
        {
            return !(left == right);
        }

        public bool Equals(Token other)
        {
            return (_value == other._value) && (_tokenType == other._tokenType);
        }

        public override string ToString()
        {
            if (IsKeyword)
            {
                return KeywordHelper.GetValue(Keywords[_tokenType]);
            }

            if (IsContextualKeyword)
            {
                return ContextualKeywordHelper.GetValue(ContextualKeywords[_tokenType]);
            }

            if (IsASCIIChar)
            {
                return ASCIICharHelper.GetValue(ASCIIChars[_tokenType]).ToString(new CultureInfo("en-US"));
            }

            return GetAnyString();
        }
    }
}
