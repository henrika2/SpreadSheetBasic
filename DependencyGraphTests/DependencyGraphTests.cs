// <copyright file="FormulaSyntaxTests.cs" company="UofU-CS3500">
//   Copyright (c) 2024 UofU-CS3500. All rights reserved.
// </copyright>
// <authors>LAN QUANG HUYNH</authors>
// <date>11/03/2024</date>

namespace CS3500.DevelopmentTests;

using CS3500.DependencyGraph;

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
///   This is a test class for DependencyGraphTest and is intended
///   to contain all DependencyGraphTest Unit Tests
/// </summary>
[TestClass]
public class DependencyGraphTests
{
    /// <summary>
    ///   Stress test to validate the performance and correctness of the DependencyGraph class under heavy loads.
    ///   
    ///   This test performs the following actions:
    ///   - Creates a large number of nodes (200) and adds dependencies between them.
    ///   - It adds dependencies in a systematic way, where each node depends on all subsequent nodes.
    ///   - Then it removes some dependencies, re-adds a subset of them, and removes more dependencies in a staggered pattern.
    ///   - Finally, it checks the correctness of the DependencyGraph by verifying that the dependents and dependees for each node
    ///     match the expected results stored in arrays.
    /// 
    ///   The test ensures that the DependencyGraph handles adding and removing dependencies efficiently and 
    ///   maintains correct relationships between nodes even under complex operations. It also verifies that the 
    ///   `GetDependents` and `GetDependees` methods return accurate results after multiple updates.
    /// 
    ///   A timeout of 2 seconds is imposed to check the graph's performance under stress conditions.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]  // 2 second run time limit <-- remove this comment
    public void StressTest()
    {
        DependencyGraph dg = new();

        // A bunch of strings to use
        const int SIZE = 200;
        string[] letters = new string[SIZE];
        for (int i = 0; i < SIZE; i++)
        {
            letters[i] = string.Empty + ((char)('a' + i));
        }

        // The correct answers
        HashSet<string>[] dependents = new HashSet<string>[SIZE];
        HashSet<string>[] dependees = new HashSet<string>[SIZE];
        for (int i = 0; i < SIZE; i++)
        {
            dependents[i] = [];
            dependees[i] = [];
        }

        // Add a bunch of dependencies
        for (int i = 0; i < SIZE; i++)
        {
            for (int j = i + 1; j < SIZE; j++)
            {
                dg.AddDependency(letters[i], letters[j]);
                dependents[i].Add(letters[j]);
                dependees[j].Add(letters[i]);
            }
        }

        // Remove a bunch of dependencies
        for (int i = 0; i < SIZE; i++)
        {
            for (int j = i + 4; j < SIZE; j += 4)
            {
                dg.RemoveDependency(letters[i], letters[j]);
                dependents[i].Remove(letters[j]);
                dependees[j].Remove(letters[i]);
            }
        }

        // Add some back
        for (int i = 0; i < SIZE; i++)
        {
            for (int j = i + 1; j < SIZE; j += 2)
            {
                dg.AddDependency(letters[i], letters[j]);
                dependents[i].Add(letters[j]);
                dependees[j].Add(letters[i]);
            }
        }

        // Remove some more
        for (int i = 0; i < SIZE; i += 2)
        {
            for (int j = i + 3; j < SIZE; j += 3)
            {
                dg.RemoveDependency(letters[i], letters[j]);
                dependents[i].Remove(letters[j]);
                dependees[j].Remove(letters[i]);
            }
        }

        // Make sure everything is right
        for (int i = 0; i < SIZE; i++)
        {
            Assert.IsTrue(dependents[i].SetEquals(new HashSet<string>(dg.GetDependents(letters[i]))));
            Assert.IsTrue(dependees[i].SetEquals(new HashSet<string>(dg.GetDependees(letters[i]))));
        }
    }


    /// <summary>
    /// Tests that a newly created DependencyGraph has size 0.
    /// </summary>
    [TestMethod]
    public void TestEmptyGraph()
    {
        DependencyGraph dg = new();
        Assert.AreEqual(0, dg.Size);
        Assert.IsFalse(dg.HasDependents("a"));
        Assert.IsFalse(dg.HasDependees("a"));
        Assert.IsFalse(dg.HasDependents("b"));
        Assert.IsFalse(dg.HasDependees("b"));
    }

    /// <summary>
    /// Tests size method by adding 1 and 19900 nodes.
    /// </summary>
    [TestMethod]
    public void TestSizeFucntion()
    {
        DependencyGraph dg = new();
        Assert.AreEqual(0, dg.Size);
        dg.AddDependency("a!", "b@");
        Assert.AreEqual(1, dg.Size);
        const int SIZE = 200;
        string[] letters = new string[SIZE];
        for (int i = 0; i < SIZE; i++)
        {
            letters[i] = string.Empty + ((char)('a' + i));
        }
        for (int i = 0; i < SIZE; i++)
        {
            for (int j = i + 1; j < SIZE; j++)
            {
                dg.AddDependency(letters[i], letters[j]);
            }
        }
        // the it iterates 19900 times
        Assert.AreEqual(19901, dg.Size);
    }

    /// <summary>
    /// Tests the HasDependents method to verify if a node has dependents.
    /// </summary>
    [TestMethod]
    public void TestHasDependents()
    {
        DependencyGraph dg = new();

        // Initially, no nodes should have dependents
        Assert.IsFalse(dg.HasDependents("x"));
        Assert.IsFalse(dg.HasDependents("y"));

        // Add a dependency
        dg.AddDependency("a", "b");

        // Node "a" should have dependents
        Assert.IsTrue(dg.HasDependents("a"));

        // Node "b" should not have dependents
        Assert.IsFalse(dg.HasDependents("b"));

        // Adding more dependencies
        dg.AddDependency("a", "c");

        // Node "a" should still have dependents
        Assert.IsTrue(dg.HasDependents("a"));

        // Node "c" should not have dependents
        Assert.IsFalse(dg.HasDependents("c"));
    }

    /// <summary>
    /// Tests the HasDependees method to verify if a node has dependees.
    /// </summary>
    [TestMethod]
    public void TestHasDependees()
    {
        DependencyGraph dg = new();

        // Initially, no nodes should have dependees
        Assert.IsFalse(dg.HasDependees("x"));
        Assert.IsFalse(dg.HasDependees("y"));

        // Add a dependency
        dg.AddDependency("a", "b");

        // Node "b" should have dependees
        Assert.IsTrue(dg.HasDependees("b"));

        // Node "a" should not have dependees
        Assert.IsFalse(dg.HasDependees("a"));

        // Adding more dependencies
        dg.AddDependency("c", "b");

        // Node "b" should still have dependees
        Assert.IsTrue(dg.HasDependees("b"));

        // Node "c" should not be a dependee
        Assert.IsFalse(dg.HasDependees("c"));
    }

    /// <summary>
    /// Tests the GetDependents method to retrieve the dependents of a node.
    /// </summary>
    [TestMethod]
    public void TestGetDependents()
    {
        DependencyGraph dg = new();

        // Add dependencies
        dg.AddDependency("a", "b");
        dg.AddDependency("a", "c");

        // Get dependents of node "a"
        var dependents = dg.GetDependents("a");
        var expectedDependents = new List<string> { "b", "c" };

        CollectionAssert.AreEquivalent(expectedDependents, new List<string>(dependents));


    }

    /// <summary>
    /// Tests the GetDependents method to retrieve the dependents of a node.
    /// </summary>
    [TestMethod]
    public void TestGetDependentsNoDependent()

    {
        DependencyGraph dg = new();
        dg.AddDependency("a", "b");
        // Get dependents of a node with no dependents
        var dependents = dg.GetDependents("b");
        var expectedDependents = new List<string>();

        CollectionAssert.AreEquivalent(expectedDependents, new List<string>(dependents));
    }


    /// <summary>
    /// Tests the GetDependees method to retrieve the dependees of a node.
    /// </summary>
    [TestMethod]
    public void TestGetDependees()
    {
        DependencyGraph dg = new();

        // Add dependencies
        dg.AddDependency("a", "b");
        dg.AddDependency("c", "b");

        // Get dependees of node "b"
        var dependees = dg.GetDependees("b");
        var expectedDependees = new List<string> { "a", "c" };

        CollectionAssert.AreEquivalent(expectedDependees, new List<string>(dependees));

        
    }

    /// <summary>
    /// Tests the GetDependees method to retrieve the dependees of a node with no denpendees.
    /// </summary>
    [TestMethod]
    public void TestGetDependeesNoDenpendee() {
        DependencyGraph dg = new();
        // Get dependees of a node with no dependees
        var dependees = dg.GetDependees("a");
        var expectedDependees = new List<string>();

        CollectionAssert.AreEquivalent(expectedDependees, new List<string>(dependees));
    } 

    /// <summary>
    /// Tests adding a single dependency to the DependencyGraph.
    /// </summary>
    [TestMethod]
    public void TestAddSingleDependency()
    {
        DependencyGraph dg = new();
        dg.AddDependency("a", "b");
        Assert.AreEqual(1, dg.Size);
        Assert.IsTrue(dg.HasDependents("a"));
        Assert.IsTrue(dg.HasDependees("b"));
        CollectionAssert.AreEquivalent(new List<string> { "b" }, new List<string>(dg.GetDependents("a")));
        CollectionAssert.AreEquivalent(new List<string> { "a" }, new List<string>(dg.GetDependees("b")));
    }

    /// <summary>
    /// Tests that adding the same dependency more than once does not increase size.
    /// </summary>
    [TestMethod]
    public void TestAddDuplicateDependency()
    {
        DependencyGraph dg = new();
        dg.AddDependency("a", "b");
        dg.AddDependency("a", "b");  // Duplicate
        Assert.AreEqual(1, dg.Size);  // Size should still be 1
        CollectionAssert.AreEquivalent(new List<string> { "b" }, new List<string>(dg.GetDependents("a")));
        CollectionAssert.AreEquivalent(new List<string> { "a" }, new List<string>(dg.GetDependees("b")));
    }

    /// <summary>
    /// Tests removing a dependency.
    /// </summary>
    [TestMethod]
    public void TestRemoveDependency()
    {
        DependencyGraph dg = new();
        dg.AddDependency("a", "b");
        dg.RemoveDependency("a", "b");
        Assert.AreEqual(0, dg.Size);
        Assert.IsFalse(dg.HasDependents("a"));
        Assert.IsFalse(dg.HasDependees("b"));
    }

    /// <summary>
    /// Tests removing a non-existent dependee dependency does not change the graph.
    /// </summary>
    [TestMethod]
    public void TestRemoveNonExistentDependeeDependency()
    {
        DependencyGraph dg = new();
        dg.AddDependency("a", "b");
        dg.RemoveDependency("a", "c");  // Non-existent
        Assert.AreEqual(1, dg.Size);
        Assert.IsTrue(dg.HasDependents("a"));
        Assert.IsTrue(dg.HasDependees("b"));
    }

    /// <summary>
    /// Tests removing a non-existent dependent dependency does not change the graph.
    /// </summary>
    [TestMethod]
    public void TestRemoveNonExistentDependentDependency()
    {
        DependencyGraph dg = new();
        dg.AddDependency("a", "b");
        dg.RemoveDependency("c", "b");  // Non-existent
        Assert.AreEqual(1, dg.Size);
        Assert.IsTrue(dg.HasDependents("a"));
        Assert.IsTrue(dg.HasDependees("b"));
    }

    /// <summary>
    /// Tests replacing dependents of a node.
    /// </summary>
    [TestMethod]
    public void TestReplaceDependents()
    {
        DependencyGraph dg = new();
        dg.AddDependency("a", "b");
        dg.AddDependency("a", "c");
        dg.ReplaceDependents("a", new List<string> { "d", "e" });
        Assert.AreEqual(2, dg.Size);
        CollectionAssert.AreEquivalent(new List<string> { "d", "e" }, new List<string>(dg.GetDependents("a")));
        Assert.IsTrue(dg.HasDependents("a"));
        Assert.IsFalse(dg.HasDependees("b"));
        Assert.IsFalse(dg.HasDependees("c"));
    }

    /// <summary>
    /// Tests replacing dependents of a node by an empty list.
    /// </summary>
    [TestMethod]
    public void TestReplaceDependentsByEmptyList()
    {
        DependencyGraph dg = new();
        dg.AddDependency("a", "b");
        dg.AddDependency("a", "c");
        dg.ReplaceDependents("a", new List<string> { });
        Assert.AreEqual(0, dg.Size);
        CollectionAssert.AreEquivalent(new List<string> { }, new List<string>(dg.GetDependents("a")));
        Assert.IsFalse(dg.HasDependents("a"));
        Assert.IsFalse(dg.HasDependees("b"));
        Assert.IsFalse(dg.HasDependees("c"));
    }

    /// <summary>
    /// Tests replacing dependents of a nonexist node .
    /// </summary>
    [TestMethod]
    public void TestReplaceDependentsNonexistNode()
    {
        DependencyGraph dg = new();
        dg.AddDependency("a", "b");
        dg.AddDependency("a", "c");
        dg.ReplaceDependents("d", new List<string> { "e", "f" });
        Assert.AreEqual(4, dg.Size);
        CollectionAssert.AreEquivalent(new List<string> { "e", "f" }, new List<string>(dg.GetDependents("d")));
        Assert.IsTrue(dg.HasDependents("a"));
        Assert.IsTrue(dg.HasDependees("b"));
        Assert.IsTrue(dg.HasDependees("c"));
        Assert.IsTrue(dg.HasDependents("d"));
        Assert.IsTrue(dg.HasDependees("e"));
        Assert.IsTrue(dg.HasDependees("f"));
    }

    /// <summary>
    /// Tests replacing dependees of a node.
    /// </summary>
    [TestMethod]
    public void TestReplaceDependees()
    {
        DependencyGraph dg = new();
        dg.AddDependency("b", "a");
        dg.AddDependency("c", "a");
        dg.ReplaceDependees("a", new List<string> { "d", "e" });
        Assert.AreEqual(2, dg.Size);
        CollectionAssert.AreEquivalent(new List<string> { "d", "e" }, new List<string>(dg.GetDependees("a")));
        Assert.IsTrue(dg.HasDependees("a"));
        Assert.IsFalse(dg.HasDependents("b"));
        Assert.IsFalse(dg.HasDependents("c"));
    }

    /// <summary>
    /// Tests replacing dependees of a node by an empty list.
    /// </summary>
    [TestMethod]
    public void TestReplaceDependeesByEmptyList()
    {
        DependencyGraph dg = new();
        dg.AddDependency("b", "a");
        dg.AddDependency("c", "a");
        dg.ReplaceDependees("a", new List<string> { });
        Assert.AreEqual(0, dg.Size);
        CollectionAssert.AreEquivalent(new List<string> { }, new List<string>(dg.GetDependees("a")));
        Assert.IsFalse(dg.HasDependees("a"));
        Assert.IsFalse(dg.HasDependents("b"));
        Assert.IsFalse(dg.HasDependents("c"));
    }

    /// <summary>
    /// Tests replacing dependees of a nonexist node.
    /// </summary>
    [TestMethod]
    public void TestReplaceDependeesNonexistNode()
    {
        DependencyGraph dg = new();
        dg.AddDependency("b", "a");
        dg.AddDependency("c", "a");
        dg.ReplaceDependees("d", new List<string> { "e", "f" });
        Assert.AreEqual(4, dg.Size);
        CollectionAssert.AreEquivalent(new List<string> {"e","f" }, new List<string>(dg.GetDependees("d")));
        Assert.IsTrue(dg.HasDependees("a"));
        Assert.IsTrue(dg.HasDependents("b"));
        Assert.IsTrue(dg.HasDependents("c"));
        Assert.IsTrue(dg.HasDependees("d"));
        Assert.IsTrue(dg.HasDependents("e"));
        Assert.IsTrue(dg.HasDependents("f"));
    }

}