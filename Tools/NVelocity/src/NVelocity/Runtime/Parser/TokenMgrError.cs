/* Generated By:JavaCC: Do not edit this line. TokenMgrError.java Version 2.1 */

namespace NVelocity.Runtime.Parser
{
	using System;
	using System.Text;

	public class TokenMgrError : ApplicationException
	{
		public override String Message
		{
			get { return base.Message; }
		}

		/*
	* Ordinals for various reasons why an Error of this type can be thrown.
	*/

		/// <summary> Lexical error occured.
		/// </summary>
		internal const int LEXICAL_ERROR = 0;

		/// <summary> An attempt wass made to create a second instance of a static token manager.
		/// </summary>
		internal const int STATIC_LEXER_ERROR = 1;

		/// <summary> Tried to change to an invalid lexical state.
		/// </summary>
		internal const int INVALID_LEXICAL_STATE = 2;

		/// <summary> Detected (and bailed out of) an infinite loop in the token manager.
		/// </summary>
		internal const int LOOP_DETECTED = 3;

		/// <summary> Indicates the reason why the exception is thrown. It will have
		/// one of the above 4 values.
		/// </summary>
		internal int errorCode;

		/// <summary> Replaces unprintable characters by their espaced (or unicode escaped)
		/// equivalents in the given string
		/// </summary>
		protected internal static String AddEscapes(String str)
		{
			StringBuilder retval = new StringBuilder();
			char ch;
			for(int i = 0; i < str.Length; i++)
			{
				switch(str[i])
				{
					case (char) (0):
						continue;

					case '\b':
						retval.Append("\\b");
						continue;

					case '\t':
						retval.Append("\\t");
						continue;

					case '\n':
						retval.Append("\\n");
						continue;

					case '\f':
						retval.Append("\\f");
						continue;

					case '\r':
						retval.Append("\\r");
						continue;

					case '\"':
						retval.Append("\\\"");
						continue;

					case '\'':
						retval.Append("\\\'");
						continue;

					case '\\':
						retval.Append("\\\\");
						continue;

					default:
						if ((ch = str[i]) < 0x20 || ch > 0x7e)
						{
							String s = "0000" + Convert.ToString(ch, 16);
							retval.Append("\\u" + s.Substring(s.Length - 4, (s.Length) - (s.Length - 4)));
						}
						else
						{
							retval.Append(ch);
						}
						continue;
				}
			}
			return retval.ToString();
		}

		/// <summary> Returns a detailed message for the Error when it is thrown by the
		/// token manager to indicate a lexical error.
		/// Parameters :
		/// EOFSeen     : indicates if EOF caused the lexicl error
		/// curLexState : lexical state in which this error occured
		/// errorLine   : line number when the error occured
		/// errorColumn : column number when the error occured
		/// errorAfter  : prefix that was seen before this error occured
		/// curchar     : the offending character
		/// Note: You can customize the lexical error message by modifying this method.
		/// </summary>
		private static String LexicalError(bool EOFSeen, int lexState, int errorLine, int errorColumn, String errorAfter,
		                                   char curChar)
		{
			return
				("Lexical error at line " + errorLine + ", column " + errorColumn + ".  Encountered: " +
				 (EOFSeen ? "<EOF> " : ("\"" + AddEscapes(curChar.ToString()) + "\"") + " (" + (int) curChar + "), ") + "after : \"" +
				 AddEscapes(errorAfter) + "\"");
		}

		/// <summary> You can also modify the body of this method to customize your error messages.
		/// For example, cases like LOOP_DETECTED and INVALID_LEXICAL_STATE are not
		/// of end-users concern, so you can return something like :
		/// *
		/// "Internal Error : Please file a bug report .... "
		/// *
		/// from this method for such cases in the release version of your parser.
		/// </summary>
		/*
	* Constructors of various flavors follow.
	*/
		public TokenMgrError()
		{
		}

		public TokenMgrError(String message, int reason) : base(message)
		{
			errorCode = reason;
		}

		public TokenMgrError(bool EOFSeen, int lexState, int errorLine, int errorColumn, String errorAfter, char curChar,
		                     int reason)
			: this(LexicalError(EOFSeen, lexState, errorLine, errorColumn, errorAfter, curChar), reason)
		{
		}
	}
}