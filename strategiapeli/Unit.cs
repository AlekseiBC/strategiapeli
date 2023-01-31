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

        public Unit(string unitName, int unitHealth, int unitAttack)
        {
            name = unitName;
            health = unitHealth;
            attack = unitAttack;
            alive = true;
        }
    }
}
