// <copyright file="FormulaSyntaxTests.cs" company="UofU-CS3500">
//   Copyright (c) 2024 UofU-CS3500. All rights reserved.
// </copyright>
// <authors>LAN QUANG HUYNH</authors>
// <date>09/03/2024</date>


namespace CS3500.FormulaTests;

using Formula;

/// <summary>
/// Author:    LAN QUANG HUYNH
/// Partner:   None
/// Date:      09/04/2024
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
///    [... and of course you should describe the contents of the
///    file in broad terms here ...]
/// </summary>

/// <summary>
///   <para>
///     The following class shows the basics of how to use the MSTest framework,
///     including:
///   </para>
///   <list type="number">
///     <item> How to catch exceptions. </item>
///     <item> How a test of valid code should look. </item>
///   </list>
/// </summary>
[TestClass]
public class FormulaSyntaxTests
{
    // --- Tests for One Token Rule ---

    /// <summary>
    ///   <para>
    ///     This test makes sure the right kind of exception is thrown
    ///     when trying to create a formula with no tokens.
    ///   </para>
    ///   <remarks>
    ///     <list type="bullet">
    ///       <item>
    ///         We use the _ (discard) notation because the formula object
    ///         is not used after that point in the method.  Note: you can also
    ///         use _ when a method must match an interface but does not use
    ///         some of the required arguments to that method.
    ///       </item>
    ///       <item>
    ///         string.Empty is often considered best practice (rather than using "") because it
    ///         is explicit in intent (e.g., perhaps the coder forgot to but something in "").
    ///       </item>
    ///       <item>
    ///         The name of a test method should follow the MS standard:
    ///         https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices
    ///       </item>
    ///       <item>
    ///         All methods should be documented, but perhaps not to the same extent
    ///         as this one.  The remarks here are for your educational
    ///         purposes (i.e., a developer would assume another developer would know these
    ///         items) and would be superfluous in your code.
    ///       </item>
    ///       <item>
    ///         Notice the use of the attribute tag [ExpectedException] which tells the test
    ///         that the code should throw an exception, and if it doesn't an error has occurred;
    ///         i.e., the correct implementation of the constructor should result
    ///         in this exception being thrown based on the given poorly formed formula.
    ///       </item>
    ///     </list>
    ///   </remarks>
    ///   <example>
    ///     <code>
    ///        // here is how we call the formula constructor with a string representing the formula
    ///        _ = new Formula( "5+5" );
    ///     </code>
    ///   </example>
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestNoTokens_Invalid()
    {
        _ = new Formula("");  // note: it is arguable that you should replace "" with string.Empty for readability and clarity of intent (e.g., not a cut and paste error or a "I forgot to put something there" error).
    }

    /// <summary>
    /// Tests that a formula with a single number token is valid.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestSingleNumberToken_Valid()
    {
        _ = new Formula("5");
    }

    /// <summary>
    /// Tests that a <see cref="FormulaFormatException"/> is thrown when the formula contains a single negative number token.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestSingleNegativeNumberToken_Invalid()
    {
        _ = new Formula("-5");
    }

    /// <summary>
    /// Tests that a formula with a single floating-point number token is valid.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestSingleFloatingNumberToken_Valid()
    {
        _ = new Formula("5.5");
    }

    /// <summary>
    /// Tests that a formula with a single negative exponential number token (uppercase 'E') is valid.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestSingleExpotionalNumberTokenUpperCase_Valid()
    {
        _ = new Formula("3.5E-6");
    }

    /// <summary>
    /// Tests that a formula with a single negative exponential number token (lowercase 'e') is valid.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestSingleExpotionalNumberTokenLowerCase_Valid()
    {
        _ = new Formula("3.5e-6");
    }

    /// <summary>
    /// Tests that a formula with a single positive exponential number token (uppercase 'E') is valid.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestSinglePositiveExpotionalNumberTokenUpperCase_Valid()
    {
        _ = new Formula("2E5");
    }

    /// <summary>
    /// Tests that a formula with a single positive exponential number token (lowercase 'e') is valid.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestSinglePositiveExpotionalNumberTokenLowerCase_Valid()
    {
        _ = new Formula("2e5");
    }

    /// <summary>
    /// Tests that a formula with a single valid variable token is valid.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestSingleVariableToken_Valid()
    {
        _ = new Formula("xX1");
    }

    /// <summary>
    /// Tests that a <see cref="FormulaFormatException"/> is thrown when the formula contains a variable with no digits.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestSingleVariableTokenNoDigit_Invalid()
    {
        _ = new Formula("x");
    }

    /// <summary>
    /// Tests that a formula with a single valid variable token containing multiple digits is valid.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestSingleVariableTokenManyDigit_Valid()
    {
        _ = new Formula("x123");
    }

    /// <summary>
    /// Tests that a formula with a single valid variable token containing multiple lowercase letters is valid.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestSingleVariableTokenManyLetterLowerCase_Valid()
    {
        _ = new Formula("xx1");
    }

    /// <summary>
    /// Tests that a formula with a single valid variable token containing a mix of uppercase and lowercase letters and multiple digits is valid.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestSingleVariableTokenManyLetterAndManyDigitMixed_Valid()
    {
        _ = new Formula("Xx123");
    }

    /// <summary>
    /// Tests that a <see cref="FormulaFormatException"/> is thrown when the formula contains a single invalid token.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestSingleInvalidToken_Invalid()
    {
        _ = new Formula("$");
    }

    /// <summary>
    /// Tests that a <see cref="FormulaFormatException"/> is thrown when the formula contains invalid parentheses within a variable.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestSingleInvalidParenthesisInVariable_Invalid()
    {
        _ = new Formula("sqrt(2)");
    }

    /// <summary>
    /// Tests that a <see cref="FormulaFormatException"/> is thrown when the formula contains a variable with a digit at the start.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestSingleVariableDigitComeFirst_Invalid()
    {
        _ = new Formula("1a");
    }

    /// <summary>
    /// Tests that a <see cref="FormulaFormatException"/> is thrown when the formula contains a variable with a digit between letters.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestSingleVariableDigitBetweenLetter_Invalid()
    {
        _ = new Formula("a1a");
    }

    // --- Tests for Valid Token Rule ---

    /// <summary>
    /// Tests that a formula with valid tokens and no variables is valid.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestValidNoVariable_Valid()
    {
        _ = new Formula("5 + 1 * (3 - 2) / 4");
    }

    /// <summary>
    /// Tests that a formula with valid tokens and no parentheses is valid.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestValidNoParentheses_Valid()
    {
        _ = new Formula("5 + 5.5 / 4");
    }

    /// <summary>
    /// Tests that a <see cref="FormulaFormatException"/> is thrown when the formula contains a variable with no digits.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestInvalidVariableNoDigit_Invalid()
    {
        _ = new Formula("5 + x");
    }

    /// <summary>
    /// Tests that a <see cref="FormulaFormatException"/> is thrown when the formula contains a variable with a digit at the start.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestInvalidVariableDigitComeFirst_Invalid()
    {
        _ = new Formula("5 + 1x");
    }

    /// <summary>
    /// Tests that a <see cref="FormulaFormatException"/> is thrown when the formula contains a variable with a digit between letters.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestInvalidVariableDigitBetweenLetter_Invalid()
    {
        _ = new Formula("5 + x1y");
    }

    /// <summary>
    /// Tests that a formula with valid tokens (numbers, variables, and operators) is valid.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestValidTokensMixed_Valid()
    {
        _ = new Formula("5 + x1 * (3 - 2) / 4");
    }

    /// <summary>
    /// Tests that a formula with division by zero is valid.
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestValidTokensDivideBy0_Valid()
    {
        _ = new Formula("5/0");
    }

    /// <summary>
    /// Tests that a <see cref="FormulaFormatException"/> is thrown when the formula contains invalid tokens.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestInvalidTokens_Invalid()
    {
        _ = new Formula("5 + x1 * (3 - 2) / 4 + #");
    }

    // --- Tests for Closing Parenthesis Rule

    /// <summary>
    ///   <para>
    ///     Verifies that a <see cref="FormulaFormatException"/> is thrown when the formula
    ///     contains more closing parentheses than opening ones.
    ///   </para>
    ///   <remarks>
    ///     <para>
    ///       This test ensures that the constructor properly identifies an invalid formula
    ///       due to an extra closing parenthesis.
    ///     </para>
    ///   </remarks>
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestMoreClosingParentheses_Invalid()
    {
        _ = new Formula("(1 + 2))");
    }

    /// <summary>
    ///   <para>
    ///     Verifies that a <see cref="FormulaFormatException"/> is thrown when a closing parenthesis
    ///     appears without a corresponding opening parenthesis.
    ///   </para>
    ///   <remarks>
    ///     <para>
    ///       This test is designed to catch cases where the formula has an improperly placed closing
    ///       parenthesis, ensuring that such formulas are rejected.
    ///     </para>
    ///   </remarks>
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestInvalidClosingParenthesesComeBeforeOpening_Invalid()
    {
        _ = new Formula("1)(");
    }

    /// <summary>
    ///   <para>
    ///     Validates that a formula with correctly placed closing parentheses is accepted.
    ///   </para>
    ///   <remarks>
    ///     <para>
    ///       This test ensures that a well-formed formula with balanced parentheses passes the constructor without throwing any exceptions.
    ///     </para>
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestCorrectClosingParentheses_Valid()
    {
        _ = new Formula("(x1 + (x4 * 3))");
    }

    // --- Tests for Balanced Parentheses Rule

    /// <summary>
    ///   <para>
    ///     Verifies that a <see cref="FormulaFormatException"/> is thrown when the parentheses in the formula
    ///     are unbalanced.
    ///   </para>
    ///   <remarks>
    ///     <para>
    ///       This test ensures that the constructor catches and rejects formulas where the parentheses do not properly match,
    ///       such as when there is an opening parenthesis without a corresponding closing parenthesis.
    ///     </para>
    ///   </remarks>
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestUnbalancedParentheses_Invalid()
    {
        _ = new Formula("(x1");
    }

    /// <summary>
    ///   <para>
    ///     Validates that a formula with balanced parentheses is correctly accepted by the constructor without throwing an exception.
    ///   </para>
    ///   <remarks>
    ///     <para>
    ///       This test ensures that the constructor properly handles formulas where the parentheses are balanced, meaning that each opening parenthesis has a corresponding closing parenthesis in the correct order.
    ///     </para>
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestBalancedParentheses_Valid()
    {
        _ = new Formula("(1 + (2 - 3)) * (4 / 5)");
    }

    // --- Tests for First Token Rule

    /// <summary>
    ///   <para>
    ///     Make sure a simple well formed formula is accepted by the constructor (the constructor
    ///     should not throw an exception).
    ///   </para>
    ///   <remarks>
    ///     This is an example of a test that is not expected to throw an exception, i.e., it succeeds.
    ///     In other words, the formula "1+1" is a valid formula which should not cause any errors.
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestFirstTokenNumber_Valid()
    {
        _ = new Formula("1+1");
    }

    /// <summary>
    ///   <para>
    ///     Validates that a formula starting with an opening parenthesis is accepted by the constructor.
    ///   </para>
    ///   <remarks>
    ///     <para>
    ///       This test ensures that the constructor correctly handles formulas that begin with an opening parenthesis.
    ///     </para>
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestFirstTokenParentheses_Valid()
    {
        _ = new Formula("(1) + 1");
    }

    /// <summary>
    ///   <para>
    ///     Verifies that a <see cref="FormulaFormatException"/> is thrown when the formula starts with an operator.
    ///   </para>
    ///   <remarks>
    ///     <para>
    ///       This test is designed to catch cases where the formula improperly begins with an operator, ensuring that such formulas are rejected.
    ///     </para>
    ///   </remarks>
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestFirstTokenOperator_Invalid()
    {
        _ = new Formula("+ 1");
    }

    /// <summary>
    ///   <para>
    ///     Validates that a formula starting with a variable is accepted by the constructor.
    ///   </para>
    ///   <remarks>
    ///     <para>
    ///       This test checks that a formula starting with a variable is correctly processed by the constructor.
    ///     </para>
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestFirstTokenVariable_Valid()
    {
        _ = new Formula("yX21 + 1");
    }

    // --- Tests for  Last Token Rule ---

    /// <summary>
    ///   <para>
    ///     Verifies that a <see cref="FormulaFormatException"/> is thrown when the formula ends with an operator.
    ///   </para>
    ///   <remarks>
    ///     <para>
    ///       This test ensures that the constructor catches and rejects formulas that end with an operator, which is invalid.
    ///     </para>
    ///   </remarks>
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestLastTokenOperator_Invalid()
    {
        _ = new Formula("1 + 2 *");
    }

    /// <summary>
    ///   <para>
    ///     Validates that a formula ending with a number is accepted by the constructor.
    ///   </para>
    ///   <remarks>
    ///     <para>
    ///       This test confirms that a formula ending with a number is properly processed and does not throw any exceptions.
    ///     </para>
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestLastTokenNumber_Valid()
    {
        _ = new Formula("1 + 2e6");
    }

    /// <summary>
    ///   <para>
    ///     Validates that a formula ending with a closing parenthesis is accepted by the constructor.
    ///   </para>
    ///   <remarks>
    ///     <para>
    ///       This test ensures that a formula ending with a closing parenthesis is correctly accepted without throwing exceptions.
    ///     </para>
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestLastTokenClosingParenthesis_Valid()
    {
        _ = new Formula("(1 + 2) * (3 + 4)");
    }

    /// <summary>
    ///   <para>
    ///     Validates that a formula ending with a variable is accepted by the constructor.
    ///   </para>
    ///   <remarks>
    ///     <para>
    ///       This test checks that a formula ending with a variable is correctly processed without any exceptions.
    ///     </para>
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestLastTokenVariable_Valid()
    {
        _ = new Formula("1 + x1");
    }

    // --- Tests for Parentheses/Operator Following Rule ---

    /// <summary>
    ///   <para>
    ///     Verifies that a <see cref="FormulaFormatException"/> is thrown when an operator is followed by another operator.
    ///   </para>
    ///   <remarks>
    ///     <para>
    ///       This test ensures that the constructor correctly identifies and rejects formulas where an operator is improperly followed by another operator.
    ///     </para>
    ///   </remarks>
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestInvalidTokenAfterOperator_Invalid()
    {
        _ = new Formula("1 + * 2");
    }

    /// <summary>
    ///   <para>
    ///     Verifies that a <see cref="FormulaFormatException"/> is thrown when an opening parenthesis is followed by an operator.
    ///   </para>
    ///   <remarks>
    ///     <para>
    ///       This test ensures that the constructor properly rejects formulas where an operator directly follows an opening parenthesis, which is not allowed.
    ///     </para>
    ///   </remarks>
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestInvalidTokenAfterOpenParenthesis_Invalid()
    {
        _ = new Formula("(+ 1)");
    }

    /// <summary>
    ///   <para>
    ///     Verifies that a <see cref="FormulaFormatException"/> is thrown when an opening parenthesis is followed by a close patenthesis.
    ///   </para>
    ///   <remarks>
    ///     <para>
    ///       This test ensures that the constructor properly rejects formulas where a close patenthesis directly follows an opening parenthesis, which is not allowed.
    ///     </para>
    ///   </remarks>
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestInvalidCloseParenthesisAfterOpenParenthesis_Invalid()
    {
        _ = new Formula("()");
    }

    /// <summary>
    ///   <para>
    ///     Validates that a formula where an operator is followed by a valid token (number, variable, or opening parenthesis) is accepted by the constructor.
    ///   </para>
    ///   <remarks>
    ///     <para>
    ///       This test checks that the constructor correctly processes formulas where valid tokens follow an operator, ensuring that no exceptions are thrown.
    ///     </para>
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestValidTokenAfterOperator_Valid()
    {
        _ = new Formula("3E-6 - Xx1");
    }

    /// <summary>
    ///   <para>
    ///     Validates that a formula where an opening parenthesis is followed by a valid token (number, variable, or another opening parenthesis) is accepted by the constructor.
    ///   </para>
    ///   <remarks>
    ///     <para>
    ///       This test ensures that the constructor properly processes formulas where valid tokens follow an opening parenthesis, allowing the formula to be correctly evaluated.
    ///     </para>
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestValidVariableAfterOpenParenthesis_Valid()
    {
        _ = new Formula("(XXY1)");
    }

    // --- Tests for Extra Following Rule ---

    /// <summary>
    ///   <para>
    ///     Verifies that a <see cref="FormulaFormatException"/> is thrown when a number is followed by another number without an operator in between.
    ///   </para>
    ///   <remarks>
    ///     <para>
    ///       This test ensures that the constructor catches and rejects formulas where numbers are improperly placed without an operator between them.
    ///     </para>
    ///   </remarks>
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestInvalidTokenAfterNumber_Invalid()
    {
        _ = new Formula("1 1 + 2");
    }

    /// <summary>
    ///   <para>
    ///     Verifies that a <see cref="FormulaFormatException"/> is thrown when a variable is followed by another variable without an operator in between.
    ///   </para>
    ///   <remarks>
    ///     <para>
    ///       This test ensures that the constructor rejects formulas where variables are improperly placed without an operator between them.
    ///     </para>
    ///   </remarks>
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestInvalidTokenAfterVariable_Invalid()
    {
        _ = new Formula("1 + x1 x2 + 3");
    }

    /// <summary>
    ///   <para>
    ///     Verifies that a <see cref="FormulaFormatException"/> is thrown when a closing parenthesis is followed by a number without an operator in between.
    ///   </para>
    ///   <remarks>
    ///     <para>
    ///       This test ensures that the constructor correctly identifies and rejects formulas where a closing parenthesis is directly followed by a number, which is not valid.
    ///     </para>
    ///   </remarks>
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestInvalidTokenAfterCloseParenthesis_Invalid()
    {
        _ = new Formula("(2) 1");
    }

    /// <summary>
    ///   <para>
    ///     Verifies that a <see cref="FormulaFormatException"/> is thrown when a closing parenthesis is followed by an open parentheses without an operator in between.
    ///   </para>
    ///   <remarks>
    ///     <para>
    ///       This test ensures that the constructor correctly identifies and rejects formulas where a closing parenthesis is directly followed by an open parentheses, which is not valid.
    ///     </para>
    ///   </remarks>
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void FormulaConstructor_TestInvalidOpenparenthesesAfterCloseParenthesis_Invalid()
    {
        _ = new Formula("(2) (2)");
    }

    /// <summary>
    ///   <para>
    ///     Validates that a formula where a number is followed by a valid token (operator, closing parenthesis, etc.) is accepted by the constructor.
    ///   </para>
    ///   <remarks>
    ///     <para>
    ///       This test checks that the constructor properly processes formulas where valid tokens follow a number, ensuring that no exceptions are thrown.
    ///     </para>
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestValidTokenAfterNumber_Valid()
    {
        _ = new Formula("1 * xX2");
    }

    /// <summary>
    ///   <para>
    ///     Validates that a formula where a variable is followed by a valid token (operator, closing parenthesis, etc.) is accepted by the constructor.
    ///   </para>
    ///   <remarks>
    ///     <para>
    ///       This test ensures that the constructor correctly handles formulas where valid tokens follow a variable, allowing the formula to be evaluated without issues.
    ///     </para>
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestValidTokenAfterVariable_Valid()
    {
        _ = new Formula("2 + x1 + 1");
    }

    /// <summary>
    ///   <para>
    ///     Validates that a formula where a closing parenthesis is followed by a valid token (operator, closing parenthesis, etc.) is accepted by the constructor.
    ///   </para>
    ///   <remarks>
    ///     <para>
    ///       This test checks that the constructor properly processes formulas where valid tokens follow a closing parenthesis, ensuring that no exceptions are thrown.
    ///     </para>
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void FormulaConstructor_TestValidTokenAfterCloseParenthesis_Valid()
    {
        _ = new Formula("(2) + x1");
    }

    // --- Tests for GetVariables method ---

    /// <summary>
    ///   <remarks>
    ///     <para>
    ///       This test ensures that the method identifies and returns all distinct variables in the formula.
    ///     </para>
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void GetVariables_FormulaWithVariables_ShouldReturnVariableSet()
    {
        // Arrange
        string formula = "a1 + b2 - c3";
        Formula f = new(formula);

        // Act
        ISet<string> variables = f.GetVariables();

        // Assert
        ISet<string> expectedVariables = new HashSet<string> { "A1", "B2", "C3" };
        Assert.IsTrue(variables.SetEquals(expectedVariables));
    }

    /// <summary>
    ///   <remarks>
    ///     <para>
    ///       This test ensures that the method identifies and returns no duplicate variables in the formula.
    ///     </para>
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void GetVariables_FormulaWithVariables_ShouldReturnNoDuplicateVariableSet()
    {
        // Arrange
        string formula = "a1 + b2 - b2";
        Formula f = new(formula);

        // Act
        ISet<string> variables = f.GetVariables();

        // Assert
        ISet<string> expectedVariables = new HashSet<string> { "A1", "B2"};
        Assert.IsTrue(variables.SetEquals(expectedVariables));
    }

    /// <summary>
    ///   <remarks>
    ///     <para>
    ///       This test checks that the method behaves correctly when there are no variables in the formula.
    ///     </para>
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void GetVariables_FormulaWithNoVariables_ShouldReturnEmptySet()
    {
        // Arrange
        string formula = "2 + 3 - (5)";
        Formula f = new(formula);

        // Act
        ISet<string> variables = f.GetVariables();

        // Assert
        Assert.AreEqual(0, variables.Count);
    }

    // --- Tests for ToSttring method ---

    /// <summary>
    ///   <remarks>
    ///     <para>
    ///       This test ensures that the method returns the correct string representation, ignoring unnecessary spaces.
    ///     </para>
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void ToString_FormulaWithSpaces_ShouldReturnCanonicalRepresentation()
    {
        // Arrange
        string formula = " a1  +  b2 - 2.05 ";
        Formula f = new(formula);

        // Act
        string result = f.ToString();

        // Assert
        Assert.AreEqual("A1+B2-2.05", result);
    }

    /// <summary>
    ///   <remarks>
    ///     <para>
    ///       This test checks that the method returns the correct representation even if the input formula has no spaces.
    ///     </para>
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void ToString_FormulaWithoutSpaces_ShouldReturnSameRepresentation()
    {
        // Arrange
        string formula = "a1+b2-2e5";
        Formula f = new(formula);

        // Act
        string result = f.ToString();

        // Assert
        Assert.AreEqual("A1+B2-200000", result);
    }

    /// <summary>
    ///   <remarks>
    ///     <para>
    ///       This test ensures that the method returns a string that properly reflects the formula's structure.
    ///     </para>
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void ToString_FormulaWithMultipleOperators_ShouldReturnCorrectRepresentation()
    {
        // Arrange
        string formula = "a1 + (b2 - c3) * d4 / e5";
        Formula f = new(formula);

        // Act
        string result = f.ToString();

        // Assert
        Assert.AreEqual("A1+(B2-C3)*D4/E5", result);
    }

    /// <summary>
    ///   <remarks>
    ///     <para>
    ///       This test ensures that the method correctly handles and returns a formula with nested parentheses.
    ///     </para>
    ///   </remarks>
    /// </summary>
    [TestMethod]
    public void ToString_FormulaWithNestedParentheses_ShouldReturnCorrectRepresentation()
    {
        // Arrange
        string formula = "(a1 + (b2 * c3)) - d4";
        Formula f = new(formula);

        // Act
        string result = f.ToString();

        // Assert
        Assert.AreEqual("(A1+(B2*C3))-D4", result);
    }

}