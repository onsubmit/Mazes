# Chapter 2

## Binary Tree Maze

![Binary Tree Maze](images/BinaryTree.png?raw=true "Binary Tree Maze")

## Sidewinder Maze

![Sidewinder Maze](images/Sidewinder.png?raw=true "Sidewinder Maze")

# Chapter 3

## Binary Tree Maze with distances from start

```
+---+---+---+---+---+---+---+---+---+---+
| 0   1   2   3   4   5   6   7   8   9 |
+---+---+---+   +   +   +---+---+---+   +
| 7   6   5   4 | 5 | 6 | d   c   b   a |
+---+---+   +   +---+---+   +   +---+   +
| 8   7   6 | 5 | g   f   e | d | c   b |
+   +   +   +---+   +---+   +---+---+   +
| 9 | 8 | 7 | i   h | g   f | e   d   c |
+   +   +---+   +---+---+---+   +---+   +
| a | 9 | k   j | i   h   g   f | e   d |
+---+   +   +   +   +   +   +---+   +   +
| b   a | l | k | j | i | h | g   f | e |
+   +---+   +   +   +---+---+---+   +   +
| c | n   m | l | k | j   i   h   g | f |
+---+   +   +   +---+   +---+   +---+   +
| p   o | n | m | l   k | j   i | h   g |
+   +   +---+---+---+---+---+---+---+   +
| q | p | o   n   m   l   k   j   i   h |
+   +   +---+---+---+---+---+---+---+   +
| r | q | p   o   n   m   l   k   j   i |
+---+---+---+---+---+---+---+---+---+---+
```

## Sidewinder Maze with distances from start

```
+---+---+---+---+---+---+---+---+---+---+
| 0   1   2   3   4   5   6   7   8   9 |
+---+---+---+---+   +---+   +   +---+   +
| 9   8   7   6   5   6 | 7 | 8   9 | a |
+   +---+---+---+---+---+   +---+---+   +
| a | d   c   b   a   9   8   9 | c   b |
+   +---+   +---+   +   +---+   +   +---+
| b   c | d | c   b | a   b | a | d   e |
+   +   +---+   +---+---+---+   +   +   +
| c | d | e   d   e | d   c   b | e | f |
+---+   +   +   +   +   +---+---+---+   +
| f   e | f | e | f | e   f   g   h | g |
+---+   +---+---+   +   +---+   +   +---+
| g   f | i   h   g | f | i   h | i   j |
+   +   +   +---+---+   +   +   +   +   +
| h | g | j   k   l | g | j | i | j | k |
+   +   +   +   +   +---+   +---+   +   +
| i | h | k | l | m   n | k | l   k | l |
+---+   +---+---+   +   +---+---+---+   +
| j   i   j   k | n | o   p   q   r | m |
+---+---+---+---+---+---+---+---+---+---+
```
