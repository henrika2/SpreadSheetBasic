/*
Author:     LAN QUANG HUYNH
Partner:    Tiancheng Li
Start Date: 1-Sep-2024
Course:     CS 3505, University of Utah, School of Computing
GitHub ID:  henrika2
Repo:       [https://github.com/uofu-cs3500-20-fall2024/spreadsheet-henrika2](https://github.com/uofu-cs3500-20-fall2024/spreadsheetpair-lan-and-lee.git)
Commit Date: 25-October-2024 (of when submission is ready to be evaluated)
Solution:   Spreadsheet
Copyright:  CS 3500 and [LAN QUANG HUYNH] - This work may not be copied for use in Academic Coursework.
*/

/*
# Overview of the Spreadsheet functionality

The Spreadsheet solution for Assignment 6 builds on the core functionality developed over previous assignments. It supports the creation, manipulation, and evaluation of spreadsheet data, including the ability to store and retrieve cell contents, manage formulas with dependencies, and handle errors such as circular dependencies. The spreadsheet can now also save and load its data to and from JSON files, ensuring persistence and interoperability.

### Current Functionality:
- **Formula Parsing**: The system parses input strings into formulas, checking syntax and adhering to predefined rules.
- **Variable Handling**: It can identify and manage variables within formulas, allowing for dynamic calculations.
- **ToString Representation**: Formulas are stored and output in a canonical form, ensuring consistency and expected output.
- **Syntax Validation**: The system validates the formula syntax during construction, throwing appropriate exceptions for invalid inputs.
- **Formula Evaluation**: Using a `Lookup` delegate, the system evaluates formulas with variables and handles division by zero or undefined variables gracefully.
- **Dependency Management**: Circular dependencies are detected, and appropriate exceptions are raised to maintain the integrity of calculations.
- **Cell Content Handling**: The system supports strings, numbers, and formulas in cells. It validates and stores contents accordingly.
- **Saving & Loading**: The spreadsheet can be saved to a JSON file and loaded back, preserving all data and formulas. The system handles file I/O errors by throwing.
# Time Expenditures:

    1. Assignment One:   Predicted Hours:  4     Actual Hours: 8
        - Description: Implemented basic formula parsing and syntax validation.
    2. Assignment Two:   Predicted Hours:   8     Actual Hours: 12
        - Description: Added support for variable handling, ToString representation, and expanded syntax validation.
    3. Assignment Three:   Predicted Hours:  8     Actual Hours: 5
        - Description: Implemented methods and variables for the DependencyGraph class.
    4. Assignment Four:   Predicted Hours:  10    Actual Hours: 5
        - Description: Implemented formula evaluation, comparison (`==`, `!=`), and hashing for formula objects. Developed unit tests for formula evaluation using both standard functions and lambda expressions.
    5. Assignment Five:    Predicted Hours:  8     Actual Hours: 5
        - Description: Added comprehensive unit tests for the Spreadsheet class, validating core functionalities and edge cases, including circular dependencies and stress tests for performance.
    6. Assignment Six:    Predicted Hours:  6     Actual Hours: 4
        - Description: Completed the implementation of Save and Load methods using JSON serialization, enhanced unit tests, and added performance stress testing.
    7. Assignment seven:  Predicted Hours:  12     ActualHours:18
        - time spent effectively:5+4    time spent debugging:5+3     time spent learning tools and techniques:0+1  
        - Description: Completed the SpreadSheet user interface, including editing Spreadsheet and table reading and writing functions. Our ability to estimate time is still fluctuating, mainly due to some unexpected 
          situations, such as the strange bug mentioned in the project README.

# Partnership:
- In the pair programming phase of this assignment, Tiancheng Li completed the content of the SpreadsheetGUI.razor.cs file, and LAN QUANG HUYNH completed the content of SpreadsheetGUI.razor. In the subsequent debugging phase, LAN QUANG HUYNH modified part of the content of the SpreadsheetGUI.razor.cs file, and Tiancheng Li further completed the code comments and README.

- In this collaboration, we created a new branch called PS7. This branch is used to save our code during pair programming. In subsequent individual programming, we will compare the codes of both parties and pull the new code before making any changes.

- During this collaboration, we have corrected each other's mistakes and inappropriate programming habits. And during non-pair programming, we will take our own tasks to ensure that we solve different problems and avoid wasting time. 

- The area we need to improve is communication between each other. Since both of us are non-native English speakers, the communication during pair programming is not efficient, which leads to misunderstandings about code issues. We should strengthen our communication with each other and use a more professional method to communicate about code issues.
*/
