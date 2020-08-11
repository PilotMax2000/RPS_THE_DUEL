using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DuelsRPG
{
    public class BattleStatesHandler : MonoBehaviour
    {
        public Fighter FirstFighter => _firstFighter;
        public Fighter SecondFighter => _secondFighter;
        public Fighter CurrentFighter => _currentFighter;
        public DuelResult LastDuelResult => _lastDuelResult;

        public static BattleStatesHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType(typeof(BattleStatesHandler)) as BattleStatesHandler;
                    if (_instance == null)
                    {
                        Debug.LogWarning("There should be at least one BattleHandler on the scene");
                    }
                }

                return _instance;
            }
        }

        private Fighter _firstFighter;
        private Fighter _secondFighter;
        private Fighter _currentFighter;

        //Save result of prev round for giving first move of the next round to the winner
        private DuelResult _lastDuelResult;

        private static BattleStatesHandler _instance;
        private StateMachine _stateMachine = new StateMachine();

        /*
         * Main battle states:
         * 1)BattleIntro - animation of characters appearing
         * 2)FighterTurn (x2) - fighters select element/spells to cast
         * 3)CastSelectedElements - creation and attacking opponents with elements. Continues until all elements will be destroyed.
         * 4)BattleVictory - shows only if one of the characters dies after duels result where calculated.
         * ..In other case new duels start from the player that dealt (more) damage in last duel
         */

        private void Start()
        {
            RandomizePlayers();
            _stateMachine.ChangeState(new StateBattleIntro());
        }

        private void Update()
        {
            _stateMachine.ExecuteStateUpdate();
        }

        private void OnEnable()
        {
            EventsHandler.StartListening(GameEvent.ElementWasChosen, SelectNextPlayer);
            EventsHandler.StartListening(GameEvent.StartNewDuel, SelectNextPlayer);
            EventsHandler.StartListening(GameEvent.SpawnPlayers, SpawnPlayers);
        }

        private void OnDisable()
        {
            EventsHandler.StopListening(GameEvent.ElementWasChosen, SelectNextPlayer);
            EventsHandler.StopListening(GameEvent.StartNewDuel, SelectNextPlayer);
            EventsHandler.StopListening(GameEvent.SpawnPlayers, SpawnPlayers);
        }

        private void SpawnPlayers()
        {
            StartCoroutine(SpawnPlayersWithDelay());
        }

        public bool IsVictoryConditionReached()
        {
            return _firstFighter.Health.IsDead || _secondFighter.Health.IsDead;
        }

        //Randomize player who will have the first move
        private void RandomizePlayers()
        {
            int rand = Random.Range(0, 2);
            _firstFighter = rand == 0 ? BattleData.Instance.Player1 : BattleData.Instance.Player2;
            _secondFighter = rand == 0 ? BattleData.Instance.Player2 : BattleData.Instance.Player1;
        }

        private void SelectNextPlayer()
        {
            if (_currentFighter == null)
            {
                _currentFighter = _firstFighter;
                _stateMachine.ChangeState(new StateFighterTurn(_currentFighter));
                return;
            }

            if (_currentFighter.ID != _secondFighter.ID)
            {
                _currentFighter = _secondFighter;
                _stateMachine.ChangeState(new StateFighterTurn(_currentFighter));
            }
            else
            {
                _currentFighter = null;
                _stateMachine.ChangeState(new StateCastSelectedElements());
            }
        }

        private IEnumerator SpawnPlayersWithDelay()
        {
            yield return new WaitForSeconds(BattleData.Instance.WaitAfterSceneStart);
            _firstFighter.gameObject.SetActive(true);
            _secondFighter.gameObject.SetActive(true);
        }
    }
    
public enum Element { None, Rock, Paper, Scissors };
public enum Result { Error, FirstPlayer, SecondPlayer, Draw }
}

