# Shroomworld
# Design
## General Design Principles
1. I have tried to separate data from interfaces as much as possible.
2. In general, each game object contains information about itself relating to its context (i.e. the world it resides in), as well as a reference to its type, which contains all the data that remains the same for all objects of that type in all worlds.
3. I have tried as much as possible to store all data in files. The only exception to this is file paths.
4. I have gone with an OOP design, with some ECS-style elements (such as using component classes to store data about game objects).
5. I have mostly opted for composition and interfaces over inheritance, except for a few small exceptions (see `QuestItem` classes).
6. I have used standard `C#` naming conventions.

## File Design
1. I use the CSV file format to store data. Sometimes I have a variable which consists of multiple components, so I have designated two other symbols to work as variable-separators, as follows:

  |Nesting Level|Symbol|Symbol as word|
  |---|---|---|
  |1|,|comma|
  |2|;|semi-colon|
  |3|:|colon|
2. I have chosen not to encrypt the data because I want to be able to edit it with ease and it's not confidential.

## Commits
I try to stick to [these guidelines]().

Unfortunately, as this is my first project using GitHub, my earlier commits are not great, however, my newer commits should be much better as I am constantly learning and improving.

## Additional Links and References
[Prototypes](https://github.com/wero-wal/shroomworld-prototypes)
