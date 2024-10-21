/*
Author:     LAN QUANG HUYNH
Partner:    None
Course:     CS 3500, University of Utah, School of Computing
GitHub ID:  henrika2
Repo:       https://github.com/uofu-cs3500-20-fall2024/spreadsheet-henrika2
Date:       17-Sep-2024 (when submission was completed) 
Project:    Formula
Copyright:  CS 3500 and [LAN QUANG HUYNH] - This work may not be copied for use in Academic Coursework.
*/

/*
# Comments to Evaluators:

Assignment 2:

This project implements a basic Spreadsheet application with functionality for parsing, validating, and representing formulas. The primary focus was on ensuring that the syntax validation and token parsing are robust and able to handle various edge cases, such as invalid sequences of operators and parentheses.

A significant portion of the project time was spent refining the `Formula` class, particularly in handling nested expressions and ensuring the consistency of the `ToString` output. Additionally, care was taken to adhere to the principles of Test Driven Development (TDD), with comprehensive unit tests covering both typical and edge cases.

One challenge encountered was optimizing the `ToString` method to run in constant time, as required by the project specification. Through careful caching of the canonical string representation, I was able to achieve the desired performance.

Overall, the project lays a solid foundation for future extensions, such as formula evaluation and integration into a full spreadsheet UI. 

Assignment 4:

This project focuses on the implementation of a `Formula` class, which supports formula parsing, validation, and evaluation. The primary objective was to ensure correct handling of arithmetic expressions, variable substitution, and error management during formula evaluation. Special attention was given to performance and compliance with the assignment specifications.

A major portion of the time spent on this project was dedicated to implementing the `Evaluate` method, ensuring that the formula is evaluated correctly using both standard functions and lambda expressions for variable lookup. Edge cases, such as division by zero and undefined variables, were handled gracefully with appropriate error reporting via the `FormulaError` class.

Test Driven Development (TDD) was employed throughout, with a variety of unit tests covering evaluation, equality (`==`, `!=`), and `GetHashCode` functionality. These tests include scenarios with and without variables and ensure correctness under different evaluation contexts.

One of the challenges faced was maintaining the performance and accuracy of the formula comparison methods (`Equals`, `==`, `!=`) while ensuring that the `ToString` output remains consistent across all formula objects. This required implementing efficient canonical string handling and caching mechanisms.

# Assignment 2 Specific Topics

The project implements the following features as required by the assignment:
- Syntax validation of formulas, including proper handling of parentheses and operators.
- Token parsing using regular expressions, with support for variables and numbers.
- A `GetVariables` method that accurately identifies and returns all variables within a formula.
- A `ToString` method that returns a canonical string representation of the formula, optimized to run in O(1) time.

# Assignment 4 Specific Topics

The project implements the following features as required by the assignment:
- Formula evaluation using a stack-based algorithm to handle arithmetic operations.
- Error management with a `FormulaError` class to report issues like division by zero or undefined variables.
- Formula comparison through `Equals`, `==`, `!=`, and `GetHashCode` methods.
- Comprehensive unit tests covering formula evaluation, equality, and hashing, following TDD principles.


*/
