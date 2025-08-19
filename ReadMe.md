# MartianRobots

A .NET solution for simulating robot movements on Mars with collision detection and scent-based navigation safety.


## Development & Explanation


If I had more time, I would refactor some of the code for better maintainability and readability. 
Additional validation checks could be implemented to ensure robustness. 

On the hindsight, implementing the <c>IInstruction</c> interface using the Factory pattern would improve extensibility and decoupling and would allow to add more instructions without needing to change `RobotControllerService`.

Furthermore, with more time, I would have created a more interactive console application or UI to allow users to enter multi-line commands.

### Project structure and rationale

The project is organized to separate responsibilities into small, focused components (instructions, controller/service, and tests). This structure was chosen to make the code easier to understand, extend, and verify.

Benefits:
- Separation of concerns: Controllers orchestrate behavior while each `IInstruction` implementation encapsulates a single action.
- Extensibility: Adding a new instruction requires a new implementation and a registration step (or a factory), avoiding large changes to core logic.
- Testability: Small, focused classes and interfaces are simple to unit-test in isolation.
- Maintainability: Clear boundaries and concise classes reduce cognitive load and make refactors safer.
- Readability & onboarding: Well-named interfaces and small files help new contributors understand the design quickly.

Trade-off: Slightly more boilerplate upfront, but it pays off as the codebase grows and requirements change.

### Adding New Instructions / Commands

1. Create a new class implementing `IInstruction`
2. Add the instruction character mapping in `RobotControllerService`
3. Add corresponding tests

## Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or later
- A terminal/command prompt
- Git (for cloning the repository)

## Installation & Setup

1. **Clone the repository:**
   ```bash
   git clone https://github.com/shahsayyed/MartianRobots.git
   cd MartianRobots
   ```

2. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

3. **Build the solution:**
   ```bash
   dotnet build
   ```

## Running the Application

### Console Application

Run the console application to interact with the robot simulator:

```bash
dotnet run --project MartianRobots.Console
```

### Input Format

The application expects input in the following format:

```
<grid_width> <grid_height>
<robot1_x> <robot1_y> <robot1_orientation>
<robot1_instructions>
<robot2_x> <robot2_y> <robot2_orientation>
<robot2_instructions>
...
```

**Example Input:**
```
5 3
1 1 E
RFRFRFRF
3 2 N
FRRFLLFFRRFLL
0 3 W
LLFFFFLFLFL
```

**Example Output:**
```
1 1 E
3 3 N LOST
2 3 S
```

### Valid Instructions

- `L` - Turn left 90 degrees
- `R` - Turn right 90 degrees  
- `F` - Move forward one grid position

### Valid Orientations

- `N` - North
- `S` - South
- `E` - East
- `W` - West

## Testing

### Run All Tests

```bash
dotnet test
```

### Run Specific Test Project

```bash
dotnet test MartianRobots.Tests
```

### Test Coverage

The test suite includes:
- Happy path scenarios with multiple robots
- Complex movement patterns
- Scent behavior verification
- Input validation and error handling
- Boundary condition testing
