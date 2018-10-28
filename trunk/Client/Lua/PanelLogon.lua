--PanelLogon
print("PanelLogon has loaded.")
local LuaPanel = Assets.Script.Frame.LuaPanel
PanelLogon = {}
setmetatable(PanelLogon, LuaPanel)
PanelLogon.__index = PanelLogon

function PanelLogon.New(base)
	print("PanelLogon.New")
	o = {}
	o.base = base;
	print(o.base.name)
	return o
end

function PanelLogon:Init()
	Debugger.Log("LuaPanelLogon.Init")
	Debugger.Log(self.base.name)
	Debugger.Log(self.base.controls)

	self.username = self.base.controls:GetInputField("InputFieldUsername")
	self.password = self.base.controls:GetInputField("InputFieldPassword")
	Debugger.Log(self.username);
end

function PanelLogon:OnButtonLogonClick()
	Debugger.Log("Lua_PanelLogon.OnButtonLogonClick")
	Debugger.Log(self.username);
	Debugger.Log("username:"..self.username.text);
	Debugger.Log("password:"..self.password.text);

	reqLogon = ReqLogon.New();
    reqLogon.Username = self.username.text;
    reqLogon.Password = self.password.text;
    Net.me:Send2(reqLogon);
end

function PanelLogon:OnButtonRegistClick()
	Debugger.Log("Lua_PanelLogon.OnButtonLogonClick")
	Debugger.Log("username:"..self.username.text);
	Debugger.Log("password:"..self.password.text);

	reqRegist = ReqRegist.New();
	reqRegist.Username = self.username.text;
	reqRegist.Password = self.password.text;
	Net.me:Send2(reqRegist);
end

