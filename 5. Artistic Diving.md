# Artistic Diving

The artistic diving event starts in five minutes, and one of the athletes is missing. You have no choice but to step in.

Fortunately, with your experience from winning first place (out of one participant) in the neighborhood championships, you remember a few moves, and now you just need to decide which ones to perform.

You have already calculated the time your dive will take. For each move you know how to perform, you know the time required to execute the move and the number of points it will earn you.

But beware, originality and diversity of moves are also taken into account. Thus, penalties are applied when a move is used multiple times: For the first repetition of a move, 1 point is subtracted. If the same move is repeated a third time, 2 **additional** points are subtracted (so a **total** of 3 points are deducted for that move). Another repetition will result in 3 **additional** points being deducted (so 6 points in **total** for that move), and so on...

Penalties are independent for each move, meaning if another move is repeated twice, a penalty of 1 point is applied for that move (but not 4 points).

What is the maximum score you can achieve?

## Data

### Input

**Line 1:** an integer `T`, the number of seconds your dive will last (1 <= `T` <= 100).

**Line 2:** an integer `N`, the number of moves you can perform (1 <= `N` <= 1000).

**The next `N` lines:** two integers separated by a space: `S`<sub>`i`</sub> the number of seconds required to perform the move `i` (1 <= `S`<sub>`i`</sub> <= `T`) and `P`<sub>`i`</sub> the number of points the move will earn you (1 <= `P`<sub>`i`</sub> <= 1000).

### Output

An integer: the maximum score you can achieve.

## Example 1

For the input:

```
10
5
7 5
5 3
2 3
1 1
3 2
```

The maximum score you can achieve is:

```
9
```

**Explanation:**

You should perform the second move once for 3 points (using 5 seconds), the third move twice for 3 then 2 points (using 2x2 seconds), and the fourth move once for 1 point (using 1 second).

## Example 2

For the input:

```
20
7
2 6
5 6
3 2
1 1
2 5
8 8
10 9
```

The maximum score is:

```
37
```

**Explanation:**

You should perform the first move four times for 6, 5, 4, and 3 points (using 4x2 seconds), the second move once for 6 points (using 5 seconds), the fourth move once for 1 point (using 1 second), and the fifth move three times for 5, 4, and 3 points (using 3x2 seconds).
