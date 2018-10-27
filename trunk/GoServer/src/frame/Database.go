package frame
import (
    "sync"
    //"fmt"
    //"github.com/golang/protobuf/proto"
	//"./packs"
	//"net"
   "database/sql"
    //"github.com/go-sql-driver/mysql"
)
import _ "github.com/go-sql-driver/mysql"

var g_Database *Database
var lock_Database *sync.Mutex = &sync.Mutex {}

func GetDatabase() *Database {
    lock_Database.Lock()
    defer lock_Database.Unlock()
    if g_Database == nil {
        g_Database = &Database {}
    }
    return g_Database
}

type Database struct {
    db *sql.DB;
    username string;
    //packGenerators map[int]PackGenerator
    //packResponsors map[int]PackResponsor
}

func (this *Database) Connect() {
    db, err := sql.Open("mysql", "root:@/newframe")
    if err != nil {
        panic("error in Connect")
    }
    this.db = db

    // rows, err := db.Query("SELECT Username, Password, Age FROM user WHERE Username='ccc'")
    // if err != nil {
    //     fmt.Println(err);
    //     panic("error1 in Connect");
        
    // }

    // if rows.Next() {
    //     var Username string
    //     var Password string
    //     var Age int32
    //     rows.Scan(&Username, &Password, &Age);
    //     //var Username string
    //     //cols, _ := rows.Columns()
    //     //Username = cols[0]
    //     //Username = cols["Username"].(string)
    //     // if err != nil {
    //     //     fmt.Println(err);
    //     //     panic("error2 in Connect")
    //     // }
    //     //this.username = Username
    //     fmt.Println(Username)
    //     fmt.Println(Password)
    //     fmt.Println(Age)
    // }

}

func (this *Database) DisConnect() {
    this.db.Close();
}

func (this *Database) GetSqlDb() (*sql.DB) {
    return this.db;
}