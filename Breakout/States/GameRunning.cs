namespace Breakout.States;

using System.Collections.Generic;
using System.Linq.Expressions;
using System.Numerics;
using Breakout;
using Breakout.Effects;
using Breakout.GameEvents;
using Collision;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Physics;
using DIKUArcade.Timers;
using LevelHandler;

public class GameRunning : IGameState {

    public static GameRunning Instance { get; private set; } = null!;
    private EntityContainer<Effects.Effect> effects;

    private StateMachine stateMachine;

    private EntityContainer<Blocks.Block> BreakableBlocks = new EntityContainer<Blocks.Block>();

    private EntityContainer<Blocks.Block> UnBreakableBlocks = new EntityContainer<Blocks.Block>();

    private Player player;

    private int ActiveLevel = 0;

    private Levels GameLevels = new Levels();

    private ScoreBoard scoreBoard;

    private EntityContainer<Ball> PlayerBalls;

    private Vector2 pos;
    private HealthBoard healthBoard;



    public GameRunning(StateMachine stateMachine) {
        BreakoutBus.Instance.Subscribe<ResetPlayerSpeedEvent>(ResetPlayerSpeed);
        BreakoutBus.Instance.Subscribe<ResetBallSpeedEvent>(ResetBallSpeed);
        BreakoutBus.Instance.Subscribe<AddEffectEvent>(AddEffect);
        this.stateMachine = stateMachine;
        Instance = this;
        effects = new EntityContainer<Effects.Effect>();


        PlayerBalls = new EntityContainer<Ball>();

        LoadLevel();

        player = new Player(
            new DynamicShape(new Vector2(0.40f, 0.08f),
                            new Vector2(0.175f, 0.03f)),
            new Image("Breakout.Assets.Images.player.png"));




        pos = player.GetPosition();

        PlayerBalls.AddEntity(new Ball(
            new DynamicShape(pos + new Vector2(-0.0145f, 0.03f), new Vector2(0.03f, 0.03f)),
            new Image("Breakout.Assets.Images.ball.png")));

        scoreBoard = new ScoreBoard();
        healthBoard = new HealthBoard();


    }
    ~GameRunning() {
        BreakoutBus.Instance.Unsubscribe<AddEffectEvent>(AddEffect);
    }


    public void Update() {
        BreakoutBus.Instance.ProcessEvents();
        player.Move();
        LevelChecker();
        GameLost();
        IterateBalls();
        healthBoard.UpdateHearts();
        IterateEffect();


    }

    public void Render(WindowContext context) {
        UnBreakableBlocks.RenderEntities(context);
        BreakableBlocks.RenderEntities(context);
        player.RenderEntity(context);
        PlayerBalls.RenderEntities(context);
        healthBoard.DisplayHearts().RenderEntities(context);
        effects.RenderEntities(context);

        foreach (var option in scoreBoard.DisplayScore()) {
            option.Render(context);
        }


    }
    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {

        player.KeyHandler(action, key);
        PlayerBalls.Iterate(ball => {
            ball.KeyHandler(action, key);
        });

        if (action == KeyboardAction.KeyPress) {
            switch (key) {
                case KeyboardKey.Escape:
                    stateMachine.SwitchState(GameState.GamePaused);
                    break;
            }
        }
    }

    private void IterateBalls() {

        NoBalls();

        PlayerBalls.Iterate(ball => {
            ball.Move();

            WallCollision.HandleKollisionWithWall(ball);

            BreakableBlocks.Iterate(block => {
                if (CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape.AsDynamicShape()).Collision) {
                    BlockCollision.HandleKollisionWithBlock(ball, block);
                    scoreBoard.AddScore(block);

                }
            });
            if (CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), player.Shape.AsDynamicShape()).Collision) {
                PlayerCollision.HandleKollisionWithPlayer(ball, player);
            }
            UnBreakableBlocks.Iterate(block => {
                if (CollisionDetection.Aabb(ball.Shape.AsDynamicShape(), block.Shape.AsDynamicShape()).Collision) {
                    BlockCollision.HandleKollisionWithBlock(ball, block);
                    scoreBoard.AddScore(block);

                }
            });
        }
        );
    }

    private void IterateEffect() {
        effects.Iterate(effect => {
            effect.Move();

            PlayerEffectCollision.HandleKollisionWithEffect(effect, player, PlayerBalls, healthBoard);

            EffectBottomCollision.HandleKollisionWithBottom(effect);
        });
    }
    private void LoadLevel() {
        string LevelPath = GameLevels.LevelsList[ActiveLevel];

        BreakableBlocks = CreateLevels.GetBreakableBlocks(LevelPath);
        UnBreakableBlocks = CreateLevels.GetUnbreakableBlocks(LevelPath);

    }

    private void WonGame() {
        stateMachine.ActiveState = new GameWon(stateMachine);
        ScoreBoard.ResetScore();
        HealthBoard.ResetLives();
    }
    private void GameLost() {
        if (HealthBoard.Lives == 0) {
            stateMachine.ActiveState = new LifeLost(stateMachine);
        }
    }


    private void NoBalls() {
        if (PlayerBalls.CountEntities() == 0) {
            effects.ClearContainer();
            ResetPlayerAndBall();
            healthBoard.LoseHealth();
            stateMachine.ActiveState = new LifeLost(stateMachine);
        }
    }

    public void LevelChecker() {

        if (BreakableBlocks.CountEntities() == 0) {
            ActiveLevel++;
            if (ActiveLevel < GameLevels.LevelsList.Count) {
                StartLevel();
                LoadLevel();
            } else {
                WonGame();
            }
        }

    }

    public void AddEffect(AddEffectEvent addEffectEvent) {
        var shape = new DynamicShape(addEffectEvent.Position, new Vector2(0.03f, 0.03f));
        Effect? effect = addEffectEvent.EffectType switch {
            "DoubleBall" => new DoubleBall(shape, new Image("Breakout.Assets.Images.ball2.png")),
            "SlowSpeed" => new SlowSpeed(shape, new Image("Breakout.Assets.Images.HalfSpeedPowerUp.png")),
            "ExtraLife" => new ExtraLife(shape, new Image("Breakout.Assets.Images.LifePickUp.png")),
            "LoseLife" => new LoseLife(shape, new Image("Breakout.Assets.Images.LoseLife.png")),
            "DoubleBSpeed" => new DoubleBSpeed(shape, new Image("Breakout.Assets.Images.DoubleBSpeed.png")),
            "QuadBall" => new QuadBall(shape, new Image("Breakout.Assets.Images.SplitPowerUp.png")),
            "UpgradeSpeed" => new UpgradeSpeed(shape, new Image("Breakout.Assets.Images.DoubleSpeedPowerUp.png")),
            _ => null
        };

        if (effect != null) {
            effects.AddEntity(effect);
        }
    }
    public void ResetPlayerSpeed(ResetPlayerSpeedEvent _) {
        player.ResetSpeed();
    }
    public void ResetBallSpeed(ResetBallSpeedEvent _) {
        PlayerBalls.Iterate(Ball => Ball.ResetBallSpeed());
    }

    public void StartLevel() {
        PlayerBalls.ClearContainer();
        effects.ClearContainer();
        UnBreakableBlocks.ClearContainer();

        ResetPlayerAndBall();
    }
    private void ResetPlayerAndBall() {
        player.Shape.AsDynamicShape().Position = new Vector2(0.40f, 0.08f);
        player.SetMoveLeft(false);
        player.SetMoveRight(false);
        player.ResetSpeed();
        pos = player.GetPosition();
        Ball ball = new Ball(
            new DynamicShape(pos + new Vector2(-0.0145f, 0.03f), new Vector2(0.03f, 0.03f)),
            new Image("Breakout.Assets.Images.ball.png"));
        ball.SetMoveLeft(false);
        ball.SetMoveRight(false);
        PlayerBalls.AddEntity(ball);
    }

    public int GetBallCount() {
        return PlayerBalls.CountEntities();
    }

    public Player GetPlayer() {
        return player;
    }

    public ScoreBoard GetScoreBoard() {
        return scoreBoard;
    }

    public HealthBoard GetHealthBoard() {
        return healthBoard;
    }
    public void ClearBalls() {
        PlayerBalls.ClearContainer();
    }

    public EntityContainer<Blocks.Block> GetBreakableBlocks() {
        return BreakableBlocks;
    }

    public Ball GetFirstBall() {
        Ball? firstBall = null;
        PlayerBalls.Iterate(ball => {
            if (firstBall == null)
                firstBall = ball;
        });
        return firstBall!;
    }

    public EntityContainer<Effect> GetEffects() {
        return effects;
    }
    public int GetActiveLevel() {
        return ActiveLevel;
    }



}

