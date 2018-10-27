package frame
import (
    "sync"
    . "../packs"
    "fmt"
    "database/sql"
)
import _ "github.com/go-sql-driver/mysql"

var g_HeroSystem *HeroSystem
var lock_HeroSystem *sync.Mutex = &sync.Mutex {}

func GetHeroSystem() *HeroSystem {
    lock_HeroSystem.Lock()
    defer lock_HeroSystem.Unlock()
    if g_HeroSystem == nil {
        g_HeroSystem = &HeroSystem {}
    }
    return g_HeroSystem
}

type HeroSystem struct {
}

func SqlRowsToHeroInfo(rows *sql.Rows) (*HeroInfo) {
    heroInfo := new (HeroInfo)
    err := rows.Scan(&heroInfo.Id,
                    &heroInfo.Level, 
                    &heroInfo.Star,
                    &heroInfo.Quality)
    CheckError(err)
    return heroInfo
}

func (this *HeroSystem) InsertHero(heroInfo *HeroInfo) error {
    fmt.Println("HeroSystem.InsertHero")
    db := GetDatabase().GetSqlDb();
    //sql := fmt.Sprintf(`INSERT INTO user(UserName, Password, Coin)
    //    VALUES ('%s','%s',10)`, username, password);



    stmt, err := db.Prepare(`INSERT hero (Id, Level, Star, Quality) values (?,?,?,?)`)
    CheckError(err)
    res, err := stmt.Exec(heroInfo.Id, heroInfo.Level, heroInfo.Star, heroInfo.Quality)
    CheckError(err)
    id, err := res.LastInsertId()
    CheckError(err)
    fmt.Println(id)
    return err

    // string cmd = string.Format("insert into hero (Id, Level, Star, Quality) values ({0},{1},{2},{3})",
    //     heroInfo.Id, heroInfo.Level, heroInfo.Star, heroInfo.Quality);
    // Database.me.ExecNonQuery(cmd);
    // return heroInfo;
}

func (this *HeroSystem) NewAndInsertHero(userid int32, resid int32) (*HeroInfo, error) {
    fmt.Println("HeroSystem.NewAndInsertHero")
    heroInfo := new (HeroInfo)
    heroInfo.Id = (userid << 16) + resid
    heroInfo.Level = 1
    heroInfo.Star = 1
    heroInfo.Quality = 1
    err := this.InsertHero(heroInfo)
    return heroInfo, err
}

func (this *HeroSystem) AllHeros(userid int32) ([]*HeroInfo, error) {
    fmt.Println("HeroSystem.AllHeros")
    db := GetDatabase().GetSqlDb();
    heros := make([]*HeroInfo,0,20)
    sql := fmt.Sprintf(`select Id, Level, Star, Quality from hero where Id>%d and Id<%d`, 
        userid<<16, (userid+1)<<16)
    fmt.Println(sql)
   
    rows, err := db.Query(sql)
    CheckError(err)

 
    for rows.Next() {
        heroInfo := SqlRowsToHeroInfo(rows)
        heros = append(heros, heroInfo)
    }
    return heros, nil
}
      
// public List<HeroInfo> AllHeros(int userid)
// {
//     List<HeroInfo> heroList = new List<HeroInfo>();
//     string cmd = string.Format("select * from hero where Id>{0} and Id<{1}", userid<<16, (userid+1)<<16);
//     MySqlDataReader reader =  Database.me.ExecQuery(cmd);
//     while (reader.Read())
//     {
//         HeroInfo heroInfo = ReaderHeroInfo(reader);
//         heroList.Add(heroInfo);
//     }
//     reader.Close();
//     return heroList;
// }