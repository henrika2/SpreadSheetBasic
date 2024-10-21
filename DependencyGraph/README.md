/*
Author:     LAN QUANG HUYNH
Partner:    None
Course:     CS 3500, University of Utah, School of Computing
GitHub ID:  henrika2
Repo:       https://github.com/uofu-cs3500-20-fall2024/spreadsheet-henrika2
Date:       11-Sep-2024 (when submission was completed)
Project:    DependencyGraph
Copyright:  CS 3500 and [LAN QUANG HUYNH] - This work may not be copied for use in Academic Coursework.
*/

# Comments to Evaluators:

This implementation of the `DependencyGraph` class effectively models a set of ordered pairs that describe dependencies between items. Special care was taken to ensure that the operations on the graph, such as adding and removing dependencies, were optimized for performance given the constraints outlined in the project. Several edge cases were considered, including self-referencing nodes and attempts to add duplicate dependencies.

The code adheres to the specifications provided in the project guidelines. Unit tests were designed to thoroughly test the public methods, ensuring that the behavior of the graph is correct under various circumstances. Additionally, the performance of the graph was tested through a stress test with a large number of nodes and dependencies to ensure scalability.

# Assignment Specific Topics:

- The `AddDependency` method ensures that no duplicate dependencies are added to the graph, maintaining the integrity of the data structure.
- The `RemoveDependency` method carefully handles attempts to remove non-existent dependencies, ensuring no unintended side effects.
- The `GetDependents` and `GetDependees` methods return correct and efficient enumerations of the relationships in the graph.
- A `StressTest` method was implemented to evaluate the performance of the graph under high load.


