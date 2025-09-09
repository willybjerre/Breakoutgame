namespace Breakout.States;

using Breakout.States;
using DIKUArcade.GUI;
using DIKUArcade.Input;

public enum GameState {
    MainMenu,
    GameRunning,
    GamePaused,
    LifeLost,
    GameLost,
    GameWon

}

public class StateMachine {
    public IGameState? PreviousState {
        get; private set;
    }
    private IGameState activeState;



    private MainMenu mainMenu;
    private GameRunning gameRunning;
    private GamePaused gamePaused;
    private LifeLost lifeLost;
    private GameLost? gameLost;
    private GameWon? gameWon;




    public IGameState ActiveState {
        get => activeState;
        set {
            activeState = value;
            PreviousState = activeState;

        }
    }

    public StateMachine() {
        mainMenu = new MainMenu(this);
        gameRunning = new GameRunning(this);
        gamePaused = new GamePaused(this);
        lifeLost = new LifeLost(this);
        gameLost = null;
        gameWon = null;

        activeState = mainMenu;

    }

    public void SwitchState(GameState newState) {
        switch (newState) {
            case GameState.MainMenu:
                ActiveState = mainMenu;
                break;
            case GameState.GameRunning:
                if (ActiveState == mainMenu || ActiveState == gameLost) {
                    gameRunning = new GameRunning(this);
                }
                ActiveState = gameRunning;
                break;
            case GameState.GamePaused:
                ActiveState = gamePaused;
                break;
            case GameState.LifeLost:
                ActiveState = lifeLost;
                break;
            case GameState.GameLost:
                gameLost = new GameLost(this);
                ActiveState = gameLost;
                break;
            case GameState.GameWon:
                gameWon = new GameWon(this);
                ActiveState = gameWon;
                break;
        }
    }
}