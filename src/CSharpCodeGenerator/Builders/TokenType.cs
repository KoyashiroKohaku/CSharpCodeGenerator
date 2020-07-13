namespace KoyashiroKohaku.CSharpCodeGenerator.Builders
{
    public enum TokenType
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
