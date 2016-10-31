using System;
using Assets.Scripts.GameManager;
using Assets.Scripts.Platforms;
using UnityEngine;

namespace Assets.Scripts.StereoPuzzle{
    public class StereoPuzzle : MonoBehaviour{
        private StereoPuzzleHook[] _puzzleHooks = new StereoPuzzleHook[4];
        //private bool _puzzleIsSolved;
        private InvisiblePath _invisiblePath;

        public bool PuzzleIsSolved;
        //{ get { return _puzzleIsSolved; } set { _puzzleIsSolved = value; } }
        private SceneLoader _sceneLoader;

        private void Start(){
            _puzzleHooks = GetComponentsInChildren<StereoPuzzleHook>();
            /*
        _puzzleHooks[0] = Left  
        _puzzleHooks[1] = Bottom
        _puzzleHooks[2] = Right
        _puzzleHooks[3] =  Top
         */
            //invisiblePath = GameObject.Find("StraigthPath").GetComponent<InvisiblePath>();
            CheckAlignedPieces();
            _sceneLoader = GetComponent<SceneLoader>();
        }

        /// <summary>
        /// returns the piece at the given position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private StereoPuzzlePiece GetPieceAtPosition(StereoPuzzleHook.Position position){
            int puzzleHookIndex;
            switch (position){
                case StereoPuzzleHook.Position.Left:
                    puzzleHookIndex = 0;
                    return _puzzleHooks[puzzleHookIndex].CurrentPiece;
                case StereoPuzzleHook.Position.Bottom:
                    puzzleHookIndex = 1;
                    return _puzzleHooks[puzzleHookIndex].CurrentPiece;
                case StereoPuzzleHook.Position.Right:
                    puzzleHookIndex = 2;
                    return _puzzleHooks[puzzleHookIndex].CurrentPiece;
                case StereoPuzzleHook.Position.Top:
                    puzzleHookIndex = 3;
                    return _puzzleHooks[puzzleHookIndex].CurrentPiece;
                default:
                    return null;
            }
        }

        /// <summary>
        ///     checks if all the pieces are aligned with the correct pieces
        /// </summary>
        /// <returns></returns>
        public void CheckAlignedPieces(){
            var numberOfAlignedPieces = 0;
            foreach (StereoPuzzleHook.Position pos in Enum.GetValues(typeof(StereoPuzzleHook.Position))){
                var currentPieceIsAligned = GetPieceAtPosition(pos).IsKey;
                var alignedPiece = currentPieceIsAligned ? 1 : 0;
                numberOfAlignedPieces += alignedPiece;
            }
            //Debug.Log("numberOfAlignedPieces: " + numberOfAlignedPieces);

            if (numberOfAlignedPieces == 4){
                PuzzleIsSolved = true;
                _sceneLoader.LoadScene();
                //Debug.Log("StereoPuzzle - PuzzleIsSolved is: " + PuzzleIsSolved);
                //put in here whatever you want to happen when you solve the puzzle
                //invisiblePath.MakePathVisible(); //is not used for v2
            }
        }
    }
}