# SuperZizi: Fly and Survive (Unity 2D)

## Overview

Welcome to **SuperZizi: Fly and Survive**, a thrilling Unity 2D adventure where you control a character navigating through a series of obstacles while collecting hearts to survive as long as possible. The game features a beautifully designed parallax background to enhance the immersive experience.

## Game Controls

- **W or Arrow Up**: Move the character upwards.
- **S or Arrow Down**: Move the character downwards.
- **Objective**: Avoid obstacles and collect hearts to keep flying as long as possible.

## Hearts System

- The player starts with **3 hearts**.
- **Hitting an obstacle**: Results in losing one heart.
- **Collecting a heart**: Increases the heart count by one, but not exceeding the maximum of 3 hearts.

## Parallax Background

The game features a dynamic parallax background that adds depth and a visually appealing environment, making the gameplay more engaging.

## Stress Test and Parallelization

To demonstrate the game's performance under heavy load, we've included a **stress test** feature:

1. **Disabling the 2D Collider on the Player**: Before running the stress test, ensure that the 2D Collider on the player is disabled.
2. **Running the Stress Test Script**: The stress test script will simulate a large number of obstacles to measure the game's performance.

### Unity Jobs System

The game includes an option to enable or disable the Unity Jobs system in the Unity Editor:

- **Enabling Unity Jobs**: Allows for parallelization, taking advantage of multithreading to improve performance, especially with a large number of obstacles.
- **Disabling Unity Jobs**: Runs the obstacle movement and other processes serially, which may cause a performance drop as the number of objects increases.

This comparison helps to illustrate the significant performance improvements that multithreading can provide in a game with numerous active objects.

## Conclusion

**SuperZizi: Fly and Survive** combines engaging gameplay with technical demonstrations, offering both entertainment and insights into optimizing game performance through parallelization. Enjoy flying through obstacles, collecting hearts, and experimenting with the stress test to see how well the game performs under pressure!

---
**Note:** This is a Unity 2D game project. The stress test and Unity Jobs system features are included to demonstrate the impact of parallelization on game performance.

Enjoy the game and feel free to contribute to the project!
