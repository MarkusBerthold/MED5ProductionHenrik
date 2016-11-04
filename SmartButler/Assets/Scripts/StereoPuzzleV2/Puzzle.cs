using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.StereoPuzzleV2 {
    public class Puzzle : MonoBehaviour {
        private List<HookPiece> hooksList = new List<HookPiece>();
        public string Ipv6Adress = "2001:DB8:85A3:8D3:1319:8A2E:370:7348";
        private Ipv6AdressPiece[] _ipv6AdressArray;
        private bool _puzzleIsSolved;
        public int NumberOfAlignedPiecesInOrderForThisPuzzleToBeSolved;

        void Awake(){
            _ipv6AdressArray = new Ipv6AdressPiece[Ipv6Adress.Length];
            for (int i = 0; i < Ipv6Adress.Length; i++) {
                _ipv6AdressArray[i] = new Ipv6AdressPiece(Ipv6Adress[i]);
                /* if (Ipv6Adress[i].Equals(":"))
                     continue;
                 else {
                      
            }*/
            }

            HookPiece[] hooks = GetComponentsInChildren<HookPiece>();
            foreach (HookPiece hook in hooks)
                hooksList.Add(hook);
        }

        public int CheckHooks(){
            bool[] piecesAreAlignedBools = new bool[hooksList.Count];

            for (int i = 0; i < hooksList.Count; i++){
                HookPiece hook = hooksList[i];
                if (hook.Aligned){
                    _ipv6AdressArray[hook.IpPosition].IsAligned = true;
                    piecesAreAlignedBools[i] = true;
                }else{
                    _ipv6AdressArray[hook.IpPosition].IsAligned = false;
                    piecesAreAlignedBools[i] = false;
                }
            }

            return Truth(piecesAreAlignedBools);
        }

        /// <summary>
        /// number of true values
        /// </summary>
        /// <param name="booleans"> the booleans to count</param>
        /// <returns>returns and integer with the value of the number of true values</returns>
        public int Truth(params bool[] booleans){
            if (NumberOfAlignedPiecesInOrderForThisPuzzleToBeSolved != 0 && booleans.Count(b => b) == NumberOfAlignedPiecesInOrderForThisPuzzleToBeSolved)
            {
                _puzzleIsSolved = true;
            }
            return booleans.Count(b => b);
        }

        public char GetIpPositionValue(int ipPosition){
            return _ipv6AdressArray[ipPosition].Value;
        }

        /// <summary>
        /// struct used to hold the character value and a bool describing wether or its character is aligned.
        /// </summary>
        private struct Ipv6AdressPiece {
            char _value;
            bool _isAligned;

            public Ipv6AdressPiece(char value) {
                _value = value;
                _isAligned = false;
            }

            public char Value
            {
                get { return _value; }
            }

            public bool IsAligned
            {
                get { return _isAligned; }
                set { _isAligned = value; }
            }
        }
    }
}