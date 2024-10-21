// <copyright file="DependencyGraph.cs" company="UofU-CS3500">
// Copyright (c) 2024 UofU-CS3500. All rights reserved.
// </copyright>
// <authors>LAN QUANG HUYNH</authors>
// <date>11/04/2024</date>/

// Skeleton implementation written by Joe Zachary for CS 3500, September 2013.
// Version 1.1 (Fixed error in comment for RemoveDependency.)
// Version 1.2 - Daniel Kopta
// Version 1.3 - H. James de St. Germain Fall 2024
// (Clarified meaning of dependent and dependee.)
// (Clarified names in solution/project structure.)
namespace CS3500.DependencyGraph;

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
/// This file contains the implementation of the DependencyGraph class,
/// which models dependencies between objects (represented as strings).
/// The class supports adding, removing, and replacing dependencies between objects,
/// as well as querying dependents and dependees of a particular object.
/// It efficiently tracks these relationships using internal data structures,
/// allowing for operations like dependency lookups and size calculations.
/// This file also includes extensive unit tests to verify the correctness
/// and performance of the class under various scenarios, including stress tests.
/// </summary>

/// <summary>
///   <para>
///     (s1,t1) is an ordered pair of strings, meaning t1 depends on s1.
///     (in other words: s1 must be evaluated before t1.)
///   </para>
///   <para>
///     A DependencyGraph can be modeled as a set of ordered pairs of strings.
///     Two ordered pairs (s1,t1) and (s2,t2) are considered equal if and only
///     if s1 equals s2 and t1 equals t2.
///   </para>
///   <remarks>
///     Recall that sets never contain duplicates.
///     If an attempt is made to add an element to a set, and the element is already
///     in the set, the set remains unchanged.
///   </remarks>
///   <para>
///     Given a DependencyGraph DG:
///   </para>
///   <list type="number">
///     <item>
///       If s is a string, the set of all strings t such that (s,t) is in DG is called dependents(s).
///       (The set of things that depend on s.)
///     </item>
///     <item>
///       If s is a string, the set of all strings t such that (t,s) is in DG is called dependees(s).
///       (The set of things that s depends on.)
///     </item>
///   </list>
///   <para>
///      For example, suppose DG = {("a", "b"), ("a", "c"), ("b", "d"), ("d", "d")}.
///   </para>
///   <code>
///     dependents("a") = {"b", "c"}
///     dependents("b") = {"d"}
///     dependents("c") = {}
///     dependents("d") = {"d"}
///     dependees("a")  = {}
///     dependees("b")  = {"a"}
///     dependees("c")  = {"a"}
///     dependees("d")  = {"b", "d"}
///   </code>
/// </summary>
public class DependencyGraph
{
    /// <summary>
    /// Dictionary to map each dependee (a variable that depends on other variables)
    /// to its associated dependents (variables that rely on this dependee).
    /// </summary>
    private readonly Dictionary<string, HashSet<string>> dependents;

    /// <summary>
    /// Dictionary to map each dependent (a variable that relies on other variables)
    /// to its associated dependees (variables that this dependent is dependent upon).
    /// </summary>
    private readonly Dictionary<string, HashSet<string>> dependees;

    /// <summary>
    /// Tracks the total number of unique dependency pairs in the dependency graph.
    /// This count is updated whenever a new dependency is added or removed.
    /// </summary>
    private int size;

    /// <summary>
    ///   Initializes a new instance of the <see cref="DependencyGraph"/> class.
    ///   The initial DependencyGraph is empty.
    /// </summary>
    public DependencyGraph()
    {
        this.dependents = new ();
        this.dependees = new ();
        this.size = 0;
    }

    /// <summary>
    /// Gets the number of ordered pairs in the DependencyGraph.
    /// </summary>
    public int Size
    {
        get { return this.size; }
    }

    /// <summary>
    ///   Reports whether the given node has dependents (i.e., other nodes depend on it).
    /// </summary>
    /// <param name="nodeName"> The name of the node.</param>
    /// <returns> true if the node has dependents. </returns>
    public bool HasDependents(string nodeName)
    {
        return this.dependents.ContainsKey(nodeName) && this.dependents[nodeName].Count > 0;
    }

    /// <summary>
    ///   Reports whether the given node has dependees (i.e., depends on one or more other nodes).
    /// </summary>
    /// <returns> true if the node has dependees.</returns>
    /// <param name="nodeName">The name of the node.</param>
    public bool HasDependees(string nodeName)
    {
        return this.dependees.ContainsKey(nodeName) && this.dependees[nodeName].Count > 0;
    }

    /// <summary>
    ///   <para>
    ///     Returns the dependents of the node with the given name.
    ///   </para>
    /// </summary>
    /// <param name="nodeName"> The node we are looking at.</param>
    /// <returns> The dependents of nodeName. </returns>
    public IEnumerable<string> GetDependents(string nodeName)
    {
        if (this.HasDependents(nodeName))
        {
            return new List<string>(this.dependents[nodeName]);
        }

        return new List<string>();
    }

    /// <summary>
    ///   <para>
    ///     Returns the dependees of the node with the given name.
    ///   </para>
    /// </summary>
    /// <param name="nodeName"> The node we are looking at.</param>
    /// <returns> The dependees of nodeName. </returns>
    public IEnumerable<string> GetDependees(string nodeName)
    {
        if (this.HasDependees(nodeName))
        {
            return new List<string>(this.dependees[nodeName]);
        }

        return new List<string>();
    }

    /// <summary>
    /// <para>
    ///   Adds the ordered pair (dependee, dependent), if it doesn't already exist (otherwise nothing happens).
    /// </para>
    /// <para>
    ///   This can be thought of as: dependee must be evaluated before dependent.
    /// </para>
    /// </summary>
    /// <param name="dependee"> The name of the node that must be evaluated first. </param>
    /// <param name="dependent"> The name of the node that cannot be evaluated until after the other node has been. </param>
    public void AddDependency(string dependee, string dependent)
    {
        // Add dependent to the dependee's dependents list
        if (!this.dependents.ContainsKey(dependee))
        {
            this.dependents[dependee] = new HashSet<string>();
        }

        if (this.dependents[dependee].Add(dependent))
        {
            this.size++; // Increment size only if a new dependency is added
        }

        // Add dependee to the dependent's dependees list
        if (!this.dependees.ContainsKey(dependent))
        {
            this.dependees[dependent] = new HashSet<string>();
        }

        this.dependees[dependent].Add(dependee);
    }

    /// <summary>
    ///   <para>
    ///     Removes the ordered pair (dependee, dependent), if it exists (otherwise nothing happens).
    ///   </para>
    /// </summary>
    /// <param name="dependee"> The name of the node that must be evaluated first. </param>
    /// <param name="dependent"> The name of the node that cannot be evaluated until the other node has been. </param>
    public void RemoveDependency(string dependee, string dependent)
    {
        // Remove dependent from the dependee's dependents list
        if (this.dependents.ContainsKey(dependee) && this.dependents[dependee].Remove(dependent))
        {
            this.size--; // Decrement size only if a dependency is removed
        }

        // Remove dependee from the dependent's dependees list
        if (this.dependees.ContainsKey(dependent))
        {
            this.dependees[dependent].Remove(dependee);
        }
    }

    /// <summary>
    ///   Removes all existing ordered pairs of the form (nodeName, *).  Then, for each
    ///   t in newDependents, adds the ordered pair (nodeName, t).
    /// </summary>
    /// <param name="nodeName"> The name of the node who's dependents are being replaced. </param>
    /// <param name="newDependents"> The new dependents for nodeName. </param>
    public void ReplaceDependents(string nodeName, IEnumerable<string> newDependents)
    {
        // Remove all existing dependents
        if (this.dependents.ContainsKey(nodeName))
        {
            foreach (var dependent in this.dependents[nodeName])
            {
                this.RemoveDependency(nodeName, dependent);
            }

            this.dependents[nodeName].Clear();
        }

        // Add new dependents
        foreach (var dependent in newDependents)
        {
            this.AddDependency(nodeName, dependent);
        }
    }

    /// <summary>
    ///   <para>
    ///     Removes all existing ordered pairs of the form (*, nodeName).  Then, for each
    ///     t in newDependees, adds the ordered pair (t, nodeName).
    ///   </para>
    /// </summary>
    /// <param name="nodeName"> The name of the node who's dependees are being replaced. </param>
    /// <param name="newDependees"> The new dependees for nodeName. Could be empty.</param>
    public void ReplaceDependees(string nodeName, IEnumerable<string> newDependees)
    {
        // Remove all existing dependees
        if (this.dependees.ContainsKey(nodeName))
        {
            foreach (var dependee in this.dependees[nodeName])
            {
                this.RemoveDependency(dependee, nodeName);
            }

            this.dependees[nodeName].Clear();
        }

        // Add new dependees
        foreach (var dependee in newDependees)
        {
            this.AddDependency(dependee, nodeName);
        }
    }
}
