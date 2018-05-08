using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle
{
    class Skill
    {
        public int damage { get; private set; }
        int skillType = 1;

        public Skill(int skillType, int damage)
        {
            this.skillType = skillType;
            this.damage = damage;
        }

        public int GetDamage()
        {
            return damage;
        }
    }

    class Role
    {
        public int hp;
        public int attack;
        public List<Skill> skills = new List<Skill>();
        Random random = new Random();

        public Role()
        {
        }

        public Role(int _hp, int _attack)
        {
            hp = _hp;
            attack = _attack;
        }

        public void BeHit(int cost_hp)
        {
            hp -= cost_hp;
            if (hp < 0) { hp = 0; }
        }

        public Skill RandomSkill()
        {
            return skills[random.Next(0, skills.Count)];
        }

        public Skill ChooseSkill()
        {
            bool success = false;
            int n = -1;

            while (!success)
            {
                Console.WriteLine("请选择技能 {0}~{1}", 1, skills.Count);
                string input = Console.ReadLine();
                success = int.TryParse(input, out n);
                if (n <= 0 || n >= skills.Count)
                {
                    success = false;
                }
            }

            return skills[n - 1];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Role player = new Role(50, 10);
            Role monster = new Role(100, 5);

            while(true)
            {
                monster.BeHit(player.RandomSkill().damage);
                Console.WriteLine("怪物被攻击，失去{0}点生命，目前生命：{1}", player.attack, monster.hp);
                if(monster.hp <= 0)
                {
                    break;
                }

                player.BeHit(monster.attack);
                Console.WriteLine("玩家被攻击，失去{0}点生命，目前生命：{1}", monster.attack, player.hp);
                if(player.hp <= 0)
                {
                    break;
                }
            }

            if(player.hp > 0)
            {
                Console.WriteLine("Player win!");
            }
            else
            {
                Console.WriteLine("Monster win!");
            }

            Console.ReadKey();
        }
    }
}
