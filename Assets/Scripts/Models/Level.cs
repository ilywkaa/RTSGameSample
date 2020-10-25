using System;

namespace Models
{
    [Serializable]
    public class Level
    {
        public Field Field;
        public Obstacle[] Obstacles;
        public Character[] Characters;
        public Gem[] Gems;
    }
}