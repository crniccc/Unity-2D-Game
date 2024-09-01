
# Super Zizi

## Overview

Welcome to **Super Zizi**, an exciting Unity 2D game where you control a brave hero navigating through challenging obstacles. Your goal is to stay airborne as long as possible by avoiding obstacles and collecting hearts. The game features a stunning parallax background that enhances the immersive experience of guiding Super Zizi through the sky.

## Game Controls

- **W or Arrow Up**: Move upwards.
- **S or Arrow Down**: Move downwards.
- **Objective**: Avoid obstacles and collect hearts to maximize your flying time.

## Hearts System

- The player starts with **3 hearts**.
- **Hitting an obstacle**: Causes you to lose one heart.
- **Collecting a heart**: Increases the heart count by one, up to a maximum of 3 hearts.

## Parallax Background

The game includes a dynamic parallax background that adds depth and visual appeal, enhancing your gameplay experience.

## Stress Test and Parallelization

To showcase the game's performance under heavy load, we’ve included a **stress test** feature:

1. **Disabling the 2D Collider on the Player**: Make sure the 2D Collider on the player is disabled before running the stress test.
2. **Running the Stress Test Script**: This script simulates a large number of obstacles to measure performance.

### Unity Jobs System

The game allows you to enable or disable the Unity Jobs system in the Unity Editor:

- **Enabling Unity Jobs**: Utilizes multithreading to improve performance with many obstacles.
- **Disabling Unity Jobs**: Processes obstacles serially, which may reduce performance under high load.

This feature helps illustrate the performance benefits of parallelization in games with numerous active objects.

## Conclusion

**Super Zizi** combines engaging gameplay with technical demonstrations, offering both fun and insights into optimizing game performance through parallelization. Enjoy flying through obstacles, collecting hearts, and testing the game’s performance!

---
**Note:** This is a Unity 2D game project. The stress test and Unity Jobs system features highlight the impact of parallelization on performance.

Feel free to contribute and enjoy the game!
