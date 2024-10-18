# Climbing

At the climbing site, the route setters didn't finish the final route, and now it's up to you to complete it.

The wall has a starting hold, a finishing hold, and numerous other holds in between.

The athletes will climb as efficiently as possible, aiming to minimize the number of holds they use. Any unnecessary holds can be removed.

Your goal is to find which holds to keep, forming the route that allows athletes to climb the wall using the fewest holds.

There is one constraint: the climber’s reach. We assume the climber can only grab holds that are within a certain distance, equal to or less than their reach, from their current hold. Also, the climber can only grab one hold at a time.

## Data

### Input

All distances and coordinates are in centimeters.

**Line 1:** an integer `S`, the climber's reach (150 <= `S` <= 200).

**Line 2:** two integers `X`<sub>`s`</sub> and `Y`<sub>`s`</sub> (separated by a space), the coordinates of the starting hold.

**Line 3:** two integers `X`<sub>`f`</sub> and `Y`<sub>`f`</sub> (separated by a space), the coordinates of the finishing hold.

**Line 4:** an integer `N`, the number of other holds on the wall (1 <= `N` <= 500).

**The next `N` lines:** two integers `X`<sub>`i`</sub> and `Y`<sub>`i`</sub> (separated by a space), the coordinates of the `i`<sup>th</sup> hold.

The coordinates of the holds are between 0 and 5000 inclusive.

### Output

The coordinates of the holds that form the route allowing the climber to ascend the wall using the fewest holds, or `-1` if the climber’s reach doesn't allow them to complete the route.

Print one hold per line, in the order of the route (including the starting and finishing holds), with the coordinates separated by a space

## Example 1

For the input:

```
175
500 100
500 1000
22
390 187
297 305
297 480
314 622
351 767
404 909
504 250
555 375
602 503
592 608
501 647
472 777
537 879
625 198
700 267
800 333
849 499
830 601
840 690
800 800
750 850
640 950
```

The shortest route is:

```
500 100
390 187
297 305
297 480
314 622
351 767
404 909
500 1000
```

**Explanation:**

Here’s what the wall looks like:

<p align="center">
  <img src="Wall 01.png?raw=true" alt="Wall hold layout for example 1" />
</p>

And here’s the shortest route:

<p align="center">
  <img src="Solution 01.png?raw=true" alt="Shortest route for example 1" />
</p>

## Example 2

For the input:

```
175
500 100
500 1000
16
603 215
350 666
555 627
546 350
430 341
656 385
475 525
425 945
450 850
599 513
500 775
357 357
350 500
406 201
435 756
465 675
```

A shortest route is:

```
500 100
603 215
546 350
599 513
555 627
500 775
450 850
500 1000
```

Another valid solution would be:

```
500 100
406 201
357 357
350 500
350 666
435 756
450 850
500 1000
```

**Explanation:**

Here’s how the holds are laid out:

<p align="center">
  <img src="Wall 02.png?raw=true" alt="Wall hold layout for example 2" />
</p>

This wall has two shortest routes:

<p align="center">
  <img src="Solution 02.png?raw=true" alt="Shortest routes for example 2 " />
</p>
