# Universal Badge

Welcome to Paris, buzzing with excitement ahead of the Olympic Games! The pre-Olympic banquet, organized to celebrate the imminent opening of this global event, has turned into a disaster: a sudden food poisoning has confined all the officials to bed, unable to continue the preparations. All... except for you, the intern.

You were only supposed to sweep the floor and make coffee, but now you’ve been catapulted into leading the operations. It's now up to you to save the honor of the City of Light.

First challenge: You must find the universal badge to gain access to all the competition venues. The only information you have is that it’s the only badge that starts with `42` and whose digits sum up to exactly `75`.

## Data

### Input

**Line 1:** an integer `N`, the number of badges (1 <= `N` <= 100).

**The next `N` lines:** an integer `X`<sub>`i`</sub>, the badge number `i`.

The badge numbers have between 1 and 25 digits.

### Output

The universal badge number.

## Example 1

For the input:

```
3
1234567890123456
4245555555555555
1212121212121212
```

The universal badge number is:

```
4245555555555555
```

**Explanation:**

It’s the badge that starts with `42` and whose digits sum up to `75`.

## Example 2

For the input:

```
4
0123
42345678910
4299999996
667
```

The universal badge number is:

```
4299999996
```

**Explanation:**

The second badge also starts with `42`, but the sum of its digits does not equal `75`.
