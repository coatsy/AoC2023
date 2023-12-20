# Day 7 - Camel Poker

This [Day07.linq](Day07.linq) file is part of the [2023 Advent of Code](https://adventofcode.com/2023) challenge and primarily evaluates different sets of playing card hands according to defined rules in a game. Here's the breakdown of what this file does:

* The `Main` method specifies the location of an input text file which contains data about different hands in a card game. The method reads this data and stores each hand in the Hands list.
* Each hand (`Hand`) is represented by a string of cards and a bid. It is defined by a custom class named `Hand`, which also includes methods to derive `HandType` and `HandTypeWithJokers` (based on the number and types of cards in the hand).
* The `HandType` is evaluated according to conventional poker hand rules, such as `FiveOfAKind`, `FourOfAKind`, `FullHouse`, and so on. The `HandTypeWithJokers` property accounts for hands with Jokers and how they affect the hand type.
* After storing all the hands, the `Main` method sorts these hands first normally and then with consideration for Jokers using `HandComparer` and `HandWithJokersComparer` respectively. These custom comparers establish how to compare two hands according to the type of hand and, in the case of a tie, the value of individual cards.
* The sorted hands are indexed and wrapped in a `RankedHand` object, which includes the Hand itself and the strength of the hand, which is just the index of the hand in the sorted list plus 1.
* Finally, the `Main` method calculates and outputs the total winnings, which is the sum of each hand's bid times its strength, for the sorted lists of hands (considering hands without and with Jokers separately).

Therefore, the file primarily reads, sorts, and analyzes card hands based on different sets of rules while also considering the bid associated with each hand to eventually calculate total winnings.

*Generated using GitHub Copilot*
