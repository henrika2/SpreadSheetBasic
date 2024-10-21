// <copyright file="SpreadsheetTests.cs" company="UofU-CS3500">
//   Copyright (c) 2024 UofU-CS3500. All rights reserved.
// </copyright>
// <authors>LAN QUANG HUYNH</authors>
// <date>09/23/2024</date>

namespace CS3500.SpreadsheetTests;

using CS3500.Spreadsheet;
using CS3500.Formula;


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
/// This file contains the unit tests for the Spreadsheet class. It validates the core functionality of the
/// Spreadsheet, ensuring that:
///     - Cell values are correctly stored and retrieved.
///     - Formulas are accurately evaluated, including handling of dependencies.
///     - Circular dependencies are detected and appropriately handled.
///     - The Save and Load functionality is correctly implemented.
/// 
/// The test methods cover normal cases, edge cases, and exceptions to guarantee the robustness of the
/// Spreadsheet class under various conditions.
/// </summary>

/// <summary>
/// This class contains unit tests for the SetContentsOfCell and GetCellsToRecalculate methods 
/// of the Spreadsheet class. These tests verify the correct functionality of handling 
/// various types of cell content, including formulas, text, and numeric values, as well 
/// as recalculation logic and exception handling.
/// 
/// Key scenarios tested include:
/// - Proper recalculation of dependent cells when contents are modified.
/// - Detection and handling of circular dependencies through exceptions.
/// - Validations for setting cell contents with invalid names or unchanged values.
/// 
/// These tests aim to ensure the Spreadsheet class behaves correctly in complex 
/// scenarios involving dependencies between cells, maintaining data consistency and 
/// preventing invalid operations.
/// </summary>

[TestClass]
public class SpreadsheetTests
{
    /// <summary>
    /// Instance of the Spreadsheet used in the tests.
    /// </summary>
    private Spreadsheet sheet = new Spreadsheet();


    /// <summary>
    /// File name used for saving and loading valid spreadsheet data during tests.
    /// </summary>
    private const string ValidFileName = "save.txt";

    /// <summary>
    /// Example of an invalid path
    /// </summary>
    private const string InvalidFileName = "c:\\some\\local\\path\\on\\your\\drive";

    /// <summary>
    /// File name used for creating invalid JSON content to test loading errors.
    /// </summary>
    private const string InvalidJsonFileName = "invalid.json";

    /// <summary>
    /// Cleans up any files that were created during the tests.
    /// Ensures that temporary test files are removed after each test execution.
    /// </summary>
    [TestCleanup]
    public void Cleanup()
    {
        // Remove any files created during the test
        if (File.Exists(ValidFileName))
        {
            File.Delete(ValidFileName);
        }
        if (File.Exists(InvalidJsonFileName))
        {
            File.Delete(InvalidJsonFileName);
        }
    }

    // --- Tests for A5 with SetContentsCell to SetContentsOfCell ---

    /// <summary>
    /// Tests that the spreadsheet can handle a large number of cells being populated
    /// and ensures that the correct number of non-empty cells are stored.
    /// </summary>
    [TestMethod]
    public void StressTestWithLargeNumberOfCells()
    {
        // Populate the spreadsheet with 10000 cells
        for (int i = 1; i <= 10000; i++)
        {
            string cellName = "A" + i;
            sheet.SetContentsOfCell(cellName, "{i} * 1.5"); // setting numeric values
        }

        // Check that all cells are stored correctly
        ISet<string> nonEmptyCells = sheet.GetNamesOfAllNonemptyCells();
        Assert.AreEqual(10000, nonEmptyCells.Count, "Number of non-empty cells is incorrect.");
    }

    /// <summary>
    /// Tests circular dependency detection by attempting to create a circular dependency
    /// between cells A1 and B1. It ensures that a CircularException is thrown.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(CircularException))]
    public void StressTestWithCircularDependency()
    {
        // Set up a circular dependency: A1 = B1, B1 = A1
        sheet.SetContentsOfCell("A1", "=B1");
        _ = sheet.SetContentsOfCell("B1", "=A1");

    }

    // Test for GetNamesOfAllNonemptyCells
    /// <summary>
    /// Tests GetNamesOfAllNonemptyCells for an empty spreadsheet.
    /// Ensures that an empty set is returned.
    /// </summary>
    [TestMethod]
    public void TestGetNamesOfAllNonemptyCells_EmptySpreadsheet_ReturnsEmptySet()
    {
        ISet<string> nonEmptyCells = sheet.GetNamesOfAllNonemptyCells();
        Assert.AreEqual(0, nonEmptyCells.Count);
    }

    /// <summary>
    /// Tests GetNamesOfAllNonemptyCells when the spreadsheet contains non-empty cells.
    /// Ensures the correct cell names are returned.
    /// </summary>
    [TestMethod]
    public void TestGetNamesOfAllNonemptyCells_WithNonEmptyCells_ReturnsCorrectNames()
    {
        sheet.SetContentsOfCell("A1", "Hello");
        sheet.SetContentsOfCell("B2", "5.0");
        sheet.SetContentsOfCell("C3", "=A1+B2");

        ISet<string> nonEmptyCells = sheet.GetNamesOfAllNonemptyCells();
        Assert.IsTrue(nonEmptyCells.Contains("A1"));
        Assert.IsTrue(nonEmptyCells.Contains("B2"));
        Assert.IsTrue(nonEmptyCells.Contains("C3"));
        Assert.AreEqual(3, nonEmptyCells.Count);
    }

    // Test for GetCellContents

    /// <summary>
    /// Tests GetCellContents to ensure that a cell with string content
    /// returns the correct string value.
    /// </summary>
    [TestMethod]
    public void TestGetCellContents_CellWithString_ReturnsStringContent()
    {
        sheet.SetContentsOfCell("A1", "Hello");
        Assert.AreEqual("Hello", sheet.GetCellContents("A1"));
    }

    /// <summary>
    /// Tests GetCellContents to ensure that a cell with double content
    /// returns the correct double value.
    /// </summary>
    [TestMethod]
    public void TestGetCellContents_CellWithDouble_ReturnsDoubleContent()
    {
        sheet.SetContentsOfCell("B1", "10.5");
        Assert.AreEqual(10.5, sheet.GetCellContents("B1"));
    }

    /// <summary>
    /// Tests GetCellContents to ensure that a cell with a formula
    /// returns the correct formula object.
    /// </summary>
    [TestMethod]
    public void TestGetCellContents_CellWithFormula_ReturnsFormula()
    {
        Formula formula = new Formula("A1 + B1");
        sheet.SetContentsOfCell("C1", "=A1+B1");
        Assert.AreEqual(formula, sheet.GetCellContents("C1"));
    }

    /// <summary>
    /// Tests GetCellContents to ensure that an empty cell
    /// returns an empty string.
    /// </summary>
    [TestMethod]
    public void TestGetCellContents_EmptyCell_ReturnsEmptyString()
    {
        Assert.AreEqual(string.Empty, sheet.GetCellContents("D1"));
    }

    /// <summary>
    /// Tests GetCellContents with an invalid cell name to ensure
    /// that it throws an InvalidNameException.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(InvalidNameException))]
    public void TestGetCellContents_InvalidName_ThrowsInvalidNameException()
    {
        _ = sheet.GetCellContents("123");
    }

    // Test for SetContentsOfCell (double)

    /// <summary>
    /// Tests SetContentsOfCell with double content to ensure
    /// that the cell content is updated correctly.
    /// </summary>
    [TestMethod]
    public void TestSetContentsOfCell_DoubleContent_UpdatesCellContent()
    {
        IList<string> recalculatedCells = sheet.SetContentsOfCell("A1", "2.5");
        Assert.AreEqual(1, recalculatedCells.Count);
        Assert.AreEqual(2.5, sheet.GetCellContents("A1"));
    }

    /// <summary>
    /// Tests SetContentsOfCell with double content to ensure
    /// that the correct recalculated cells are returned.
    /// </summary>
    [TestMethod]
    public void TestSetContentsOfCell_DoubleContent_ReturnsRecalculatedCells()
    {
        sheet.SetContentsOfCell("A1", "2.5");
        IList<string> recalculatedCells = sheet.SetContentsOfCell("B1", "=A1*2");
        Assert.IsTrue(recalculatedCells.Contains("B1"));
    }

    /// <summary>
    /// Tests SetContentsOfCell with an invalid cell name for double content
    /// to ensure that it throws an InvalidNameException.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(InvalidNameException))]
    public void TestSetContentsOfCell_InvalidNameForDouble_ThrowsInvalidNameException()
    {
        _ = sheet.SetContentsOfCell("1B", "5.0");
    }

    /// <summary>
    /// Tests SetContentsOfCell with a valid lowercase cell name
    /// to ensure it works correctly.
    /// </summary>
    [TestMethod]
    public void TestSetContentsOfCell_ValidNameLowerCase_Valid()
    {
        _ = sheet.SetContentsOfCell("b1", "5.0");
    }

    // Test for SetContentsOfCell (string)

    /// <summary>
    /// Verifies that setting a cell's content to a string updates the cell's content correctly.
    /// </summary>
    [TestMethod]
    public void TestSetContentsOfCell_StringContent_UpdatesCellContent()
    {
        IList<string> recalculatedCells = sheet.SetContentsOfCell("A1", "Text");
        Assert.AreEqual(1, recalculatedCells.Count);
        Assert.AreEqual("Text", sheet.GetCellContents("A1"));
    }

    /// <summary>
    /// Verifies that setting a cell's content to an empty string removes the cell's content.
    /// </summary>
    [TestMethod]
    public void TestSetContentsOfCell_EmptyString_RemovesCellContent()
    {
        sheet.SetContentsOfCell("A1", "Text");
        sheet.SetContentsOfCell("A1", string.Empty);
        Assert.AreEqual(string.Empty, sheet.GetCellContents("A1"));
    }

    /// <summary>
    /// Tests that setting a cell's content with an invalid name for a string throws an InvalidNameException.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(InvalidNameException))]
    public void TestSetContentsOfCell_InvalidNameForString_ThrowsInvalidNameException()
    {
        _ = sheet.SetContentsOfCell("1B", "Text");
    }

    /// <summary>
    /// Tests that setting a cell's content with an invalid name for a string (no digit) throws an InvalidNameException.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(InvalidNameException))]
    public void TestSetContentsOfCell_InvalidNameForStringNoDigit_ThrowsInvalidNameException()
    {
        _ = sheet.SetContentsOfCell("B", "Text");
    }

    /// <summary>
    /// Tests that setting a cell's content with an empty name for a string throws an InvalidNameException.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(InvalidNameException))]
    public void TestSetContentsOfCell_InvalidNameForStringEmpty_ThrowsInvalidNameException()
    {
        _ = sheet.SetContentsOfCell("", "Text");
    }

    /// <summary>
    /// Verifies that setting a cell's content to a valid name with many letters and digits is accepted.
    /// </summary>
    [TestMethod]
    public void TestSetContentsOfCell_ValidNameForStringManyLetterAndDigit_ValidName()
    {
        _ = sheet.SetContentsOfCell("BASHB4261", "Text");
    }

    // Test for SetContentsOfCell (Formula)

    /// <summary>
    /// Verifies that setting a cell's content to a formula updates the cell's content correctly.
    /// </summary>
    [TestMethod]
    public void TestSetContentsOfCell_FormulaContent_UpdatesCellContent()
    {
        Formula formula = new Formula("A1 + B1");
        IList<string> recalculatedCells = sheet.SetContentsOfCell("C1", "=A1+B1");
        Assert.AreEqual(1, recalculatedCells.Count);
        Assert.AreEqual(formula, sheet.GetCellContents("C1"));
    }

    /// <summary>
    /// Tests setting the contents of a cell with a new double value after it previously contained a formula.
    /// Verifies that the cell's content is updated to the new double value and that the recalculated cells list
    /// contains the correct count of cells that were recalculated.
    /// </summary>
    [TestMethod]
    public void TestSetContentsOfCell_FormulaContentForNewDouble_OldCellFormula()
    {
        Formula formula = new Formula("A1 + B1");
        IList<string> recalculatedCells = sheet.SetContentsOfCell("C1", "=A1+B1");
        Assert.AreEqual(1, recalculatedCells.Count);
        Assert.AreEqual(formula, sheet.GetCellContents("C1"));
        recalculatedCells = sheet.SetContentsOfCell("C1", "2");
        Assert.AreEqual(1, recalculatedCells.Count);
        Assert.AreEqual(2.0, sheet.GetCellContents("C1"));
    }

    /// <summary>
    /// Tests setting the contents of a cell with a new string value after it previously contained a formula.
    /// Verifies that the cell's content is updated to the new string value and that the recalculated cells list
    /// contains the correct count of cells that were recalculated.
    /// </summary>
    [TestMethod]
    public void TestSetContentsOfCell_FormulaContentForNewText_OldCellFormula()
    {
        Formula formula = new Formula("A1 + B1");
        IList<string> recalculatedCells = sheet.SetContentsOfCell("C1", "=A1+B1");
        Assert.AreEqual(1, recalculatedCells.Count);
        Assert.AreEqual(formula, sheet.GetCellContents("C1"));
        recalculatedCells = sheet.SetContentsOfCell("C1", "a");
        Assert.AreEqual(1, recalculatedCells.Count);
        Assert.AreEqual("a", sheet.GetCellContents("C1"));
    }

    /// <summary>
    /// Tests setting the contents of a cell with a new formula after it previously contained a different formula.
    /// Verifies that the cell's content is updated to the new formula and that the recalculated cells list
    /// contains the correct count of cells that were recalculated.
    /// </summary>
    [TestMethod]
    public void TestSetContentsOfCell_FormulaContentForNewFormula_OldCellFormula()
    {
        Formula formula = new Formula("A1 + B1");
        IList<string> recalculatedCells = sheet.SetContentsOfCell("C1", "=A1+B1");
        Assert.AreEqual(1, recalculatedCells.Count);
        Assert.AreEqual(formula, sheet.GetCellContents("C1"));
        recalculatedCells = sheet.SetContentsOfCell("C1", "=A2+B2");
        Assert.AreEqual(1, recalculatedCells.Count);
        Assert.AreEqual(new Formula("A2 + B2"), sheet.GetCellContents("C1"));
    }

    /// <summary>
    /// Tests setting the contents of a cell with a formula that references other cells.
    /// This method verifies that when the contents of the referenced cells are changed, 
    /// the recalculation list includes all cells that depend on the updated cell, 
    /// and that the updated cell contains the correct value.
    /// </summary>
    [TestMethod]
    public void TestSetContentsOfCell_FormulaContent_RecalculateSet()
    {
        sheet.SetContentsOfCell("A1", "=C1");
        sheet.SetContentsOfCell("B1", "=A1");
        IList<string> recalculatedCells = sheet.SetContentsOfCell("C1", "1");
        CollectionAssert.AreEqual(new List<String>() { "C1", "A1", "B1" }, sheet.SetContentsOfCell("C1", "1").ToList());
        Assert.AreEqual(3, recalculatedCells.Count);
        Assert.AreEqual(1.0, sheet.GetCellContents("C1"));
    }

    /// <summary>
    /// Tests that setting cell contents with a circular dependency throws a CircularException.
    /// This method verifies that when two formulas reference each other, 
    /// an attempt to set their contents results in an expected exception.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(CircularException))]
    public void TestSetContentsOfCell_CircularDependency_ThrowsCircularException()
    {
        Formula formula1 = new Formula("B1 + 2");
        Formula formula2 = new Formula("A1 + 2");

        sheet.SetContentsOfCell("A1", "=B1+2");
        _ = sheet.SetContentsOfCell("B1", "=A1+2");
    }

    /// <summary>
    /// Tests that attempting to set cell contents in a scenario with a circular dependency 
    /// does not result in changes if the contents are effectively unchanged.
    /// This method ensures that when an invalid operation is attempted due to a circular reference,
    /// the existing value in the cell remains unchanged, and the correct dependencies are identified.
    /// </summary>
    [TestMethod]
    public void TestSetContentsOfCell_NothingChange_ThrowsCircularException()
    {
        sheet.SetContentsOfCell("A1","=C1");
        sheet.SetContentsOfCell("B1", "=A1");
        IList<string> recalculatedCells = sheet.SetContentsOfCell("C1", "1");
        CollectionAssert.AreEqual(new List<String>() { "C1", "A1", "B1" }, sheet.SetContentsOfCell("C1", "1").ToList());

        try
        {
            sheet.SetContentsOfCell("C1", "=A1");
        }
        catch (CircularException)
        {
            Assert.AreEqual(1.0, sheet.GetCellContents("C1"));
            CollectionAssert.AreEqual(new List<String>() { "C1", "A1", "B1" }, sheet.SetContentsOfCell("C1", "1").ToList());
        }
    }

    /// <summary>
    /// Tests that setting cell contents with an invalid cell name throws an InvalidNameException.
    /// This method verifies that when an invalid name is used for a cell, 
    /// the expected exception is thrown, preventing invalid operations.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(InvalidNameException))]
    public void TestSetContentsOfCell_InvalidNameForFormula_ThrowsInvalidNameException()
    {
        Formula formula = new Formula("A1 + 2");
        _ = sheet.SetContentsOfCell("1A", "=A1+2");
    }

    // Test for GetCellsToRecalculate
    /// <summary>
    /// Tests that attempting to get cells to recalculate in a scenario with a circular dependency throws a CircularException.
    /// This method ensures that when formulas reference each other in a circular manner, 
    /// an expected exception is thrown, indicating a circular reference issue.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(CircularException))]
    public void TestGetCellsToRecalculate_CircularDependency_ThrowsCircularException()
    {
        Formula formula = new Formula("B1 + 2");
        sheet.SetContentsOfCell("A1", "=B1+2");
        _ = sheet.SetContentsOfCell("B1", "=A1+2");
    }

    /// <summary>
    /// Tests that the recalculation order is correct when cell contents are updated.
    /// This method verifies that when the contents of a cell are changed, 
    /// all dependent cells are identified correctly, and the recalculation list contains all relevant cells.
    /// </summary>
    [TestMethod]
    public void TestGetCellsToRecalculate_RecalculationOrderIsCorrect()
    {
        sheet.SetContentsOfCell("A1", "2.0");
        sheet.SetContentsOfCell("B1", "=A1+2");
        sheet.SetContentsOfCell("C1", "=B1+A1");

        IList<string> recalculatedCells = sheet.SetContentsOfCell("A1", "5.0");
        Assert.AreEqual(3, recalculatedCells.Count);
        Assert.IsTrue(recalculatedCells.Contains("A1"));
        Assert.IsTrue(recalculatedCells.Contains("B1"));
        Assert.IsTrue(recalculatedCells.Contains("C1"));
    }

    // --- Tests for A6 ---

    /// <summary>
    /// This test verifies the performance and correctness of the Spreadsheet class 
    /// when handling a large number of cell insertions, updates, and dependency chains.
    /// </summary>
    [TestMethod]
    public void StressTest_MassCellInsertionAndDependencyChains()
    {
        const int numCells = 100; // Number of cells to test with
        DateTime start = DateTime.Now;

        // Step 1: Insert a large number of cells with numeric values
        for (int i = 1; i <= numCells; i++)
        {
            sheet.SetContentsOfCell($"A{i}", i.ToString());
            Assert.IsTrue(sheet.Changed); // test Chhanged property
        }

        // test GetNamesOfAllNonemptyCells
        Assert.AreEqual(100, sheet.GetNamesOfAllNonemptyCells().Count);

        // test GetCellContents
        for (int i = 1; i <= numCells; i++)
        {
            Assert.AreEqual((double)i, sheet.GetCellContents($"A{i}"));
        }

        // test GetCellValue
        for (int i = 1; i <= numCells; i++)
        {
            Assert.AreEqual((double)i, sheet.GetCellValue($"A{i}"));
        }


        // Step 2: Create a long chain of dependencies with formulas
        sheet.SetContentsOfCell("B1", "100");
        for (int i = 2; i <= numCells; i++)
        {
            sheet.SetContentsOfCell($"B{i}", $"=A{i} + B{i - 1}");
            Assert.IsTrue(sheet.Changed); // test Chhanged property
        }

        // test GetCellContents
        for (int i = 2; i <= numCells; i++)
        {
            Assert.AreEqual(new Formula($"A{i} + B{i - 1}"), sheet.GetCellContents($"B{i}"));
        }


        // test GetCellValue
        double sum = 100;
        for (int i = 2; i <= numCells; i++)
        {
            sum = sum + i;
            Assert.AreEqual(sum, sheet.GetCellValue($"B{i}"));
        }

        // test GetNamesOfAllNonemptyCells
        Assert.AreEqual(200, sheet.GetNamesOfAllNonemptyCells().Count);

        DateTime afterInsertion = DateTime.Now;
        TimeSpan insertionTime = afterInsertion - start;

        // Step 3: Check recalculation and dependency chains
        // Set a new value in A1, which should propagate recalculation across all B cells
        sheet.SetContentsOfCell("A2", "100");
        Assert.AreEqual(200.0,sheet.GetCellValue("B2"));

        DateTime afterRecalculation = DateTime.Now;
        TimeSpan recalculationTime = afterRecalculation - afterInsertion;

        // Step 4: Save and Load to verify file operations under stress
        const string StressTestFile = "stress_test_save.txt";
        sheet.Save(StressTestFile);
        Assert.IsFalse(sheet.Changed); // test Chhanged property

        Spreadsheet loadedSheet = new Spreadsheet();
        loadedSheet.Load(StressTestFile);
        Assert.IsFalse(sheet.Changed); // test Chhanged property

        // test GetNamesOfAllNonemptyCells
        Assert.AreEqual(200, sheet.GetNamesOfAllNonemptyCells().Count);

        // test GetCellContents
        for (int i = 2; i <= numCells; i++)
        {
            Assert.AreEqual(new Formula($"A{i} + B{i - 1}"), sheet.GetCellContents($"B{i}"));
        }


        // test GetCellValue
        sum = 200;
        for (int i = 3; i <= numCells; i++)
        {
            sum = sum + i;
            Assert.AreEqual(sum, sheet.GetCellValue($"B{i}"));
        }

        DateTime afterLoad = DateTime.Now;
        TimeSpan saveLoadTime = afterLoad - afterRecalculation;

        // Clean up test file
        if (File.Exists(StressTestFile))
        {
            File.Delete(StressTestFile);
        }

        // Assert reasonable performance times
        Assert.IsTrue(insertionTime.TotalSeconds < 5, "Mass cell insertion took too long");
        Assert.IsTrue(recalculationTime.TotalSeconds < 5, "Recalculation of dependencies took too long");
        Assert.IsTrue(saveLoadTime.TotalSeconds < 10, "Saving and loading took too long");

        // Verify content of loaded sheet matches original
        Assert.AreEqual(100.0, loadedSheet.GetCellValue("A2"));
        Assert.AreEqual(100.0, loadedSheet.GetCellValue("B1"));
        Assert.AreEqual(200.0, loadedSheet.GetCellValue("B2"));
    }

    /// <summary>
    /// Test the constructor with a valid name argument.
    /// </summary>
    [TestMethod]
    public void SpreadsheetConstructor_ValidName_NoException()
    {
        _ = new Spreadsheet("name");
    }

    /// <summary>
    /// Test getting the names of all non-empty cells when all cells are empty.
    /// </summary>
    [TestMethod]
    public void GetNamesOfAllNonemptyCells_AllEmptyCells_ReturnsEmptySet()
    {
        Assert.AreEqual(0, sheet.GetNamesOfAllNonemptyCells().Count);
    }

    /// <summary>
    /// Test getting the names of non-empty cells when a few cells have content.
    /// </summary>
    [TestMethod]
    public void GetNamesOfAllNonemptyCells_FewCellsHaveContent_ReturnsCorrectCount()
    {
        sheet.SetContentsOfCell("A1", "5");
        Assert.AreEqual(1, sheet.GetNamesOfAllNonemptyCells().Count);
        sheet.SetContentsOfCell("X100", "hello");
        Assert.AreEqual(2, sheet.GetNamesOfAllNonemptyCells().Count);
    }

    /// <summary>
    /// Test setting cell contents with valid cell names.
    /// </summary>
    [TestMethod]
    public void SetContentsOfCell_ValidCellNames_NoException()
    {
        _ = sheet.SetContentsOfCell("A1", "5");
        _ = sheet.SetContentsOfCell("X100", "hello");
    }

    /// <summary>
    /// Test setting cell contents and recalculate cells.
    /// </summary>
    [TestMethod]
    public void SetContentsOfCell_RecalculateCells_NoException()
    {
        sheet.SetContentsOfCell("A1", "5");
        sheet.SetContentsOfCell("A2", "=A1+1");
        sheet.SetContentsOfCell("A3", "=A1+A2");
        sheet.SetContentsOfCell("A4", "a2");
        Assert.AreEqual(6.0, sheet.GetCellValue("A2"));
        sheet.SetContentsOfCell("A1", "6");
        Assert.AreEqual(7.0, sheet.GetCellValue("A2"));
        Assert.AreEqual(13.0, sheet.GetCellValue("A3"));
        Assert.AreEqual("a2", sheet.GetCellValue("A4"));
    }

    /// <summary>
    /// Test setting cell contents with an invalid cell name.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(InvalidNameException))]
    public void SetContentsOfCell_InvalidCellName_ThrowsInvalidNameException()
    {
        sheet.SetContentsOfCell("1A", "5");
    }

    /// <summary>
    /// Test setting and getting cell contents for strings and numbers.
    /// </summary>
    [TestMethod]
    public void SetAndGetCellContents_ValidContents_ReturnsCorrectValues()
    {
        sheet.SetContentsOfCell("A1", "hello");
        Assert.AreEqual("hello", sheet.GetCellContents("A1"));

        sheet.SetContentsOfCell("A1", "5");
        Assert.AreEqual(5.0, sheet.GetCellContents("A1"));

        sheet.SetContentsOfCell("A1", "hello");
        Assert.AreEqual("hello", sheet.GetCellContents("A1"));

        sheet.SetContentsOfCell("A1", "=5");
        Assert.AreEqual(new Formula("5"), sheet.GetCellContents("A1"));

        sheet.SetContentsOfCell("A1", "");
        Assert.AreEqual(0, sheet.GetNamesOfAllNonemptyCells().Count);
    }

    /// <summary>
    /// Test getting cell contents for an empty cell.
    /// </summary>
    [TestMethod]
    public void GetCellContents_EmptyCell_ReturnsEmptyString()
    {
        Assert.AreEqual("", sheet.GetCellContents("A2"));
    }

    /// <summary>
    /// Test getting cell value for an empty cell.
    /// </summary>
    [TestMethod]
    public void GetCellValue_EmptyCell_ReturnsEmptyString()
    {
        Assert.AreEqual("", sheet.GetCellValue("A2"));
    }

    /// <summary>
    /// Test setting and evaluating a formula.
    /// </summary>
    [TestMethod]
    public void SetAndGetFormula_ValidFormula_ReturnsCorrectEvaluatedValue()
    {
        sheet.SetContentsOfCell("A1", "5");
        sheet.SetContentsOfCell("B1", "=A1 + 5");

        Assert.AreEqual(10.0, sheet["B1"]);  // The formula evaluates to 10
    }

    /// <summary>
    /// Test setting and evaluating a formula.
    /// </summary>
    [TestMethod]
    public void Indexer_ValidFormula_ReturnsCorrectEvaluatedValue()
    {
        sheet.SetContentsOfCell("A1", "5");
        sheet.SetContentsOfCell("B1", "=A1 + 5");

        Assert.AreEqual(10.0, sheet["B1"]);  // The formula evaluates to 10
        Assert.AreEqual(5.0, sheet["A1"]);
    }

    /// <summary>
    /// Test setting an invalid formula.
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(FormulaFormatException))]
    public void SetContentsOfCell_InvalidFormula_ThrowsFormulaFormatException()
    {
        _ = sheet.SetContentsOfCell("A1", "=Invalid + 5");
    }

    /// <summary>
    /// Test Indexer.
    /// </summary>
    [TestMethod]
    public void Indexer_InvalidNameCell_ThrowInvalidNameException()
    {
        Assert.ThrowsException<InvalidNameException>(() => sheet["1A"]);
    }

    /// <summary>
    /// Test detecting circular dependencies.
    /// </summary>
    [TestMethod]
    public void SetContentsOfCell_CircularDependency_ThrowsCircularException()
    {
        sheet.SetContentsOfCell("A1", "=B1 + 5");
        sheet.SetContentsOfCell("B1", "=C1 + 2");

        Assert.ThrowsException<CircularException>(() => sheet.SetContentsOfCell("C1", "=A1 * 3"));
    }

    /// <summary>
    /// Test the "Changed" state of the spreadsheet after setting content, saving, and modifying content.
    /// </summary>
    [TestMethod]
    public void Spreadsheet_ChangedState_AfterModifications_ReflectsCorrectState()
    {
        Assert.IsFalse(sheet.Changed);

        sheet.SetContentsOfCell("A1", "5");
        Assert.IsTrue(sheet.Changed);

        sheet.Save(ValidFileName);
        Assert.IsFalse(sheet.Changed);

        sheet.SetContentsOfCell("B1", "=A1 + 2");
        Assert.IsTrue(sheet.Changed);
    }

    /// <summary>
    /// Test saving and loading a valid spreadsheet.
    /// </summary>
    [TestMethod]
    public void SaveAndLoad_ValidFile_CorrectlySavesAndLoadsContents()
    {
        // Add data and save
        sheet.SetContentsOfCell("A1", "5");
        sheet.SetContentsOfCell("B1", "hello");
        sheet.SetContentsOfCell("C1", "=5");
        sheet.SetContentsOfCell("D1", "=C1+A1");

        sheet.Save(ValidFileName);

        // Load it back into a new spreadsheet
        Spreadsheet loadedSheet = new Spreadsheet();
        loadedSheet.Load(ValidFileName);

        // Verify the contents
        Assert.AreEqual(5.0, loadedSheet.GetCellContents("A1"));
        Assert.AreEqual("hello", loadedSheet.GetCellContents("B1"));
        Assert.AreEqual(new Formula("5"), loadedSheet.GetCellContents("C1"));
        Assert.AreEqual(new Formula("C1+A1"), loadedSheet.GetCellContents("D1"));
    }

    /// <summary>
    /// Test saving and loading a valid spreadsheet.
    /// </summary>
    [TestMethod]
    public void SaveAndLoad_ValidFile_CorrectlySavesAndLoadsEmptyContents()
    {

        sheet.Save(ValidFileName);

        // Load it back into a new spreadsheet
        Spreadsheet loadedSheet = new Spreadsheet();
        loadedSheet.Load(ValidFileName);
        Assert.AreEqual(0, loadedSheet.GetNamesOfAllNonemptyCells().Count);

    }

    /// <summary>
    /// Test saving to an invalid file path.
    /// </summary>
    [TestMethod]
    public void Save_InvalidFileNamePathNotExist_ThrowsSpreadsheetReadWriteException()
    {
        Assert.ThrowsException<SpreadsheetReadWriteException>(() => sheet.Save(InvalidFileName));
    }

    /// <summary>
    /// Test saving to an invalid file path.
    /// </summary>
    [TestMethod]
    public void Save_InvalidFileNameJustDot_ThrowsSpreadsheetReadWriteException()
    {
        Assert.ThrowsException<SpreadsheetReadWriteException>(() => sheet.Save("."));
    }

    /// <summary>
    /// Test loading an invalid JSON file.
    /// </summary>
    [TestMethod]
    public void Load_InvalidJsonFile_ThrowsSpreadsheetReadWriteException()
    {
        File.WriteAllText(InvalidJsonFileName, "invalid json");

        Assert.ThrowsException<SpreadsheetReadWriteException>(() => sheet.Load(InvalidJsonFileName));
    }

    /// <summary>
    /// Test loading from a non-existent file.
    /// </summary>
    [TestMethod]
    public void Load_NonExistentFile_ThrowsSpreadsheetReadWriteException()
    {
        Assert.ThrowsException<SpreadsheetReadWriteException>(() => sheet.Load("non_existent_file.txt"));
    }

    /// <summary>
    /// Tests the Load method by attempting to load a malformed JSON file.
    /// The test verifies that a SpreadsheetReadWriteException is thrown, indicating
    /// that the file content is invalid and cannot be correctly deserialized.
    /// </summary>
    [TestMethod]
    public void Load_BadJsonFile_ThrowsSpreadsheetReadWriteException()
    {
        // Create a malformed JSON file
        const string malformedJsonFileName = "bad_json.txt";
        string badJsonContent = "{ \"Cells\": { \"A1\": { \"StringForm\": \"=B1 +\" }, "; // Malformed JSON (incomplete structure)

        // Write the malformed JSON content to a file
        File.WriteAllText(malformedJsonFileName, badJsonContent);

        // Attempt to load the malformed JSON file and expect an exception
        Assert.ThrowsException<SpreadsheetReadWriteException>(() => sheet.Load(malformedJsonFileName));

        // Clean up by deleting the temporary file
        if (File.Exists(malformedJsonFileName))
        {
            File.Delete(malformedJsonFileName);
        }
    }

    /// <summary>
    /// Verifies that the file saved by the Spreadsheet.Save method produces the correct JSON structure.
    /// The test sets various types of content (numeric, formula, string) into the spreadsheet, saves it,
    /// and checks the serialized file for expected content including proper Unicode encoding for characters
    /// such as "+" and quotes. The test compares the actual output with the expected JSON format, ensuring 
    /// correct structure, encoding, and consistency across the cells.
    /// </summary>
    [TestMethod]
    public void TestFileContentAfterSave()
    {
        // Arrange
        Spreadsheet sheet = new Spreadsheet();

        // Set content for cells
        //sheet.SetContentsOfCell("A1", "5");               // Numeric value
        //sheet.SetContentsOfCell("B1", "=A1+2");            // Formula with reference to A1
        //sheet.SetContentsOfCell("C1", "=B1+A1");           // Formula with reference to B1 and A1
        //sheet.SetContentsOfCell("D1", "\"A\"");            // String value with quotes

        string filename = "test_save.txt";

        // Act
        sheet.Save(filename);

        // Assert - Check file content
        string fileContent = File.ReadAllText(filename);

        // Expected JSON format with Unicode encoding for special characters
        string expectedJson = @"
    {
        ""Cells"": {
            ""A1"": { ""StringForm"": ""5"" },
            ""B1"": { ""StringForm"": ""=A1\u002B2"" },
            ""C1"": { ""StringForm"": ""=B1\u002BA1"" },
            ""D1"": { ""StringForm"": ""\u0022A\u0022"" }
        }
    }";

        // Ensure both the actual and expected JSON have no excess spaces or line breaks
        Assert.AreEqual(NormalizeJson(expectedJson), NormalizeJson(fileContent));
    }

    /// <summary>
    /// Helper method to normalize JSON for comparison, trimming spaces and line breaks.
    /// </summary>
    private string NormalizeJson(string json)
    {
        return json.Replace("\r", "").Replace("\n", "").Replace(" ", "");
    }
}