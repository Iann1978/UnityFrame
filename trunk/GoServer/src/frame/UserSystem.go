package frame
import (
    "sync"
    "../packs"
    "fmt"
    "database/sql"
)
import _ "github.com/go-sql-driver/mysql"

var g_UserSystem *UserSystem
var lock_UserSystem *sync.Mutex = &sync.Mutex {}

func GetUserSystem() *UserSystem {
    lock_UserSystem.Lock()
    defer lock_UserSystem.Unlock()
    if g_UserSystem == nil {
        g_UserSystem = &UserSystem {}
    }
    return g_UserSystem
}

type UserSystem struct {
}

var columns string = `Id, Username, Coin, Diamond, Vigour, VipLevel, TeamLevel,
    TeamExp, TeamIcon, TeamIconFrame, BattlePower`

func SqlRowsToUserInfo(rows *sql.Rows) (*packs.UserInfo) {
    userInfo := new (packs.UserInfo)
    err := rows.Scan(&userInfo.Id,
                    &userInfo.Username, 
                    &userInfo.Coin,
                    &userInfo.Diamond,
                    &userInfo.Vigour,
                    &userInfo.VipLevel,
                    &userInfo.TeamLevel,
                    &userInfo.TeamExp,
                    &userInfo.TeamIcon,
                    &userInfo.TeamIconFrame,
                    &userInfo.BattlePower)
    CheckError(err)
    return userInfo
}

func (this *UserSystem) getBySql(sql string) (*packs.UserInfo, error) {
    db := GetDatabase().GetSqlDb();
    rows, err := db.Query(sql)
    CheckError(err)

    if rows.Next() != true {
        fmt.Println(err);
        return nil, err; 
    }

    userInfo := SqlRowsToUserInfo(rows);
    return userInfo, err
}


func (this *UserSystem) GetByUsername(username string)(*packs.UserInfo, error) {
    fmt.Println("UserSystem.GetByUsername")
    sql := fmt.Sprintf(`select %s from user where Username='%s'`, 
        columns, username)
    return this.getBySql(sql)
}

func (this *UserSystem) GetByUsernameAndPassword(username string, password string) (*packs.UserInfo, error){
    fmt.Println("UserSystem.GetByUsernameAndPassword")
    sql := fmt.Sprintf(`select %s from user where Username='%s' and Password='%s'`, 
        columns, username, password)
    return this.getBySql(sql)
}

func (this *UserSystem) GetById(id int32) (*packs.UserInfo, error){
    fmt.Println("UserSystem.GetById")
    sql := fmt.Sprintf(`select %s from user where Id='%d'`, columns, id) 
    return this.getBySql(sql)
}

func (this *UserSystem) NewUser(username string, password string) (*packs.UserInfo, error){
    fmt.Println("UserSystem.NewUser")
    db := GetDatabase().GetSqlDb();
    //sql := fmt.Sprintf(`INSERT INTO user(UserName, Password, Coin)
    //    VALUES ('%s','%s',10)`, username, password);



    stmt, err := db.Prepare(`INSERT user (UserName,Password,Coin) values (?,?,10)`)
    CheckError(err)
    res, err := stmt.Exec(username, password)
    CheckError(err)
    id, err := res.LastInsertId()
    CheckError(err)
    fmt.Println(id)
    return this.GetById(int32(id))
    //return nil, nil
    // UserInfo userInfo = new UserInfo();
    // string cmd = String.Format("INSERT INTO `user`(`UserName`, `Password`, `Coin`) VALUES ('{0}','{1}',10)", username, password);
    // Database.me.ExecNonQuery(cmd);
    // return GetByUsernameAndPassword(username, password);
}

func (this *UserSystem) ChangeTeamIconFrame(id int32, teamIconFrame int32) error {
    fmt.Println("UserSystem.ChangeTeamIconFrame")
    db := GetDatabase().GetSqlDb();
    stmt, err := db.Prepare(`UPDATE user set TeamiconFrame=? where Id=?`)
    CheckError(err)
    res, err := stmt.Exec(teamIconFrame, id)
    CheckError(err)
    num, err := res.RowsAffected()
    CheckError(err)
    fmt.Println(num)
    return err
}

func (this *UserSystem) ChangeUsername(id int32, username string) error {
    fmt.Println("UserSystem.ChangeUsername")
    db := GetDatabase().GetSqlDb();
    stmt, err := db.Prepare(`Update user set Username=? where Id=?`)
    CheckError(err)
    res, err := stmt.Exec(username, id)
    CheckError(err)
    num, err := res.RowsAffected()
    CheckError(err)
    fmt.Println(num)
    return err
}
