## Author Information
- **Author**: LAN QUANG HUYNH
- **Partner**: None
- **Course**: CS 3500, University of Utah, School of Computing
- **GitHub ID**: henrika2
- **Repo**: [Spreadsheet Repository](https://github.com/uofu-cs3500-20-fall2024/spreadsheet-henrika2)
- **Date**: 16-October-2024 (when submission was completed)
- **Copyright**: CS 3500 and [LAN QUANG HUYNH] - This work may not be copied for use in Academic Coursework.

## Comments to Evaluators

The unit tests implemented for the Spreadsheet class are designed to ensure its core functionality operates correctly under various conditions. They include validating cell values, ensuring correct formula evaluation with dependency management, detecting circular dependencies, and verifying the Save and Load operations. Tests also handle exceptional cases such as invalid cell names and formulas. Additionally, a stress test was implemented to validate the performance of the Spreadsheet class with a large number of cells and dependencies.

## Assignment Specific Topics

- The `StressTest_MassCellInsertionAndDependencyChains` method evaluates the spreadsheet's performance with a large number of cells and formulas, ensuring that cells are correctly inserted, dependencies are properly recalculated, and performance is reasonable under heavy load.
- The `SaveAndLoad_ValidFile_CorrectlySavesAndLoadsContents` method verifies the correct saving and loading functionality of the spreadsheet to and from a file, ensuring that all cell contents, including strings, numbers, and formulas, are accurately preserved.
- The `SetContentsOfCell_CircularDependency_ThrowsCircularException`, method checks the handling of circular dependencies, ensuring that the correct exception is thrown and the spreadsheet remains consistent.

## Examples of Good Software Practice (GSP)

1. **Comprehensive Unit Testing**: The `SpreadsheetTests` class includes a wide range of unit tests that cover various scenarios, including normal operation, edge cases, and exceptional situations. This thorough testing strategy ensures that the `Spreadsheet` class functions correctly across different inputs and conditions, thereby increasing reliability and maintainability.

2. **Use of Descriptive Test Method Names**: The test method names in the `SpreadsheetTests`  class are clear and descriptive, such as `SetContentsOfCell_ValidCellNames_NoException`. This naming convention improves readability and allows other developers to quickly understand the purpose of each test.

3. **Modular Test Structure**: he tests are organized into modular units that focus on specific features, such as cell content handling, formula evaluation, and file operations. This separation of concerns enhances test maintainability and ease of understanding.

### Other Good Software Practices Achieved:
- Assertions that check for both expected results and error conditions.
- Clear documentation for the test class, including copyright and authorship information.
