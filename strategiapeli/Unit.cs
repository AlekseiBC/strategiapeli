using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strategiapeli
{
    class Unit
    {
        public string name;
        public int health;
        public int attack;
        public bool alive;
        public int[] savedHealth = new int[1000];

        public Unit(string unitName, int unitHealth, int unitAttack)
        {
            name = unitName;
            health = unitHealth;
            attack = unitAttack;
            alive = true;
            savedHealth[0] = unitHealth;
        }
    }
}
