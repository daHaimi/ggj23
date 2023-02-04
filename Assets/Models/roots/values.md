## values of roots (or the roots behind the roots)

| root      | fuel value (max 100) | health value (max 100) | price (points) | spawn factor  |
|-----------|----------------------|------------------------|----------------|---------------|
| caroot    | 5                    | 5                      | 10             | 45%           |
| redroot   | 10                   | 7                      | 20             | 20%           |
| greenroot | 5                    | 10                     | 20             | 15%           |
| iceroot   | -5                   | -5                     | 50             | 10%           |
| rootato   | 10                   | 15                     | 30             | 10%           |

We start with removing 1 point of health and fuel per second.\
There is a difficulty factor (starting at 1) it might increase over time or by some other mechanic.
The difficulty factor also increases the speed of the train.

Ideas for difficulty progression:
  * add 0.25 points every minute
  * add 0.05 points for every root picked (or "consumed")

We need to give the user some feedback when he drops of a carrot and it had a negative effect.\
Simple but effective (for a mvp) would be popping up numbers (like damage values in some games)

The game always ends because it becomes too hard to sustain the fuel/food needs fast enough.