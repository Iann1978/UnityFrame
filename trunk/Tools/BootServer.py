import sys
import os
import Path

def BootServer() :
	print("BootServer")
	os.system("start c:/xampp/mysql/bin/mysqld --defaults-file=c:/xampp/mysql/bin/my.ini --standalone --console");
	binPath = Path.GetBinPath()
	cmd = "start " + binPath + "/Server/HallAndRoomServer.exe";
	os.system(cmd);

if sys.argv[0] == __file__ :
	BootServer()
