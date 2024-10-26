
## Author Information
- **Author**: LAN QUANG HUYNH
- **Partner**: Tiancheng Li
- **Course**: CS 3500, University of Utah, School of Computing
- **GitHub ID**: henrika2
- **Repo**: [Spreadsheet Repository](https://github.com/uofu-cs3500-20-fall2024/spreadsheetpair-lan-and-lee.git)
- **Date**: 16-October-2024 (when submission was completed)
- **Copyright**: CS 3500 and [LAN QUANG HUYNH] - This work may not be copied for use in Academic Coursework.

## Comments to Evaluators
LAN QUANG HUYNH went to Tim's TA hour discussed the following bug：
When the user enters content that causes an exception in an empty cell, the content bar will maintain the content that causes the exception until the user clicks a non-empty cell or manually modifies the content of the content bar.
But LAN QUANG HUYNH and Tim finally failed to find out the cause of the bug. Tim told LAN QUANG HUYNH that we should record this situation here.

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

## Partnership:
- In the pair programming phase of this assignment, Tiancheng Li completed the content of the SpreadsheetGUI.razor.cs file, and LAN QUANG HUYNH completed the content of SpreadsheetGUI.razor. In the subsequent debugging phase, LAN QUANG HUYNH modified part of the content of the SpreadsheetGUI.razor.cs file, and Tiancheng Li further completed the code comments and README.

- In this collaboration, we created a new branch called PS7. This branch is used to save our code during pair programming. In subsequent individual programming, we will compare the codes of both parties and pull the new code before making any changes.

- During this collaboration, we have corrected each other's mistakes and inappropriate programming habits. And during non-pair programming, we will take our own tasks to ensure that we solve different problems and avoid wasting time. 

- The area we need to improve is communication between each other. Since both of us are non-native English speakers, the communication during pair programming is not efficient, which leads to misunderstandings about code issues. We should strengthen our communication with each other and use a more professional method to communicate about code issues.
