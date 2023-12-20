# Day 6 - Boat Races

This [Day06.linq](Day06.linq) file is part of the [2023 Advent of Code](https://adventofcode.com/2023) challenge.

Here's a general overview of its main tasks:

* The `Main` method first defines file paths and fetches the input file containing race data. The specific input file used here is input.txt. It's located in a path constructed from `baseFolder` and `dayFolder`.
* These text files consist of time and distance related to some kind of races. The application reads these Time and Distance strings from the file, manipulates the strings to ensure they do not contain extra whitespaces, and then splits the values into arrays.
* The `Main` method subsequently constructs several `Race` instances from pairs of time and distance values and stores them in the `Races` list.
* A `Race` class represents an individual race. It stores its time and distance as long fields. It also has a property named `Ways2Win` which calculates the potential ways to win the race based ~on some racing rule defined in this property~ the fact that total distance travelled is defined by the formula  
`d = (time available - time pressed) * (time pressed)`  
Which is a quadratic equation and the roots represent the times at which the distance travelled is greater than the record.
* After storing all the races, the `Main` method then calculates the product of ways to win for all the races, which can be interpreted as some kind of combined score or result.

Therefore, the file primarily reads race data from an input file, processes and analyzes the races according to specific rules, and finally computes some output based on the provided data.

*Generated using GitHub Copilot*
