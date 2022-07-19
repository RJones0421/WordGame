using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distribution : MonoBehaviour
{
    [SerializeField] private List<Powerup> powerups = new List<Powerup>();

    public Powerup SelectPowerup() {

        // Quasi-Zipfian Distribution for Powerups -- Further down on list == less common

        int selection;

        while(true) {

            // Randomly select number in list

            selection = Random.Range(0, powerups.Count);

            // Check Probability: (1 / (2/3)x) for x > 1
            // x == rank in list starting at 1 
            //(1, 0.75, 0.5, 0.375, 0.3, 0.25, etc.)

            if (selection == 0) return powerups[selection];

            else {
                int selectionCheck = selection + 1;
                int probabilityRole = Random.Range(1, 101);

                if (probabilityRole <= (100 / ((2/3) * selectionCheck))) return powerups[selection];
            }

        }

    }
}
