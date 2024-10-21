// <copyright file="EvaluationTests.cs" company="UofU-CS3500">
//   Copyright (c) 2024 UofU-CS3500. All rights reserved.
// </copyright>
// <authors>LAN QUANG HUYNH</authors>
// <date>09/17/2024</date>



namespace CS3500.FormulaTests;

using CS3500.Formula;

/// <summary>
/// Author:    LAN QUANG HUYNH
/// Partner:   None
/// Date:      09/11/2024
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
/// This file contains unit tests for the DependencyGraph class. 
/// The tests cover a variety of scenarios, including:
/// 
/// - Testing graph creation and initial states.
/// - Adding and removing dependencies, ensuring correct updates to the graph.
/// - Replacing dependents and dependees, checking if the operations modify the graph as expected.
/// - Handling edge cases, such as duplicate dependencies and non-existent nodes.
/// - Stress testing the graph under heavy loads to verify its performance and robustness.
/// 
/// The tests validate the correctness of methods like AddDependency, RemoveDependency,
/// HasDependents, HasDependees, GetDependents, GetDependees, and Size to ensure proper 
/// functionality and behavior of the DependencyGraph class.
/// </summary>

/// <summary>
/// Unit tests for evaluating formulas in the <see cref="Formula"/> class.
/// These tests cover a variety of scenarios including basic arithmetic, variable evaluation,
/// handling of division by zero, equality, and hashing of formulas.
/// </summary>
/// <remarks>
/// The <see cref="Formula"/> class is designed to parse and evaluate arithmetic expressions
/// that may include variables, operators, and parentheses. These tests ensure that the
/// evaluation method behaves as expected for different input scenarios. 
/// Key cases include:
/// <list type="bullet">
/// <item>Evaluating formulas with basic arithmetic operations such as addition, subtraction, multiplication, and division.</item>
/// <item>Handling formulas with variables by passing values through a lookup function.</item>
/// <item>Detecting and handling division by zero with appropriate errors.</item>
/// <item>Testing formula equality, inequality, and hash code consistency.</item>
/// </list>
/// </remarks>

[TestClass]
public class EvaluationTests
{
    /// <summary>
    /// Tests that evaluating a simple addition formula without variables returns the correct result.
    /// Formula: 1 + 2
    /// </summary>
    [TestMethod]
    public void Evaluate_AdditionWithoutVariables_ReturnsCorrectValue()
    {
        Formula formula = new Formula("1 + 2");
        Assert.AreEqual(3.0, formula.Evaluate(s => 0));  // No variables
    }

    /// <summary>
    /// Tests that evaluating a formula with two consecutive additions without variables returns the correct result.
    /// Formula: 1 + 2 + 4
    /// </summary>
    [TestMethod]
    public void Evaluate_TwoAdditionsInARowWithoutVariables_ReturnsCorrectValue()
    {
        Formula formula = new Formula("1 + 2 + 4");
        Assert.AreEqual(7.0, formula.Evaluate(s => 0));  // No variables
    }

    /// <summary>
    /// Tests that evaluating a formula with variables returns the correct result when all variables are assigned the value 5.
    /// Formula: A1 + B1
    /// </summary>
    [TestMethod]
    public void Evaluate_AdditionWithVariables_ReturnsCorrectValue()
    {
        Formula formula = new Formula("A1 + B1");
        Assert.AreEqual(10.0, formula.Evaluate(s => 5.0));  // Lambda returns 5 for all variables
    }

    /// <summary>
    /// Tests that evaluating a division by zero returns a FormulaError.
    /// Formula: 5 / 0
    /// </summary>
    [TestMethod]
    public void Evaluate_DivisionByZero_ReturnsFormulaError()
    {
        Formula formula = new Formula("5 / 0");
        object result = formula.Evaluate(s => 0);
        Assert.IsInstanceOfType(result, typeof(FormulaError));
        FormulaError error = (FormulaError)result;
        Assert.AreEqual("Denominator cannot be 0", error.Reason);
    }

    /// <summary>
    /// Tests that evaluating a formula with an unknown variable returns a FormulaError.
    /// Formula: X1 + 5
    /// </summary>
    [TestMethod]
    public void Evaluate_UnknownVariable_ReturnsFormulaError()
    {
        Formula formula = new Formula("X1 + 5");
        object result = formula.Evaluate(s => throw new ArgumentException("Unknown variable"));
        Assert.IsInstanceOfType(result, typeof(FormulaError));
        FormulaError error = (FormulaError)result;
        Assert.AreEqual("Undefined variable: X1", error.Reason);  // Assuming 'Reason' is the property in FormulaError
    }

    /// <summary>
    /// Tests that evaluating a complex formula with parentheses returns the correct result.
    /// Formula: (2 + 3) * 4
    /// </summary>
    [TestMethod]
    public void Evaluate_ComplexFormula_ReturnsCorrectValue()
    {
        Formula formula = new Formula("(2 + 3) * 4");
        Assert.AreEqual(20.0, formula.Evaluate(s => 0));  // No variables, lambda returns dummy value
    }

    /// <summary>
    /// Tests that evaluating a simple subtraction formula without variables returns the correct result.
    /// Formula: 5 - 2
    /// </summary>
    [TestMethod]
    public void Evaluate_Subtraction_ReturnsCorrectValue()
    {
        Formula formula = new Formula("5 - 2");
        Assert.AreEqual(3.0, formula.Evaluate(s => 0));  // No variables
    }

    /// <summary>
    /// Tests that evaluating a formula with two consecutive subtractions returns the correct result.
    /// Formula: 5 - 2 - 3
    /// </summary>
    [TestMethod]
    public void Evaluate_TwoSubtractionsInARow_ReturnsCorrectValue()
    {
        Formula formula = new Formula("5 - 2 - 3");
        Assert.AreEqual(0.0, formula.Evaluate(s => 0));  // No variables
    }

    /// <summary>
    /// Tests that evaluating a formula with multiplication and division returns the correct result.
    /// Formula: 10 / 2 * 3
    /// </summary>
    [TestMethod]
    public void Evaluate_MultiplicationAndDivision_ReturnsCorrectValue()
    {
        Formula formula = new Formula("10 / 2 * 3");
        Assert.AreEqual(15.0, formula.Evaluate(s => 0));
    }

    /// <summary>
    /// Tests that evaluating a formula with nested parentheses returns the correct result.
    /// Formula: ((1 + 2) * (3 + 4))
    /// </summary>
    [TestMethod]
    public void Evaluate_NestedParentheses_ReturnsCorrectValue()
    {
        Formula formula = new Formula("((1 + 2) * (3 + 4))");
        Assert.AreEqual(21.0, formula.Evaluate(s => 0));
    }

    /// <summary>
    /// Tests that evaluating a complex formula with multiple operations in parentheses returns the correct result.
    /// Formula: (1+2*3-4+5*1/5+4-4)
    /// </summary>
    [TestMethod]
    public void Evaluate_ComplexInParentheses_ReturnsCorrectValue()
    {
        Formula formula = new Formula("(1+2*3-4+5*1/5+4-4)");
        Assert.AreEqual(4.0, formula.Evaluate(s => 0));
    }

    /// <summary>
    /// Tests that variable normalization is handled correctly and evaluating the formula returns the expected result.
    /// Formula: x1 + y1 * x3 (variables normalized to uppercase)
    /// </summary>
    [TestMethod]
    public void Evaluate_VariableNormalization_ReturnsCorrectValue()
    {
        Formula formula = new Formula("x1 + y1 * x3");
        Assert.AreEqual(30.0, formula.Evaluate(s => 5));  // Normalization should make both variables uppercase
    }

    /// <summary>
    /// Tests that division by zero using a variable returns a FormulaError.
    /// Formula: 5 / x1
    /// </summary>
    [TestMethod]
    public void Evaluate_DivisionByZeroVariable_ReturnsFormulaError()
    {
        Formula formula = new Formula("5 / x1");
        object result = formula.Evaluate(s => 0);
        Assert.IsInstanceOfType(result, typeof(FormulaError));
        FormulaError error = (FormulaError)result;
        Assert.AreEqual("Denominator cannot be 0", error.Reason);
    }

    /// <summary>
    /// Tests that evaluating a formula with division by a variable returns the correct result when the variable is non-zero.
    /// Formula: 5 / x1
    /// </summary>
    [TestMethod]
    public void Evaluate_DivisionVariable_ReturnsCorrectValue()
    {
        Formula formula = new Formula("5 / x1");
        Assert.AreEqual(1.0, formula.Evaluate(s => 5));
    }

    /// <summary>
    /// Tests that division inside parentheses returns the correct result.
    /// Formula: 5 / (5)
    /// </summary>
    [TestMethod]
    public void Evaluate_DivisionParentheses_ReturnsCorrectValue()
    {
        Formula formula = new Formula("5 / (5)");
        Assert.AreEqual(1.0, formula.Evaluate(s => 5));
    }

    /// <summary>
    /// Tests that division by zero inside parentheses returns a FormulaError.
    /// Formula: 5 / (0)
    /// </summary>
    [TestMethod]
    public void Evaluate_DivisionZeroParentheses_ReturnsFormulaError()
    {
        Formula formula = new Formula("5 / (0)");
        object result = formula.Evaluate(s => 0);
        Assert.IsInstanceOfType(result, typeof(FormulaError));
        FormulaError error = (FormulaError)result;
        Assert.AreEqual("Denominator cannot be 0", error.Reason);

    }

    /// <summary>
    /// Tests that equality operator returns true for two identical formulas.
    /// Formula: A1 + B1 == A1 + B1
    /// </summary>
    [TestMethod]
    public void Formula_Equality_Consistent()
    {
        Formula f1 = new Formula("A1 + B1");
        Formula f2 = new Formula("A1 + B1");
        Assert.IsTrue(f1 == f2);
    }

    /// <summary>
    /// Tests that the equality operator returns true when comparing two null formulas.
    /// </summary>
    [TestMethod]
    public void Formula_Equality_Null()
    {
        Formula? f1 = null;
        Formula? f2 = null;
#pragma warning disable CS8604 // Possible null reference argument.
        Assert.IsTrue(f1 == f2);
#pragma warning restore CS8604 // Possible null reference argument.
    }

    /// <summary>
    /// Tests that inequality operator returns true for two different formulas.
    /// Formula: A1 + B1 != A1 + B2
    /// </summary>
    [TestMethod]
    public void Formula_Inequality_Inconsistent()
    {
        Formula f1 = new Formula("A1 + B1");
        Formula f2 = new Formula("A1 + B2");
        Assert.IsTrue(f1 != f2);
    }

    /// <summary>
    /// Tests that inequality operator returns false for the same formulas.
    /// Formula: A1 + B1 != A1 + B2
    /// </summary>
    [TestMethod]
    public void Formula_Inequality_Consistent()
    {
        Formula f1 = new Formula("A1 + B1");
        Formula f2 = new Formula("A1 + B1");
        Assert.IsFalse(f1 != f2);
    }

    /// <summary>
    /// Tests that Equals function returns true for two identical formulas.
    /// Formula: A1 + B1 == A1 + B1 (using Equals method)
    /// </summary>
    [TestMethod]
    public void Formula_EqualFunction_Consistent()
    {
        Formula f1 = new Formula("A1 + B1");
        Formula f2 = new Formula("A1 + b1");
        Assert.IsTrue(f1.Equals(f2));
    }

    /// <summary>
    /// Tests that Equals function returns false when comparing a formula to null.
    /// Formula: A1 + B1 != null
    /// </summary>
    [TestMethod]
    public void Formula_EqualFunctionNull_False()
    {
        Formula f1 = new Formula("A1 + B1");
        Formula? f2 = null;
        Assert.IsFalse(f1.Equals(f2));
    }

    /// <summary>
    /// Tests that two identical formulas have the same hash code.
    /// Formula: A1 + B1
    /// </summary>
    [TestMethod]
    public void Formula_Hashing_Consistent()
    {
        Formula f1 = new Formula("A1 + B1");
        Formula f2 = new Formula("A1 + B1");
        Assert.AreEqual(f1.GetHashCode(), f2.GetHashCode());
    }

    /// <summary>
    /// Tests that two different formulas have different hash code.
    /// Formula: A1 + B1 and (A1 +B1)
    /// </summary>
    [TestMethod]
    public void Formula_Hashing_Inconsistent()
    {
        Formula f1 = new Formula("(A1 + B1)");
        Formula f2 = new Formula("A1 + B1");
        Assert.IsFalse(f1.GetHashCode().Equals(f2.GetHashCode()));
    }
}