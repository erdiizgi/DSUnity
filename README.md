# Welcome to DSUnity Project!

DSunity is an open source project. It contains several data structures for game development for Unity in C#. 
Currently, I am the only developer here and I am writing on [erdiizgi.com](https://erdiizgi.com/) about game development with Unity3D game engine and C#.

## Data Structures

1. Fibonacci Heap
2. [Priority Queue](https://erdiizgi.com/data-structure-for-games-priority-queue-for-unity-in-c/)
3. [BiDictionary](https://erdiizgi.com/data-structures-for-games:bidirectional-dictionary-for-unity-in-c#)

There will be more data structures in time.

## How to contribute
There are three different branches. 
1. **development**: This is the branch I use for development and it can contain more than the released version. It contains the unit tests.
2. **master**: This is the branch for released version. It also contains the unit tests.
3. **upm**: This contains the same information as master branch, however the folder structure of this branch adjusted to use with Unity Package Manager. Releases are done over this branch and it doesn't contain the unit tests.

### Which branch to use

In case you would like to contribute to the project, please make a pull request to the **development** branch. I will manage the rest. 

### Pushing a new Data Structure?
If you developed a new data structure, you shouldn't forget to add these
* Interface with code documentation
* Clean implementation of the interface
* Unit tests for each method in the interface and for the public static methods in the implementation

### Fixing a bug?

If the code you would like to add to the project is a fix for a bug;
* Don't forget to add a unit test for the case you are fixing
* If there is a issue, please put the issue link in the commit message

## Todos
* ~~Create guidelines for the contribution~~
* Add unit tests for every data structure and enhance the existed ones
* Create a documentation page
* Refactor the code so it will be more readable
