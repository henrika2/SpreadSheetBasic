/*
Author:     LAN QUANG HUYNH
Partner:    None
Course:     CS 3500, University of Utah, School of Computing
GitHub ID:  henrika2
Repo:       https://github.com/uofu-cs3500-20-fall2024/spreadsheet-henrika2
Date:       17-Sep-2024 (when submission was completed) 
Project:    FormulaTests
Copyright:  CS 3500 and [LAN QUANG HUYNH] - This work may not be copied for use in Academic Coursework.
*/

/*
# Comments to Evaluators:

Assignment 2:
This test project is designed to rigorously evaluate the functionality of the Spreadsheet application's `Formula` class. Comprehensive unit tests have been developed to ensure that all public methods are thoroughly tested, with a focus on edge cases and invalid input scenarios.

The tests are structured to verify:
- **Syntax Validation**: Ensuring that the `Formula` constructor correctly identifies and handles invalid formulas, throwing the appropriate exceptions.
- **Token Parsing**: Verifying that the tokenization process accurately handles numbers, variables, operators, and parentheses.
- **GetVariables Method**: Testing the accurate retrieval of variables from complex formulas.
- **ToString Method**: Confirming that the canonical string representation is returned in O(1) time and matches the expected format.

The test suite follows Test Driven Development (TDD) practices, with tests written before and during the implementation of the `Formula` class. This approach helped in identifying and correcting issues early in the development process.

I faced challenges in crafting tests for nested expressions and sequences with mixed operators and parentheses. However, by iterating over the test cases and refining the implementation, these challenges were successfully addressed.

The test project aims to cover all potential use cases of the `Formula` class, ensuring its robustness and reliability.

Assignment 4:
This test project has been developed to rigorously evaluate the `Formula` class, focusing on its ability to correctly parse, validate, evaluate, and handle arithmetic expressions and variables. The test suite follows the principles of Test Driven Development (TDD) to ensure that functionality is fully covered before and during implementation.

The tests are structured to verify the following:
- **Formula Evaluation**: Testing formulas with and without variables using both standard functions and lambda expressions for lookup, including scenarios with division by zero and undefined variables.
- **Equality and Hashing**: Verifying the behavior of the `Equals`, `==`, `!=`, and `GetHashCode` methods, ensuring consistency and correctness when comparing formulas.
- **Edge Case Handling**: Ensuring the `Formula` class throws appropriate exceptions for invalid input, such as malformed formulas or illegal sequences of operators and parentheses.

Comprehensive tests were designed to handle edge cases, such as deeply nested expressions and sequences involving multiple types of operations. The tests also cover error reporting, ensuring that the `FormulaError` class is correctly used to signal issues during evaluation.

Key challenges involved designing tests for formula evaluation in different contexts, especially for handling variable lookup and ensuring that formulas produce consistent results. By employing a TDD approach, these challenges were addressed iteratively and systematically.


# Assignment 2 Specific Topics

The test project includes:
- Unit tests for each public method in the `Formula` class.
- Tests covering both valid and invalid input cases, ensuring comprehensive coverage.
- Performance tests to validate the efficiency of the `ToString` method.

# Assignment 4 Specific Topics

The test project includes:
- Unit tests for formula evaluation, variable handling, and arithmetic operations.
- Tests for equality (`==`, `!=`), `Equals`, and `GetHashCode`.
- Rigorous error handling tests, ensuring the robustness of the formula evaluation process.
*/