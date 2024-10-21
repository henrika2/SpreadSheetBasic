// <copyright file="Formula.cs" company="UofU-CS3500">
// Copyright (c) 2024 UofU-CS3500. All rights reserved.
// </copyright>
// <authors>LAN QUANG HUYNH</authors>
// <date>09/04/2024</date>

namespace CS3500.Formula;
using System.Text.RegularExpressions;
using System.Text;

/// <summary>
/// Author:    LAN QUANG HUYNH
/// Partner:   None
/// Date:      09/23/2024
/// Course:    CS 3500, University of Utah, School of Computing
/// Copyright: CS 3500 and LAN QUANG HUYNH - This work may not
///            be copied for use in Academic Coursework.
///
/// I, LAN QUANG HUYNH, certify that I wrote this code from scratch and
/// did not copy it in part or whole from another source.  All
/// references used in the completion of the assignments are cited
/// in my README file.
///
/// File Contents
///
///
///    This file contains the implementation of the `Formula` class, which is used
///    to represent and evaluate arithmetic expressions consisting of numbers,
///    variables, and operators (such as addition, subtraction, multiplication,
///    and division). The class supports:
///    - Parsing input strings to construct a valid formula.
///    - Handling variables and retrieving their values using a delegate function.
///    - Performing formula evaluation while managing division by zero and other
///      potential errors.
///    - Comparing formulas for equality and generating consistent hash codes.
///    - Providing a canonical string representation of the formula.
///    The class also includes robust error handling, ensuring proper behavior for
///    invalid input and edge cases.
/// </summary>

/// <summary>
/// Represents a mathematical formula that can be parsed, validated, and evaluated.
/// </summary>
public class Formula
{
    /// <summary>
    ///   All variables are letters followed by numbers.  This pattern
    ///   represents valid variable name strings.
    /// </summary>
    private const string VariableRegExPattern = @"[a-zA-Z]+\d+";

    /// <summary>
    /// A list of tokens parsed from the formula string.
    /// </summary>
    private readonly List<string> tokens;

    /// <summary>
    /// The original formula string provided as input.
    /// </summary>
    private readonly string formula;

    /// <summary>
    /// The string representation of the formula after being processed.
    /// </summary>
    private readonly string formulaString;

    /// <summary>
    ///   Initializes a new instance of the <see cref="Formula"/> class.
    ///   <para>
    ///     Creates a Formula from a string that consists of an infix expression written as
    ///     described in the class comment.  If the expression is syntactically incorrect,
    ///     throws a FormulaFormatException with an explanatory Message.  See the assignment
    ///     specifications for the syntax rules you are to implement.
    ///   </para>
    ///   <para>
    ///     Non Exhaustive Example Errors:
    ///   </para>
    ///   <list type="bullet">
    ///     <item>
    ///        Invalid variable name, e.g., x, x1x  (Note: x1 is valid, but would be normalized to X1)
    ///     </item>
    ///     <item>
    ///        Empty formula, e.g., string.Empty
    ///     </item>
    ///     <item>
    ///        Mismatched Parentheses, e.g., "(("
    ///     </item>
    ///     <item>
    ///        Invalid Following Rule, e.g., "2x+5"
    ///     </item>
    ///   </list>
    /// </summary>
    /// <param name="formula"> The string representation of the formula to be created.</param>
    public Formula(string formula)
    {
        // Store the formula string
        this.formula = formula;

        // Split the formula into tokens
        this.tokens = GetTokens(formula);

        // Validate the syntax of the formula
        this.ValidateSyntax();

        StringBuilder canonicalForm = new();
        foreach (string token in this.tokens)
        {
            if (IsVar(token))
            {
                canonicalForm.Append(token.ToUpperInvariant());
            }
            else if (IsNumber(token))
            {
                // Normalize the number (e.g., remove trailing zeros)
                double number = double.Parse(token);
                canonicalForm.Append(number.ToString("G"));
            }
            else
            {
                canonicalForm.Append(token);
            }
        }

        this.formulaString = canonicalForm.ToString();
    }

    /// <summary>
    ///   <para>
    ///     Reports whether f1 == f2, using the notion of equality from the <see cref="Equals"/> method.
    ///   </para>
    /// </summary>
    /// <param name="f1"> The first of two formula objects. </param>
    /// <param name="f2"> The second of two formula objects. </param>
    /// <returns> true if the two formulas are the same.</returns>
    public static bool operator ==(Formula f1, Formula f2)
    {
        // Check for nulls first
        if (ReferenceEquals(f1, null))
        {
            return ReferenceEquals(f2, null);
        }

        // Use Equals method for comparison
        return f1.Equals(f2);
    }

    /// <summary>
    ///   <para>
    ///     Reports whether f1 != f2, using the notion of equality from the <see cref="Equals"/> method.
    ///   </para>
    /// </summary>
    /// <param name="f1"> The first of two formula objects. </param>
    /// <param name="f2"> The second of two formula objects. </param>
    /// <returns> true if the two formulas are not equal to each other.</returns>
    public static bool operator !=(Formula f1, Formula f2)
    {
        return !(f1 == f2);
    }

    /// <summary>
    ///   <para>
    ///     Returns a set of all the variables in the formula.
    ///   </para>
    ///   <remarks>
    ///     Important: no variable may appear more than once in the returned set, even
    ///     if it is used more than once in the Formula.
    ///   </remarks>
    ///   <para>
    ///     For example, if N is a method that converts all the letters in a string to upper case:
    ///   </para>
    ///   <list type="bullet">
    ///     <item>new("x1+y1*z1").GetVariables() should enumerate "X1", "Y1", and "Z1".</item>
    ///     <item>new("x1+X1"   ).GetVariables() should enumerate "X1".</item>
    ///   </list>
    /// </summary>
    /// <returns> the set of variables (string names) representing the variables referenced by the formula. </returns>
    public ISet<string> GetVariables()
    {
        ISet<string> varSet = new HashSet<string>();
        foreach (string s in this.tokens)
        {
            if (IsVar(s))
            {
                varSet.Add(s.ToUpper());
            }
        }

        return varSet;
    }

    /// <summary>
    ///   <para>
    ///     Returns a string representation of a canonical form of the formula.
    ///   </para>
    ///   <para>
    ///     The string will contain no spaces.
    ///   </para>
    ///   <para>
    ///     If the string is passed to the Formula constructor, the new Formula f
    ///     will be such that this.ToString() == f.ToString().
    ///   </para>
    ///   <para>
    ///     All of the variables in the string will be normalized.  This
    ///     means capital letters.
    ///   </para>
    ///   <para>
    ///       For example:
    ///   </para>
    ///   <code>
    ///       new("x1 + y1").ToString() should return "X1+Y1"
    ///       new("X1 + 5.0000").ToString() should return "X1+5".
    ///   </code>
    ///   <para>
    ///     This code should execute in O(1) time.
    ///   </para>
    /// </summary>
    /// <returns>
    ///   A canonical version (string) of the formula. All "equal" formulas
    ///   should have the same value here.
    /// </returns>
    public override string ToString()
    {
        return this.formulaString;
    }

    /// <summary>
    ///   <para>
    ///     Determines if two formula objects represent the same formula.
    ///   </para>
    ///   <para>
    ///     By definition, if the parameter is null or does not reference
    ///     a Formula Object then return false.
    ///   </para>
    ///   <para>
    ///     Two Formulas are considered equal if their canonical string representations
    ///     (as defined by ToString) are equal.
    ///   </para>
    /// </summary>
    /// <param name="obj"> The other object.</param>
    /// <returns>
    ///   True if the two objects represent the same formula.
    /// </returns>
    public override bool Equals(object? obj)
    {
        // If the object is null or not a Formula, return false
        if (obj == null || !(obj is Formula))
        {
            return false;
        }

        // Compare the canonical string representations (from ToString)
        Formula other = (Formula)obj;
        return this.ToString() == other.ToString();
    }

    /// <summary>
    ///   <para>
    ///     Evaluates this Formula, using the lookup delegate to determine the values of
    ///     variables.
    ///   </para>
    ///   <remarks>
    ///     When the lookup method is called, it will always be passed a Normalized (capitalized)
    ///     variable name.  The lookup method will throw an ArgumentException if there is
    ///     not a definition for that variable token.
    ///   </remarks>
    ///   <para>
    ///     If no undefined variables or divisions by zero are encountered when evaluating
    ///     this Formula, the numeric value of the formula is returned.  Otherwise, a
    ///     FormulaError is returned (with a meaningful explanation as the Reason property).
    ///   </para>
    ///   <para>
    ///     This method should never throw an exception.
    ///   </para>
    /// </summary>
    /// <param name="lookup">
    ///   <para>
    ///     Given a variable symbol as its parameter, lookup returns the variable's (double) value
    ///     (if it has one) or throws an ArgumentException (otherwise).  This method should expect
    ///     variable names to be capitalized.
    ///   </para>
    /// </param>
    /// <returns> Either a double or a formula error, based on evaluating the formula.</returns>
    public object Evaluate(Lookup lookup)
    {
        Stack<double> valueStack = new Stack<double>();
        Stack<string> operatorStack = new Stack<string>();

        IEnumerable<string> tokens = GetTokens(this.formula); // Assume GetTokens splits the formula correctly

        foreach (string token in tokens)
        {
            if (IsNumber(token))
            {
                double number = double.Parse(token);

                if (operatorStack.Count > 0 && (operatorStack.Peek() == "*" || operatorStack.Peek() == "/"))
                {
                    if (!ApplyOperator(operatorStack.Pop(), valueStack, number, valueStack.Pop()))
                    {
                        return new FormulaError("Denominator cannot be 0");
                    }
                }
                else
                {
                    valueStack.Push(number);
                }
            }
            else if (IsVar(token))
            {
                double variableValue;
                try
                {
                    variableValue = lookup(token.ToUpper());
                }
                catch (ArgumentException)
                {
                    return new FormulaError($"Undefined variable: {token}");
                }

                if (operatorStack.Count > 0 && (operatorStack.Peek() == "*" || operatorStack.Peek() == "/"))
                {
                    if(!ApplyOperator(operatorStack.Pop(), valueStack, variableValue, valueStack.Pop()))
                    {
                        return new FormulaError("Denominator cannot be 0");
                    }
                }
                else
                {
                    valueStack.Push(variableValue);
                }
            }
            else if (token == "+" || token == "-")
            {
                if (operatorStack.Count > 0 && (operatorStack.Peek() == "+" || operatorStack.Peek() == "-"))
                {
                    ApplyOperator(operatorStack.Pop(), valueStack, valueStack.Pop(), valueStack.Pop());
                }

                operatorStack.Push(token);
            }
            else if (token == "*" || token == "/")
            {
                operatorStack.Push(token);
            }
            else if (token == "(")
            {
                operatorStack.Push(token);
            }
            else if (token == ")")
            {
                if (operatorStack.Peek() != "(")
                {
                    ApplyOperator(operatorStack.Pop(), valueStack, valueStack.Pop(), valueStack.Pop());
                }

                // Pop the '(' from the operator stack
                operatorStack.Pop();

                if (operatorStack.Count > 0 && (operatorStack.Peek() == "*" || operatorStack.Peek() == "/"))
                {
                    if (!ApplyOperator(operatorStack.Pop(), valueStack, valueStack.Pop(), valueStack.Pop()))
                    {
                        return new FormulaError("Denominator cannot be 0");
                    }
                }
            }
        }

        // Final evaluation after processing all tokens
        while (operatorStack.Count > 0)
        {
            ApplyOperator(operatorStack.Pop(), valueStack, valueStack.Pop(), valueStack.Pop());
        }

        return valueStack.Pop();
    }

    /// <summary>
    ///   <para>
    ///     Returns a hash code for this Formula.  If f1.Equals(f2), then it must be the
    ///     case that f1.GetHashCode() == f2.GetHashCode().  Ideally, the probability that two
    ///     randomly-generated unequal Formulas have the same hash code should be extremely small.
    ///   </para>
    /// </summary>
    /// <returns> The hashcode for the object. </returns>
    public override int GetHashCode()
    {
        return this.ToString().GetHashCode();
    }

    /// <summary>
    ///   Determines whether the specified token matches the pattern of a valid variable.
    /// </summary>
    /// <param name="token">The string to be evaluated as a potential variable.</param>
    /// <returns>
    ///   <c>true</c> if the token is a valid variable according to the defined regular expression pattern; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    ///   <para>
    ///     This method uses a regular expression pattern to validate whether the provided token is a well-formed variable.
    ///     The pattern is matched against the entire string to ensure that the token does not contain any extraneous characters.
    ///   </para>
    ///   <para>
    ///     The method is useful for identifying variable tokens in a formula, where variables may be used alongside numbers and operators.
    ///   </para>
    /// </remarks>
    private static bool IsVar(string token)
    {
        // notice the use of ^ and $ to denote that the entire string being matched is just the variable
        string standaloneVarPattern = $"^{VariableRegExPattern}$";
        return Regex.IsMatch(token, standaloneVarPattern);
    }

    /// <summary>
    ///   Determines whether the specified token is a valid mathematical operator.
    /// </summary>
    /// <param name="token">The string to be evaluated as a potential operator.</param>
    /// <returns>
    ///   <c>true</c> if the token is one of the supported operators ("+", "-", "*", "/"); otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    ///   <para>
    ///     This method checks if the provided token is one of the four basic arithmetic operators.
    ///     It is used in formula parsing to differentiate between operators, numbers, and variables.
    ///   </para>
    ///   <para>
    ///     The method is useful in scenarios where the formula's syntax needs to be validated, ensuring that only valid operators are processed.
    ///   </para>
    /// </remarks>
    private static bool IsOperator(string token)
    {
        return token == "+" || token == "-" || token == "*" || token == "/";
    }

    /// <summary>
    ///   Determines whether the specified token can be parsed as a number.
    /// </summary>
    /// <param name="token">The string to be evaluated as a potential number.</param>
    /// <returns>
    ///   <c>true</c> if the token can be parsed as a <see cref="double"/>; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    ///   <para>
    ///     This method attempts to parse the token into a double-precision floating-point number using <see cref="double.TryParse(string, out double)"/>.
    ///     If successful, the method returns <c>true</c>, indicating that the token is a valid numeric value.
    ///   </para>
    ///   <para>
    ///     The method is particularly useful in distinguishing between numeric values and other types of tokens in a formula,
    ///     ensuring that only valid numbers are considered during formula evaluation.
    ///   </para>
    /// </remarks>
    private static bool IsNumber(string token)
    {
        return double.TryParse(token, out _);
    }

    /// <summary>
    ///   <para>
    ///     Given an expression, enumerates the tokens that compose it.
    ///   </para>
    ///   <para>
    ///     Tokens returned are:
    ///   </para>
    ///   <list type="bullet">
    ///     <item>left paren</item>
    ///     <item>right paren</item>
    ///     <item>one of the four operator symbols</item>
    ///     <item>a string consisting of one or more letters followed by one or more numbers</item>
    ///     <item>a double literal</item>
    ///     <item>and anything that doesn't match one of the above patterns</item>
    ///   </list>
    ///   <para>
    ///     There are no empty tokens; white space is ignored (except to separate other tokens).
    ///   </para>
    /// </summary>
    /// <param name="formula"> A string representing an infix formula such as 1*B1/3.0. </param>
    /// <returns> The ordered list of tokens in the formula. </returns>
    private static List<string> GetTokens(string formula)
    {
        List<string> results = new();

        string lpPattern = @"\(";
        string rpPattern = @"\)";
        string opPattern = @"[\+\-*/]";
        string doublePattern = @"(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: [eE][\+-]?\d+)?";
        string spacePattern = @"\s+";

        // Overall pattern
        string pattern = string.Format(
                                        "({0}) | ({1}) | ({2}) | ({3}) | ({4}) | ({5})",
                                        lpPattern,
                                        rpPattern,
                                        opPattern,
                                        VariableRegExPattern,
                                        doublePattern,
                                        spacePattern);

        // Enumerate matching tokens that don't consist solely of white space.
        foreach (string s in Regex.Split(formula, pattern, RegexOptions.IgnorePatternWhitespace))
        {
            if (!Regex.IsMatch(s, @"^\s*$", RegexOptions.Singleline))
            {
                results.Add(s);
            }
        }

        return results;
    }

    /// <summary>
    /// Applies the specified arithmetic operator to the given operands and pushes the result onto the value stack.
    /// </summary>
    /// <param name="op">The operator to apply. Supported operators are '+', '-', '*', and '/'.</param>
    /// <param name="valueStack">The stack of values used for the evaluation, where the result will be pushed.</param>
    /// <param name="rightOperand">The second operand for the operation, which is popped from the stack.</param>
    /// <param name="leftOperand">The first operand for the operation, which is also popped from the stack.</param>
    /// <returns>
    /// Returns true if the operation is successful. Returns false if a division by zero is encountered.
    /// </returns>
    private static bool ApplyOperator(string op, Stack<double> valueStack, double rightOperand, double leftOperand)
    {
        if (op == "+")
        {
            valueStack.Push(leftOperand + rightOperand);
        }
        else if (op == "-")
        {
            valueStack.Push(leftOperand - rightOperand);
        }
        else if (op == "*")
        {
            valueStack.Push(leftOperand * rightOperand);
        }
        else if (op == "/")
        {
            if (rightOperand == 0)
            {
                return false; // Division by zero
            }

            valueStack.Push(leftOperand / rightOperand);
        }

        return true;
    }

    /// <summary>
    /// Validates the syntax of the formula based on a set of predefined rules.
    /// Ensures that the formula conforms to the proper structure, such as correct use of operators, parentheses,
    /// numbers, and variables. Throws <see cref="FormulaFormatException"/> if any rule is violated.
    /// </summary>
    /// <remarks>
    /// The method processes each token in the formula, checking for several rules, including:
    /// <list type="bullet">
    /// <item>Operators cannot be the first token or follow another operator or an opening parenthesis.</item>
    /// <item>Opening parentheses must not follow a number, variable, or closing parenthesis.</item>
    /// <item>Closing parentheses must not follow an operator or unmatched opening parentheses.</item>
    /// <item>Variables and numbers cannot follow another variable, number, or closing parenthesis.</item>
    /// <item>Unrecognized tokens are not allowed.</item>
    /// </list>
    /// After iterating through all tokens, additional checks ensure that:
    /// <list type="bullet">
    /// <item>There are no unmatched opening parentheses.</item>
    /// <item>The formula does not end with an operator.</item>
    /// </list>
    /// </remarks>
    /// <exception cref="FormulaFormatException">
    /// Thrown when the formula violates any of the syntax rules, such as unmatched parentheses,
    /// improper placement of operators, or unrecognized tokens.
    /// </exception>
    private void ValidateSyntax()
    {
        // Initialize counters and flags
        int openParentheses = 0;
        string previousToken = string.Empty;
        if (this.tokens.Count == 0)
        {
            throw new FormulaFormatException("Invalid syntax: There must be at least one token.");
        }

        foreach (var token in this.tokens)
        {
            // Check rules related to the current token
            if (IsOperator(token))
            {
                if (previousToken == string.Empty || IsOperator(previousToken) || previousToken == "(")
                {
                    throw new FormulaFormatException("Invalid syntax: Operator cannot be the first token or follow another operator or '('.");
                }
            }
            else if (token == "(")
            {
                if (previousToken == ")" || IsNumber(previousToken) || IsVar(previousToken))
                {
                    throw new FormulaFormatException("Invalid syntax: Any token that immediately follows a number, a variable, or a closing parenthesis must be either an operator or a closing parenthesis.");
                }

                openParentheses++;
            }
            else if (token == ")")
            {
                if (openParentheses == 0 || IsOperator(previousToken))
                {
                    throw new FormulaFormatException("Invalid syntax: Unmatched ')' or ')' following an operator.");
                }
                else if (previousToken == "(")
                {
                    throw new FormulaFormatException("Any token that immediately follows an opening parenthesis or an operator must be either a number, a variable, or an opening parenthesis.");
                }

                openParentheses--;
            }
            else if (IsVar(token) || IsNumber(token))
            {
                if (previousToken != string.Empty && (IsVar(previousToken) || IsNumber(previousToken) || previousToken == ")"))
                {
                    throw new FormulaFormatException("Invalid syntax: Variables or numbers cannot follow other variables, numbers, or ')'.");
                }
            }
            else
            {
                throw new FormulaFormatException("Invalid syntax: Unrecognized token.");
            }

            previousToken = token;
        }

        // Additional checks after the loop
        if (openParentheses > 0)
        {
            throw new FormulaFormatException("Invalid syntax: Unmatched '('.");
        }

        if (IsOperator(previousToken))
        {
            throw new FormulaFormatException("Invalid syntax: Expression cannot end with an operator.");
        }
    }
}

/// <summary>
/// Used as a possible return value of the Formula.Evaluate method.
/// </summary>
public class FormulaError
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="FormulaError"/> class.
    ///   <para>
    ///     Constructs a FormulaError containing the explanatory reason.
    ///   </para>
    /// </summary>
    /// <param name="message"> Contains a message for why the error occurred.</param>
    public FormulaError(string message)
    {
        this.Reason = message;
    }

    /// <summary>
    ///  Gets the reason why this FormulaError was created.
    /// </summary>
    public string Reason { get; private set; }
}

/// <summary>
///   Any method meeting this type signature can be used for
///   looking up the value of a variable.  In general the expected behavior is that
///   the Lookup method will "know" about all variables in a formula
///   and return their appropriate value.
/// </summary>
/// <exception cref="ArgumentException">
///   If a variable name is provided that is not recognized by the implementing method,
///   then the method should throw an ArgumentException.
/// </exception>
/// <param name="variableName">
///   The name of the variable (e.g., "A1") to lookup.
/// </param>
/// <returns> The value of the given variable (if one exists). </returns>
public delegate double Lookup(string variableName);

/// <summary>
///   Used to report syntax errors in the argument to the Formula constructor.
/// </summary>
public class FormulaFormatException : Exception
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="FormulaFormatException"/> class.
    ///   <para>
    ///      Constructs a FormulaFormatException containing the explanatory message.
    ///   </para>
    /// </summary>
    /// <param name="message"> A developer defined message describing why the exception occured.</param>
    public FormulaFormatException(string message)
        : base(message)
    {
        // All this does is call the base constructor. No extra code needed.
    }
}
