
## Author Information
- **Author**: LAN QUANG HUYNH
- **Partner**: None
- **Course**: CS 3500, University of Utah, School of Computing
- **GitHub ID**: henrika2
- **Repo**: [Spreadsheet Repository](https://github.com/uofu-cs3500-20-fall2024/spreadsheet-henrika2)
- **Date**: 16-October-2024 (when submission was completed)
- **Copyright**: CS 3500 and [LAN QUANG HUYNH] - This work may not be copied for use in Academic Coursework.

## Comments to Evaluators
This implementation of the Spreadsheet class models the core functionality of a spreadsheet, including cell storage, formula evaluation, and dependency management between cells. The class uses efficient data structures to handle operations, such as recalculation of dependent cells and validation of cell names and formulas.

The key design changes include the addition of constructors, JSON-based save/load functionality, simplified API for setting cell contents, and enhanced error handling for file operations. The class also supports a new indexer to access cell values and a Changed property that tracks spreadsheet modifications. All public methods were thoroughly tested with unit tests, including edge cases and performance under stress.
## Assignment Specific Topics

- The `Save ` method allows the spreadsheet to be saved as a JSON file, ensuring that all cell contents are serialized correctly. The spreadsheet is marked as unchanged after a successful save.
- The `Load ` method loads the spreadsheet from a JSON file, with error handling in place to ensure that the original spreadsheet is restored if any issues arise during loading.
- The `SetContentsOfCell ` method simplifies content setting by determining if the input is a string, double, or formula and updating the cell accordingly, ensuring cell names are validated in one place.
- The `GetCellValue ` method efficiently retrieves the value of a cell, ensuring that it is precomputed when content is set.
- The spreadsheet indexer `[]`  allows direct access to cell values, providing a convenient way to interact with the spreadsheet.
- The `Changed ` property tracks whether the spreadsheet has been modified since the last save or load, making it easier to manage the spreadsheet's state.

## Examples of Good Software Practice (GSP)

1. **Separation of Concerns**: The project separates key functionalities like cell management, formula evaluation, and dependency tracking into distinct classes. This modularity enhances maintainability and testability.

2. **DRY Principle (Don't Repeat Yourself)**:  Repeated logic, such as cell name validation, is centralized in a single method (ValidateCellName). This reduces code duplication and improves maintainability.

3. **Well-Named and Documented Methods**: All methods are named descriptively, and XML comments are provided to explain their purpose, parameters, return values, and exceptions. This documentation ensures clarity for other developers and evaluators.

### Other Good Software Practices Achieved:
- Comprehensive Testing: Unit tests cover all public methods and edge cases, ensuring correctness and robustness.
- Custom Exception Handling: Meaningful exceptions (InvalidNameException, CircularException, SpreadsheetReadWriteException) are used to handle error cases gracefully.
- Stress Testing: A StressTest validates the performance and scalability of the spreadsheet under heavy load (e.g., with thousands of cells and dependencies).