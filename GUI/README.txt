
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

- HandleSaveFile: Saves the spreadsheet as a JSON file, serializing all cell contents and marking the spreadsheet as unchanged after a successful save. The method also triggers a download in the browser.
- HandleLoadFile: Loads the spreadsheet from a selected JSON file, validating the file content and ensuring error handling. If loading fails, the original spreadsheet state is restored to prevent data loss.
- HandleClear: Clears the entire spreadsheet if it hasn't been modified or if the user confirms clearing unsaved changes. It resets all cell values to empty strings and updates the GUI to reflect the cleared state.
- HandleUpdateCellInSpreadsheet: Updates the content of a specified cell based on user input, handling circular dependencies and formatting errors. It also updates dependent cells, if any, and ensures the GUI reflects the changes.

## Examples of Good Software Practice (GSP)

1. **Separation of Concerns**: Follow the MVC principle and write the model, view and control functions separately.

2. **DRY Principle (Don't Repeat Yourself)**:  Repeated logic, such as update tool bar, is centralized in a single method.

3. **Well-Named and Documented Methods**: All methods are named descriptively, and XML comments are provided to explain their purpose, parameters, return values, and exceptions. This documentation ensures clarity for other developers and evaluators.

### Other Good Software Practices Achieved:
- Version Control: The project was developed using Git, ensuring proper tracking of changes, easy collaboration, and a clear history of modifications through branches and commits. Using branches (e.g., PS7) kept the main branch stable and allowed safe experimentation.
- Code Modularity: Functions were broken down into smaller, reusable components, adhering to the single-responsibility principle. This made the code easier to maintain, test, and understand.
- User Input Validation: Comprehensive validation was implemented for user inputs, ensuring that data integrity is maintained and preventing invalid or unexpected entries from causing crashes or errors.

### Branching
- Branch Name: PS7
- All development was done on the PS7 branch to maintain a clean main branch.
- Merging from PS7 to the main branch was performed after all work was completed, ensuring no conflicts.
- Commit Numbers: 1f97bfd

###Time Reflection
- Note that: time predict and actual time is in the README in SpreadSheet solution.
- Estimate Accuracy: The initial estimate was slightly lower than the actual time spent, primarily due to unexpected debugging challenges and additional time for learning Blazor and C# integration techniques.
- Effective Time Usage: Time was effectively used during pair programming, while debugging took more time than expected due to a challenging bug that persisted. This indicates a need to improve debugging strategies.

## Partnership:
- In the pair programming phase of this assignment, Tiancheng Li completed the content of the SpreadsheetGUI.razor.cs file, and LAN QUANG HUYNH completed the content of SpreadsheetGUI.razor. In the subsequent debugging phase, LAN QUANG HUYNH modified part of the content of the SpreadsheetGUI.razor.cs file, and Tiancheng Li further completed the code comments and README.

- In this collaboration, we created a new branch called PS7. This branch is used to save our code during pair programming. In subsequent individual programming, we will compare the codes of both parties and pull the new code before making any changes.

- During this collaboration, we have corrected each other's mistakes and inappropriate programming habits. And during non-pair programming, we will take our own tasks to ensure that we solve different problems and avoid wasting time. 

- The area we need to improve is communication between each other. Since both of us are non-native English speakers, the communication during pair programming is not efficient, which leads to misunderstandings about code issues. We should strengthen our communication with each other and use a more professional method to communicate about code issues.
