using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle
{
    class Skill
    {
        int skillType = 1;
        int multiply = 1;
        int _damage = 0;
        public int damage
        {
            get 
            {
                return _damage * multiply;
            }
            private set 
            {
                _damage = value;
            }
        }
        

        public Skill(int skillType, int damage)
        {
            this.skillType = skillType;
            this.damage = damage;
        }

        public int GetDamage()
        {
            return damage;
        }

        public void SetDamageMutiply(int multi)
        {
            multiply = multi;
        }
    }

    class Role
    {
        public int hp;
        protected int maxHp;
        public List<Skill> skills = new List<Skill>();
        protected Random random = new Random();

        public Role(int _hp)
        {
            hp = _hp;
            maxHp = _hp;
        }

        public void BeHit(int cost_hp)
        {
            hp -= cost_hp;
            if (hp < 0) { hp = 0; }

            if(hp * 1.0f / maxHp < 0.5f)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("愤怒！攻击力增加三倍");
                foreach(var skill in skills)
                {
                    skill.SetDamageMutiply(3);
                }
            }
        }

        virtual public Skill SelectSkill()
        {
            return null;
        }
    }

    class Player : Role
    {
        public Player(int _hp) : base(_hp)
        {
        }

        public override Skill SelectSkill()
        {
            bool success = false;
            int n = -1;

            while (!success)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Please select a skill {0}~{1}", 1, skills.Count);
                string input = Console.ReadLine();
                success = int.TryParse(input, out n);
                if (n <= 0 || n > skills.Count)
                {
                    success = false;
                }
            }

            return skills[n - 1];
        }
    }

    class Monster : Role
    {
        public Monster(int _hp) : base(_hp)
        { 

        }

        public override Skill SelectSkill()
        {
            return skills[random.Next(0, skills.Count)];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player(100);
            player.skills.Add(new Skill(1, 5));
            player.skills.Add(new Skill(1, 6));
            player.skills.Add(new Skill(1, 7));

            Monster monster = new Monster(100);
            monster.skills.Add(new Skill(1, 1));
            monster.skills.Add(new Skill(1, 12));

            Skill skill;
            while (true)
            {
                skill = player.SelectSkill();
                monster.BeHit(skill.damage);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("怪物被攻击，失去{0}点生命，目前生命：{1}", skill.damage, monster.hp);
                if(monster.hp <= 0)
                {
                    break;
                }

                skill = monster.SelectSkill();
                player.BeHit(skill.damage);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("玩家被攻击，失去{0}点生命，目前生命：{1}", skill.damage, player.hp);
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




