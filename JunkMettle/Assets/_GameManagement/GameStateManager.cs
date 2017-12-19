using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

        public enum GAME_STATE { START, GAME, PLACE, WIN, LOOSE, OPTIONS };
        private GAME_STATE currentState = GAME_STATE.START;

        public GAME_STATE CurrentState{

            get{ return currentState; }
            set{
                currentState = value;
                StopAllCoroutines();

                switch(currentState) {
                    case GAME_STATE.START:
                        StartCoroutine(GSStart());   
                    break;

                case GAME_STATE.PLACE:
                    StartCoroutine(GSPlace());
                    break;

                case GAME_STATE.GAME:
                    StartCoroutine(GSGame());
                    break;

                case GAME_STATE.WIN:
                    StartCoroutine(GSWin());
                    break;

                case GAME_STATE.LOOSE:
                    StartCoroutine(GSLoose());
                    break;

                case GAME_STATE.OPTIONS:
                    StartCoroutine(GSOptions());
                    break;
            }
            }
        }

        public IEnumerator GSStart() {
                 while (currentState == GAME_STATE.START){

                    yield return null;

                 }
        }

        public IEnumerator GSPlace() {
            while (currentState == GAME_STATE.PLACE) {

                yield return null;

             }

    }

        public IEnumerator GSGame() {
            while (currentState == GAME_STATE.GAME) {

                yield return null;

             }


    }
        public IEnumerator GSWin() {
            while (currentState == GAME_STATE.WIN) {

                yield return null;

            }

    }

        public IEnumerator GSLoose() {
            while (currentState == GAME_STATE.LOOSE) {

                yield return null;

            }

    }

        public IEnumerator GSOptions() {
            while (currentState == GAME_STATE.OPTIONS) {

                yield return null;

              }

    }


        // Use this for initialization
        void Awake() {

        }

        void Start(){

        }

        // Update is called once per frame
        void Update() {

        }
        
    }


