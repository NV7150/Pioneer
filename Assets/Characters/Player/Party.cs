using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character {
    public class Party {
        List<IPlayable> party = new List<IPlayable>();
        private readonly static int PARTY_MAX = 4;

        public bool join(IPlayable character) {
            if (party.Count >= PARTY_MAX) {
                Debug.Log("into false");
                return false;
            }
            party.Add(character);
            return true;
        }

        public void left(IPlayable character) {
            party.Remove(character);
        }

        public List<IPlayable> getParty() {
            return new List<IPlayable>(party);
        }
    }
}