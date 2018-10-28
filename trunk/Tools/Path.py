
import os;
def GetTrunkPath() :
	path = os.path.join(os.getcwd(), "../");
	path = path.replace("\\","/");
	return path;

def GetTexturePackerExe() :
	path = os.environ["TexturePackerExe"];
	path = path.replace("\\","/");
	return path;

def GetUnityExe() :
	path = os.environ["UnityExe"];
	path = path.replace("\\","/");
	return path;

def GetMsvsExe() : 
	path = os.environ["MsvsExe"];
	path = path.replace("\\","/");
	return path;

def GetAltasPath() :
	trunkPath = GetTrunkPath();
	path = os.path.join(trunkPath, "Client/Assets/Resources/Altas/");
	return path;

def GetExcelPath() : 
	trunkPath = GetTrunkPath();
	path = os.path.join(trunkPath, "Excels/")
	return path

def GetLocalDb() :
	trunkPath = GetTrunkPath();
	path = os.path.join(trunkPath, "Csv/StaticTables.db")
	return path

def GetBinPath() :
	trunkPath = GetTrunkPath();
	path = os.path.join(trunkPath, "Bin/")
	return path

def GetLogicLuaPath() :
    trunkPath = GetTrunkPath();
    path = os.path.join(trunkPath, "Client/Lua/");
    return path

def GetSystemLuaPath() :
    trunkPath = GetTrunkPath();
    path = os.path.join(trunkPath, "Client/Assets/Thirds/ToLua/ToLua/Lua/");
    return path
