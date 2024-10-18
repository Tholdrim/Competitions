# Medal Table

The Games can finally begin, but one question is on everyone's mind: Which country is currently leading the medal standings?

You know the number of medals each country has, and your goal is to determine which one is in the first place.

The ranking is based on the number of gold medals, followed by silver, and then bronze.

**There will never be a tie for first place.**

## Data

### Input

**Line 1:** an integer `N`, the number of countries (1 <= `N` <= 100).

**The next `N` lines:** each contains, separated by spaces: `C` the name of the country, `G` the number of gold medals, `S` the number of silver medals, and `B` the number of bronze medals.

The country name does not contain spaces and is between 1 and 30 characters in length. The medal counts are integers between 0 and 1000.

### Output

The name of the country at the top of the medal standings.

## Example 1

For the input:

```
5
Italie 12 13 15
Japon 20 12 13
Republique-de-Coree 13 9 10
Allemagne 12 13 8
France 16 26 22
```

The country at the top of the standings is:

```
Japon
```

**Explanation:**

Japan has the most gold medals.

## Example 2

For the input:

```
7
Singapour 2 1 0
Iran 8 10 7
Ouzbekistan 10 9 7
Tunisie 5 3 3
Suisse 8 8 5
Canada 10 9 10
Ethiopie 2 1 0
```

The country at the top of the standings is:
```
Canada
```
**Explanation:**

Canada and Uzbekistan have the most gold medals and the same number of silver medals, but Canada has more bronze medals, so it ranks higher.
