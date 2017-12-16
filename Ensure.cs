////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Ensure.cs
//
// summary:	Implements the ensure class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;

namespace OpenNLP.Common
{
    /// <summary>   An ensure. </summary>
    public static class Ensure
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Argument not null. </summary>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        ///
        /// <param name="argument">     The argument. </param>
        /// <param name="argumentName"> Name of the argument. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [DebuggerStepThrough]
        public static void ArgumentNotNull(object argument, string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Argument is null. </summary>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        ///
        /// <param name="argument">     The argument. </param>
        /// <param name="argumentName"> Name of the argument. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [DebuggerStepThrough]
        public static void ArgumentIsNull(object argument, string argumentName)
        {
            if (ReferenceEquals(argument, null))
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Is not null. </summary>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        ///
        /// <param name="argument">     The argument. </param>
        /// <param name="argumentName"> Name of the argument. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [DebuggerStepThrough]
        public static void IsNotNull(object argument, string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   String is valid. </summary>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        ///
        /// <param name="argument">     The argument. </param>
        /// <param name="argumentName"> Name of the argument. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [DebuggerStepThrough]
        public static void StringIsValid(string argument, string argumentName)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Condition is met. </summary>
        ///
        /// <exception cref="ArgumentException">    Thrown when one or more arguments have unsupported or
        ///                                         illegal values. </exception>
        ///
        /// <param name="expr">     True to expression. </param>
        /// <param name="message">  (Optional) The message. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [DebuggerStepThrough]
        public static void ConditionIsMet(bool expr, string message = "")
        {
            if (!expr)
                throw new ArgumentException(message);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Operation is valid. </summary>
        ///
        /// <exception cref="InvalidOperationException">    Thrown when the requested operation is
        ///                                                 invalid. </exception>
        ///
        /// <param name="oper">     True to operator. </param>
        /// <param name="message">  (Optional) The message. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [DebuggerStepThrough]
        public static void OperationIsValid(bool oper, string message = "")
        {
            if (!oper)
                throw new InvalidOperationException(message);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Operation is within bounds. </summary>
        ///
        /// <exception cref="ArgumentOutOfRangeException">  Thrown when one or more arguments are outside
        ///                                                 the required range. </exception>
        ///
        /// <param name="oper">         True to operator. </param>
        /// <param name="paramName">    Name of the parameter. </param>
        /// <param name="actualValue">  The actual value. </param>
        /// <param name="message">      The message. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        [DebuggerStepThrough]
        public static void OperationIsWithinBounds(bool oper, string paramName, object actualValue, string message)
        {
            if (!oper)
                throw new ArgumentOutOfRangeException(paramName, actualValue, message);
        }
    }
}
