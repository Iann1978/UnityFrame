using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace HallAndRoomServer
{
    class HeroSystem : Singleton<HeroSystem>
    {
        public static HeroInfo ReaderHeroInfo(MySqlDataReader reader)
        {
            HeroInfo heroInfo = new HeroInfo();
            heroInfo.Id = reader.GetInt32("Id");
            heroInfo.Level = reader.GetInt32("Level");
            heroInfo.Star = reader.GetInt32("Star");
            heroInfo.Quality = reader.GetInt32("Quality");
            return heroInfo;
        }
        public HeroInfo NewHero(int userid, int resid)
        {
            HeroInfo heroInfo = new HeroInfo();
            heroInfo.Id = (userid << 16) + resid;
            heroInfo.Level = 1;
            heroInfo.Star = 1;
            heroInfo.Quality = 1;
            return NewHero(userid, heroInfo);

        }

        public HeroInfo NewHero(int userid, HeroInfo heroInfo)
        {
            string cmd = string.Format("insert into hero (Id, Level, Star, Quality) values ({0},{1},{2},{3})",
                heroInfo.Id, heroInfo.Level, heroInfo.Star, heroInfo.Quality);
            Database.me.ExecNonQuery(cmd);
            return heroInfo;
        }


        public List<HeroInfo> AllHeros(int userid)
        {
            List<HeroInfo> heroList = new List<HeroInfo>();
            string cmd = string.Format("select * from hero where Id>{0} and Id<{1}", userid<<16, (userid+1)<<16);
            MySqlDataReader reader =  Database.me.ExecQuery(cmd);
            while (reader.Read())
            {
                HeroInfo heroInfo = ReaderHeroInfo(reader);
                heroList.Add(heroInfo);
            }
            reader.Close();
            return heroList;
        }
    }
}
