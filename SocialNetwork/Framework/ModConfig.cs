using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Framework
{
    class ModConfig
    {
        public int Bonus { get; set; } = 50;

        public bool TrackNetwork { get; set; } = true;

        public Dictionary<string, List<string>> Network { get; set; } = new Dictionary<string, List<string>>
        {
            [Characters.Abigail] = new List<string>
            {
                Characters.Caroline,
                Characters.Pierre,
                Characters.Sam,
                Characters.Sebastian
            },
            [Characters.Alex] = new List<string>
            {
                Characters.Evelyn,
                Characters.George,
                Characters.Haley
            },
            [Characters.Caroline] = new List<string>
            {
                Characters.Abigail,
                Characters.Pierre,
                Characters.Jodi
            },
            [Characters.Clint] = new List<string>
            {
                Characters.Emily,
                Characters.Willy
            },
            [Characters.Demetrius] = new List<string>
            {
                Characters.Maru,
                Characters.Robin,
                Characters.Sebastian
            },
            [Characters.Dwarf] = new List<string>
            {
                Characters.Wizard
            },
            [Characters.Elliot] = new List<string>
            {
                Characters.Leah,
                Characters.Willy
            },
            [Characters.Emily] = new List<string>
            {
                Characters.Clint,
                Characters.Gus,
                Characters.Haley,
                Characters.Sandy
            },
            [Characters.Evelyn] = new List<string>
            {
                Characters.Alex,
                Characters.George
            },
            [Characters.George] = new List<string>
            {
                Characters.Alex,
                Characters.Evelyn
            },
            [Characters.Gus] = new List<string>
            {
                Characters.Emily,
                Characters.Pam
            },
            [Characters.Haley] = new List<string>
            {
                Characters.Alex,
                Characters.Emily
            },
            [Characters.Harvey] = new List<string>
            {
                Characters.Maru
            },
            [Characters.Jas] = new List<string>
            {
                Characters.Marnie,
                Characters.Penny,
                Characters.Shane,
                Characters.Vincent
            },
            [Characters.Jodi] = new List<string>
            {
                Characters.Kent,
                Characters.Sam,
                Characters.Vincent,
                Characters.Caroline
            },
            [Characters.Kent] = new List<string>
            {
                Characters.Jodi,
                Characters.Sam,
                Characters.Vincent
            },
            [Characters.Krobus] = new List<string>
            {
                Characters.Wizard
            },
            [Characters.Leah] = new List<string>
            {
                Characters.Elliot
            },
            [Characters.Leo] = new List<string>
            {
                Characters.Jas,
                Characters.Linus,
                Characters.Penny,
                Characters.Vincent
            },
            [Characters.Lewis] = new List<string>
            {
                Characters.Marnie
            },
            [Characters.Linus] = new List<string>
            {
                Characters.Wizard
            },
            [Characters.Marnie] = new List<string>
            {
                Characters.Jas,
                Characters.Lewis,
                Characters.Shane
            },
            [Characters.Maru] = new List<string>
            {
                Characters.Demetrius,
                Characters.Harvey,
                Characters.Penny,
                Characters.Robin,
                Characters.Sebastian
            },
            [Characters.Pam] = new List<string>
            {
                Characters.Gus,
                Characters.Penny
            },
            [Characters.Penny] = new List<string>
            {
                Characters.Jas,
                Characters.Maru,
                Characters.Pam,
                Characters.Sam,
                Characters.Vincent
            },
            [Characters.Pierre] = new List<string>
            {
                Characters.Abigail,
                Characters.Caroline
            },
            [Characters.Robin] = new List<string>
            {
                Characters.Demetrius,
                Characters.Maru,
                Characters.Sebastian
            },
            [Characters.Sam] = new List<string>
            {
                Characters.Abigail,
                Characters.Jodi,
                Characters.Kent,
                Characters.Penny,
                Characters.Sebastian,
                Characters.Vincent
            },
            [Characters.Sandy] = new List<string>
            {
                Characters.Emily
            },
            [Characters.Sebastian] = new List<string>
            {
                Characters.Abigail,
                Characters.Demetrius,
                Characters.Maru,
                Characters.Robin,
                Characters.Sam
            },
            [Characters.Shane] = new List<string>
            {
                Characters.Jas,
                Characters.Marnie
            },
            [Characters.Vincent] = new List<string>
            {
                Characters.Jas,
                Characters.Jodi,
                Characters.Kent,
                Characters.Penny,
                Characters.Sam
            },
            [Characters.Willy] = new List<string>
            {
                Characters.Clint,
                Characters.Elliot
            },
            [Characters.Wizard] = new List<string>
            {
                Characters.Dwarf,
                Characters.Krobus,
                Characters.Linus
            }
        };
    }
}
