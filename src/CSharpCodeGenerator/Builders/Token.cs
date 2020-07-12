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

        private readonly TokenType _tokenType;

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
            _tokenType = TokenType.AnyString;
        }

        internal Token(TokenType tokenType)
        {
            if (!System.Enum.IsDefined(typeof(TokenType), tokenType))
            {
                throw new InvalidEnumArgumentException(nameof(tokenType), (int)tokenType, typeof(TokenType));
            }

            if (tokenType == TokenType.AnyString)
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
        private static Dictionary<TokenType, ASCIIChar> ASCIIChars => new Dictionary<TokenType, ASCIIChar>
        {
            { TokenType.Space, ASCIIChar.Space },
            { TokenType.ExclamationMark, ASCIIChar.ExclamationMark },
            { TokenType.QuotationMark, ASCIIChar.QuotationMark },
            { TokenType.NumberSign, ASCIIChar.NumberSign },
            { TokenType.DollarSign, ASCIIChar.DollarSign },
            { TokenType.PercentSign, ASCIIChar.PercentSign },
            { TokenType.Ampersand, ASCIIChar.Ampersand },
            { TokenType.Apostrophe, ASCIIChar.Apostrophe },
            { TokenType.LeftParentheses, ASCIIChar.LeftParentheses },
            { TokenType.RightParentheses, ASCIIChar.RightParentheses },
            { TokenType.Asterisk, ASCIIChar.Asterisk },
            { TokenType.PlusSign, ASCIIChar.PlusSign },
            { TokenType.Commma, ASCIIChar.Commma },
            { TokenType.HyphenMinus, ASCIIChar.HyphenMinus },
            { TokenType.FullStop, ASCIIChar.FullStop },
            { TokenType.Slash, ASCIIChar.Slash },
            { TokenType.Colon, ASCIIChar.Colon },
            { TokenType.Semicolon, ASCIIChar.Semicolon },
            { TokenType.LessThanSign, ASCIIChar.LessThanSign },
            { TokenType.Equals, ASCIIChar.Equals },
            { TokenType.GreaterThanSign, ASCIIChar.GreaterThanSign },
            { TokenType.QuestionMark, ASCIIChar.QuestionMark },
            { TokenType.AtSign, ASCIIChar.AtSign },
            { TokenType.LeftSquareBracket, ASCIIChar.LeftSquareBracket },
            { TokenType.Backslash, ASCIIChar.Backslash },
            { TokenType.RightSquareBracket, ASCIIChar.RightSquareBracket },
            { TokenType.CircumflexAccent, ASCIIChar.CircumflexAccent },
            { TokenType.LowLine, ASCIIChar.LowLine },
            { TokenType.GraveAccent, ASCIIChar.GraveAccent },
            { TokenType.LeftCurlyBracket, ASCIIChar.LeftCurlyBracket },
            { TokenType.VerticalBar, ASCIIChar.VerticalBar },
            { TokenType.RightCurlyBracket, ASCIIChar.RightCurlyBracket },
            { TokenType.Tilde, ASCIIChar.Tilde }
        };

        /// <summary>
        /// Keywords dictionary.
        /// </summary>
        private static Dictionary<TokenType, Keyword> Keywords => new Dictionary<TokenType, Keyword>
        {
            { TokenType.Abstract, Keyword.Abstract },
            { TokenType.As, Keyword.As },
            { TokenType.Base, Keyword.Base },
            { TokenType.Bool, Keyword.Bool },
            { TokenType.Break, Keyword.Break },
            { TokenType.Byte, Keyword.Byte },
            { TokenType.Case, Keyword.Case },
            { TokenType.Catch, Keyword.Catch },
            { TokenType.Char, Keyword.Char },
            { TokenType.Checked, Keyword.Checked },
            { TokenType.Class, Keyword.Class },
            { TokenType.Const, Keyword.Const },
            { TokenType.Continue, Keyword.Continue },
            { TokenType.Decimal, Keyword.Decimal },
            { TokenType.Default, Keyword.Default },
            { TokenType.Delegate, Keyword.Delegate },
            { TokenType.Do, Keyword.Do },
            { TokenType.Double, Keyword.Double },
            { TokenType.Else, Keyword.Else },
            { TokenType.Enum, Keyword.Enum },
            { TokenType.Event, Keyword.Event },
            { TokenType.Explicit, Keyword.Explicit },
            { TokenType.Extern, Keyword.Extern },
            { TokenType.False, Keyword.False },
            { TokenType.Finally, Keyword.Finally },
            { TokenType.Fixed, Keyword.Fixed },
            { TokenType.Float, Keyword.Float },
            { TokenType.For, Keyword.For },
            { TokenType.Foreach, Keyword.Foreach },
            { TokenType.Goto, Keyword.Goto },
            { TokenType.If, Keyword.If },
            { TokenType.Implicit, Keyword.Implicit },
            { TokenType.In, Keyword.In },
            { TokenType.Int, Keyword.Int },
            { TokenType.Interface, Keyword.Interface },
            { TokenType.Internal, Keyword.Internal },
            { TokenType.Is, Keyword.Is },
            { TokenType.Lock, Keyword.Lock },
            { TokenType.Long, Keyword.Long },
            { TokenType.Namespace, Keyword.Namespace },
            { TokenType.New, Keyword.New },
            { TokenType.Null, Keyword.Null },
            { TokenType.Object, Keyword.Object },
            { TokenType.Operator, Keyword.Operator },
            { TokenType.Out, Keyword.Out },
            { TokenType.Override, Keyword.Override },
            { TokenType.Params, Keyword.Params },
            { TokenType.Private, Keyword.Private },
            { TokenType.Protected, Keyword.Protected },
            { TokenType.Public, Keyword.Public },
            { TokenType.Readonly, Keyword.Readonly },
            { TokenType.Ref, Keyword.Ref },
            { TokenType.Return, Keyword.Return },
            { TokenType.Sbyte, Keyword.Sbyte },
            { TokenType.Sealed, Keyword.Sealed },
            { TokenType.Short, Keyword.Short },
            { TokenType.Sizeof, Keyword.Sizeof },
            { TokenType.Stackalloc, Keyword.Stackalloc },
            { TokenType.Static, Keyword.Static },
            { TokenType.String, Keyword.String },
            { TokenType.Struct, Keyword.Struct },
            { TokenType.Switch, Keyword.Switch },
            { TokenType.This, Keyword.This },
            { TokenType.Throw, Keyword.Throw },
            { TokenType.True, Keyword.True },
            { TokenType.Try, Keyword.Try },
            { TokenType.Typeof, Keyword.Typeof },
            { TokenType.Uint, Keyword.Uint },
            { TokenType.Ulong, Keyword.Ulong },
            { TokenType.Unchecked, Keyword.Unchecked },
            { TokenType.Unsafe, Keyword.Unsafe },
            { TokenType.Ushort, Keyword.Ushort },
            { TokenType.Using, Keyword.Using },
            { TokenType.Virtual, Keyword.Virtual },
            { TokenType.Void, Keyword.Void },
            { TokenType.Volatile, Keyword.Volatile },
            { TokenType.While, Keyword.While }
        };

        /// <summary>
        /// ContextualKeywords dictionary.
        /// </summary>
        private static Dictionary<TokenType, ContextualKeyword> ContextualKeywords => new Dictionary<TokenType, ContextualKeyword>
        {
            { TokenType.Add, ContextualKeyword.Add },
            { TokenType.Alias, ContextualKeyword.Alias },
            { TokenType.Ascending, ContextualKeyword.Ascending },
            { TokenType.Async, ContextualKeyword.Async },
            { TokenType.Await, ContextualKeyword.Await },
            { TokenType.By, ContextualKeyword.By },
            { TokenType.Descending, ContextualKeyword.Descending },
            { TokenType.Dynamic, ContextualKeyword.Dynamic },
            { TokenType.Equals, ContextualKeyword.Equals },
            { TokenType.From, ContextualKeyword.From },
            { TokenType.Get, ContextualKeyword.Get },
            { TokenType.Global, ContextualKeyword.Global },
            { TokenType.Group, ContextualKeyword.Group },
            { TokenType.Into, ContextualKeyword.Into },
            { TokenType.Join, ContextualKeyword.Join },
            { TokenType.Let, ContextualKeyword.Let },
            { TokenType.Nameof, ContextualKeyword.Nameof },
            { TokenType.On, ContextualKeyword.On },
            { TokenType.Orderby, ContextualKeyword.Orderby },
            { TokenType.PartialType, ContextualKeyword.PartialType },
            { TokenType.PartialMethod, ContextualKeyword.PartialMethod },
            { TokenType.Remove, ContextualKeyword.Remove },
            { TokenType.Select, ContextualKeyword.Select },
            { TokenType.Set, ContextualKeyword.Set },
            { TokenType.UnmanagedGenericTypeConstraint, ContextualKeyword.UnmanagedGenericTypeConstraint },
            { TokenType.Value, ContextualKeyword.Value },
            { TokenType.Var, ContextualKeyword.Var },
            { TokenType.WhenFilterCondition, ContextualKeyword.WhenFilterCondition },
            { TokenType.WhereGenericTypeConstraint, ContextualKeyword.WhereGenericTypeConstraint },
            { TokenType.WhereQueryClause, ContextualKeyword.WhereQueryClause },
            { TokenType.Yield, ContextualKeyword.Yield }
        };

        public bool IsKeyword => Keywords.ContainsKey(_tokenType);

        public bool IsContextualKeyword => ContextualKeywords.ContainsKey(_tokenType);

        public bool IsASCIIChar => ASCIIChars.ContainsKey(_tokenType);

        public bool IsAnyString => _tokenType == TokenType.AnyString;

        #region ASCII Chars
        /// <summary>
        /// &nbsp;
        /// </summary>
        public static Token Space => new Token(TokenType.Space);

        /// <summary>
        /// !
        /// </summary>
        public static Token ExclamationMark => new Token(TokenType.ExclamationMark);

        /// <summary>
        /// "
        /// </summary>
        public static Token QuotationMark => new Token(TokenType.QuotationMark);

        /// <summary>
        /// #
        /// </summary>
        public static Token NumberSign => new Token(TokenType.NumberSign);

        /// <summary>
        /// $
        /// </summary>
        public static Token DollarSign => new Token(TokenType.DollarSign);

        /// <summary>
        /// %
        /// </summary>
        public static Token PercentSign => new Token(TokenType.PercentSign);

        /// <summary>
        /// &amp;
        /// </summary>
        public static Token Ampersand => new Token(TokenType.Ampersand);

        /// <summary>
        /// '
        /// </summary>
        public static Token Apostrophe => new Token(TokenType.Apostrophe);

        /// <summary>
        /// (
        /// </summary>
        public static Token LeftParentheses => new Token(TokenType.LeftParentheses);

        /// <summary>
        /// )
        /// </summary>
        public static Token RightParentheses => new Token(TokenType.RightParentheses);

        /// <summary>
        /// *
        /// </summary>
        public static Token Asterisk => new Token(TokenType.Asterisk);

        /// <summary>
        /// +
        /// </summary>
        public static Token PlusSign => new Token(TokenType.PlusSign);

        /// <summary>
        /// ,
        /// </summary>
        public static Token Commma => new Token(TokenType.Commma);

        /// <summary>
        /// -
        /// </summary>
        public static Token HyphenMinus => new Token(TokenType.HyphenMinus);

        /// <summary>
        /// .
        /// </summary>
        public static Token FullStop => new Token(TokenType.FullStop);

        /// <summary>
        /// /
        /// </summary>
        public static Token Slash => new Token(TokenType.Slash);

        /// <summary>
        /// :
        /// </summary>
        public static Token Colon => new Token(TokenType.Colon);

        /// <summary>
        /// ;
        /// </summary>
        public static Token Semicolon => new Token(TokenType.Semicolon);

        /// <summary>
        /// &lt;
        /// </summary>
        public static Token LessThanSign => new Token(TokenType.LessThanSign);

        /// <summary>
        /// =
        /// </summary>
        public static Token EqualSign => new Token(TokenType.EqualSign);

        /// <summary>
        /// &gt;
        /// </summary>
        public static Token GreaterThanSign => new Token(TokenType.GreaterThanSign);

        /// <summary>
        /// ?
        /// </summary>
        public static Token QuestionMark => new Token(TokenType.QuestionMark);

        /// <summary>
        /// @
        /// </summary>
        public static Token AtSign => new Token(TokenType.AtSign);

        /// <summary>
        /// [
        /// </summary>
        public static Token LeftSquareBracket => new Token(TokenType.LeftSquareBracket);

        /// <summary>
        /// \
        /// </summary>
        public static Token Backslash => new Token(TokenType.Backslash);

        /// <summary>
        /// ]
        /// </summary>
        public static Token RightSquareBracket => new Token(TokenType.RightSquareBracket);

        /// <summary>
        /// ^
        /// </summary>
        public static Token CircumflexAccent => new Token(TokenType.CircumflexAccent);

        /// <summary>
        /// _
        /// </summary>
        public static Token LowLine => new Token(TokenType.LowLine);

        /// <summary>
        /// `
        /// </summary>
        public static Token GraveAccent => new Token(TokenType.GraveAccent);

        /// <summary>
        /// {
        /// </summary>
        public static Token LeftCurlyBracket => new Token(TokenType.LeftCurlyBracket);

        /// <summary>
        /// |
        /// </summary>
        public static Token VerticalBar => new Token(TokenType.VerticalBar);

        /// <summary>
        /// }
        /// </summary>
        public static Token RightCurlyBracket => new Token(TokenType.RightCurlyBracket);

        /// <summary>
        /// ~
        /// </summary>
        public static Token Tilde => new Token(TokenType.Tilde);
        #endregion

        #region Separator other than spaces
        /// <summary>
        /// End of line.
        /// </summary>
        public static Token EndOfLine => new Token(TokenType.EndOfLine);

        /// <summary>
        /// Indent.
        /// </summary>
        public static Token Indent => new Token(TokenType.Indent);
        #endregion

        #region Keywords
        /// <summary>
        /// abstract
        /// </summary>
        public static Token Abstract => new Token(TokenType.Abstract);

        /// <summary>
        /// as
        /// </summary>
        public static Token As => new Token(TokenType.As);

        /// <summary>
        /// base
        /// </summary>
        public static Token Base => new Token(TokenType.Base);

        /// <summary>
        /// bool
        /// </summary>
        public static Token Bool => new Token(TokenType.Bool);

        /// <summary>
        /// break
        /// </summary>
        public static Token Break => new Token(TokenType.Break);

        /// <summary>
        /// byte
        /// </summary>
        public static Token Byte => new Token(TokenType.Byte);

        /// <summary>
        /// case
        /// </summary>
        public static Token Case => new Token(TokenType.Case);

        /// <summary>
        /// catch
        /// </summary>
        public static Token Catch => new Token(TokenType.Catch);

        /// <summary>
        /// char
        /// </summary>
        public static Token Char => new Token(TokenType.Char);

        /// <summary>
        /// checked
        /// </summary>
        public static Token Checked => new Token(TokenType.Checked);

        /// <summary>
        /// class
        /// </summary>
        public static Token Class => new Token(TokenType.Class);

        /// <summary>
        /// const
        /// </summary>
        public static Token Const => new Token(TokenType.Const);

        /// <summary>
        /// continue
        /// </summary>
        public static Token Continue => new Token(TokenType.Continue);

        /// <summary>
        /// decimal
        /// </summary>
        public static Token Decimal => new Token(TokenType.Decimal);

        /// <summary>
        /// default
        /// </summary>
        public static Token Default => new Token(TokenType.Default);

        /// <summary>
        /// delegate
        /// </summary>
        public static Token Delegate => new Token(TokenType.Delegate);

        /// <summary>
        /// do
        /// </summary>
        public static Token Do => new Token(TokenType.Do);

        /// <summary>
        /// double
        /// </summary>
        public static Token Double => new Token(TokenType.Double);

        /// <summary>
        /// else
        /// </summary>
        public static Token Else => new Token(TokenType.Else);

        /// <summary>
        /// enum
        /// </summary>
        public static Token Enum => new Token(TokenType.Enum);

        /// <summary>
        /// event
        /// </summary>
        public static Token Event => new Token(TokenType.Event);

        /// <summary>
        /// explicit
        /// </summary>
        public static Token Explicit => new Token(TokenType.Explicit);

        /// <summary>
        /// extern
        /// </summary>
        public static Token Extern => new Token(TokenType.Extern);

        /// <summary>
        /// false
        /// </summary>
        public static Token False => new Token(TokenType.False);

        /// <summary>
        /// finally
        /// </summary>
        public static Token Finally => new Token(TokenType.Finally);

        /// <summary>
        /// fixed
        /// </summary>
        public static Token Fixed => new Token(TokenType.Fixed);

        /// <summary>
        /// float
        /// </summary>
        public static Token Float => new Token(TokenType.Float);

        /// <summary>
        /// for
        /// </summary>
        public static Token For => new Token(TokenType.For);

        /// <summary>
        /// foreach
        /// </summary>
        public static Token Foreach => new Token(TokenType.Foreach);

        /// <summary>
        /// goto
        /// </summary>
        public static Token Goto => new Token(TokenType.Goto);

        /// <summary>
        /// if
        /// </summary>
        public static Token If => new Token(TokenType.If);

        /// <summary>
        /// implicit
        /// </summary>
        public static Token Implicit => new Token(TokenType.Implicit);

        /// <summary>
        /// in
        /// </summary>
        public static Token In => new Token(TokenType.In);

        /// <summary>
        /// int
        /// </summary>
        public static Token Int => new Token(TokenType.Int);

        /// <summary>
        /// interface
        /// </summary>
        public static Token Interface => new Token(TokenType.Interface);

        /// <summary>
        /// internal
        /// </summary>
        public static Token Internal => new Token(TokenType.Internal);

        /// <summary>
        /// is
        /// </summary>
        public static Token Is => new Token(TokenType.Is);

        /// <summary>
        /// lock
        /// </summary>
        public static Token Lock => new Token(TokenType.Lock);

        /// <summary>
        /// long
        /// </summary>
        public static Token Long => new Token(TokenType.Long);

        /// <summary>
        /// namespace
        /// </summary>
        public static Token Namespace => new Token(TokenType.Namespace);

        /// <summary>
        /// new
        /// </summary>
        public static Token New => new Token(TokenType.New);

        /// <summary>
        /// null
        /// </summary>
        public static Token Null => new Token(TokenType.Null);

        /// <summary>
        /// object
        /// </summary>
        public static Token Object => new Token(TokenType.Object);

        /// <summary>
        /// operator
        /// </summary>
        public static Token Operator => new Token(TokenType.Operator);

        /// <summary>
        /// out
        /// </summary>
        public static Token Out => new Token(TokenType.Out);

        /// <summary>
        /// override
        /// </summary>
        public static Token Override => new Token(TokenType.Override);

        /// <summary>
        /// params
        /// </summary>
        public static Token Params => new Token(TokenType.Params);

        /// <summary>
        /// private
        /// </summary>
        public static Token Private => new Token(TokenType.Private);

        /// <summary>
        /// protected
        /// </summary>
        public static Token Protected => new Token(TokenType.Protected);

        /// <summary>
        /// public
        /// </summary>
        public static Token Public => new Token(TokenType.Public);

        /// <summary>
        /// readonly
        /// </summary>
        public static Token Readonly => new Token(TokenType.Readonly);

        /// <summary>
        /// ref
        /// </summary>
        public static Token Ref => new Token(TokenType.Ref);

        /// <summary>
        /// return
        /// </summary>
        public static Token Return => new Token(TokenType.Return);

        /// <summary>
        /// sbyte
        /// </summary>
        public static Token Sbyte => new Token(TokenType.Sbyte);

        /// <summary>
        /// sealed
        /// </summary>
        public static Token Sealed => new Token(TokenType.Sealed);

        /// <summary>
        /// short
        /// </summary>
        public static Token Short => new Token(TokenType.Short);

        /// <summary>
        /// sizeof
        /// </summary>
        public static Token Sizeof => new Token(TokenType.Sizeof);

        /// <summary>
        /// stackalloc
        /// </summary>
        public static Token Stackalloc => new Token(TokenType.Stackalloc);

        /// <summary>
        /// static
        /// </summary>
        public static Token Static => new Token(TokenType.Static);

        /// <summary>
        /// string
        /// </summary>
        public static Token String => new Token(TokenType.String);

        /// <summary>
        /// struct
        /// </summary>
        public static Token Struct => new Token(TokenType.Struct);

        /// <summary>
        /// switch
        /// </summary>
        public static Token Switch => new Token(TokenType.Switch);

        /// <summary>
        /// this
        /// </summary>
        public static Token This => new Token(TokenType.This);

        /// <summary>
        /// throw
        /// </summary>
        public static Token Throw => new Token(TokenType.Throw);

        /// <summary>
        /// true
        /// </summary>
        public static Token True => new Token(TokenType.True);

        /// <summary>
        /// try
        /// </summary>
        public static Token Try => new Token(TokenType.Try);

        /// <summary>
        /// typeof
        /// </summary>
        public static Token Typeof => new Token(TokenType.Typeof);

        /// <summary>
        /// uint
        /// </summary>
        public static Token Uint => new Token(TokenType.Uint);

        /// <summary>
        /// ulong
        /// </summary>
        public static Token Ulong => new Token(TokenType.Ulong);

        /// <summary>
        /// unchecked
        /// </summary>
        public static Token Unchecked => new Token(TokenType.Unchecked);

        /// <summary>
        /// unsafe
        /// </summary>
        public static Token Unsafe => new Token(TokenType.Unsafe);

        /// <summary>
        /// ushort
        /// </summary>
        public static Token Ushort => new Token(TokenType.Ushort);

        /// <summary>
        /// using
        /// </summary>
        public static Token Using => new Token(TokenType.Using);

        /// <summary>
        /// virtual
        /// </summary>
        public static Token Virtual => new Token(TokenType.Virtual);

        /// <summary>
        /// void
        /// </summary>
        public static Token Void => new Token(TokenType.Void);

        /// <summary>
        /// volatile
        /// </summary>
        public static Token Volatile => new Token(TokenType.Volatile);

        /// <summary>
        /// while
        /// </summary>
        public static Token While => new Token(TokenType.While);
        #endregion

        #region ContextualKeywords
        /// <summary>
        /// add
        /// </summary>
        public static Token Add => new Token(TokenType.Add);

        /// <summary>
        /// alias
        /// </summary>
        public static Token Alias => new Token(TokenType.Alias);

        /// <summary>
        /// ascending
        /// </summary>
        public static Token Ascending => new Token(TokenType.Ascending);

        /// <summary>
        /// async
        /// </summary>
        public static Token Async => new Token(TokenType.Async);

        /// <summary>
        /// await
        /// </summary>
        public static Token Await => new Token(TokenType.Await);

        /// <summary>
        /// by
        /// </summary>
        public static Token By => new Token(TokenType.By);

        /// <summary>
        /// descending
        /// </summary>
        public static Token Descending => new Token(TokenType.Descending);

        /// <summary>
        /// dynamic
        /// </summary>
        public static Token Dynamic => new Token(TokenType.Dynamic);

        /// <summary>
        /// equals
        /// </summary>
        public static Token EqualsKeyword => new Token(TokenType.Equals);

        /// <summary>
        /// from
        /// </summary>
        public static Token From => new Token(TokenType.From);

        /// <summary>
        /// get
        /// </summary>
        public static Token Get => new Token(TokenType.Get);

        /// <summary>
        /// global
        /// </summary>
        public static Token Global => new Token(TokenType.Global);

        /// <summary>
        /// group
        /// </summary>
        public static Token Group => new Token(TokenType.Group);

        /// <summary>
        /// into
        /// </summary>
        public static Token Into => new Token(TokenType.Into);

        /// <summary>
        /// join
        /// </summary>
        public static Token Join => new Token(TokenType.Join);

        /// <summary>
        /// let
        /// </summary>
        public static Token Let => new Token(TokenType.Let);

        /// <summary>
        /// nameof
        /// </summary>
        public static Token Nameof => new Token(TokenType.Nameof);

        /// <summary>
        /// on
        /// </summary>
        public static Token On => new Token(TokenType.On);

        /// <summary>
        /// orderby
        /// </summary>
        public static Token Orderby => new Token(TokenType.Orderby);

        /// <summary>
        /// partial (type)
        /// </summary>
        public static Token PartialType => new Token(TokenType.PartialType);

        /// <summary>
        /// partial (method)
        /// </summary>
        public static Token PartialMethod => new Token(TokenType.PartialMethod);

        /// <summary>
        /// remove
        /// </summary>
        public static Token Remove => new Token(TokenType.Remove);

        /// <summary>
        /// select
        /// </summary>
        public static Token Select => new Token(TokenType.Select);

        /// <summary>
        /// set
        /// </summary>
        public static Token Set => new Token(TokenType.Set);

        /// <summary>
        /// unmanaged (generic type constraint)
        /// </summary>
        public static Token UnmanagedGenericTypeConstraint => new Token(TokenType.UnmanagedGenericTypeConstraint);

        /// <summary>
        /// value
        /// </summary>
        public static Token Value => new Token(TokenType.Value);

        /// <summary>
        /// var
        /// </summary>
        public static Token Var => new Token(TokenType.Var);

        /// <summary>
        /// when (filter condition)
        /// </summary>
        public static Token WhenFilterCondition => new Token(TokenType.WhenFilterCondition);

        /// <summary>
        /// when where (generic type constraint)
        /// </summary>
        public static Token WhereGenericTypeConstraint => new Token(TokenType.WhereGenericTypeConstraint);

        /// <summary>
        /// where (query clause)
        /// </summary>
        public static Token WhereQueryClause => new Token(TokenType.WhereQueryClause);

        /// <summary>
        /// yield
        /// </summary>
        public static Token Yield => new Token(TokenType.Yield);
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
