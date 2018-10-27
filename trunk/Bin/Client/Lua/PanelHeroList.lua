-- PanelHeroList.lua
print("PanelHeroList.lua loaded")


local GameObject = UnityEngine.GameObject
local LuaPanel = Assets.Script.Frame.LuaPanel
local panelMgr = Assets.Script.Frame.PanelManager.me
local gamedata = Gamedata.me

PanelHeroList = {}

function PanelHeroList.New(base)
    print("PanelHeroList.New")
    o = {}
    o.base = base;
    return o;
end

function PanelHeroList:Init()
	Debugger.Log("LuaPanelHeroList.Init")
	Debugger.Log(self.base.controls);
	local controls = self.base.controls;

	tp = typeof(UnityEngine.RectTransform)
	Debugger.Log(typeof(UnityEngine.RectTransform));
	self.HeroItem = controls:GetObject("HeroItem"):GetComponent(typeof(UnityEngine.RectTransform))
	self.OwnedHeros = controls:GetObject("OwnedHeros"):GetComponent(typeof(UnityEngine.RectTransform));
	self.NotOwnedHeros = controls:GetObject("NotOwnedHeros"):GetComponent(typeof(UnityEngine.RectTransform));
	self.ScrollView = controls:GetObject("ScrollView"):GetComponent(typeof(UnityEngine.UI.ScrollRect));
	self.HeroItem.gameObject:SetActive(false);
	self.ownedHeroCount = 0;
	self.notOwnedHeroCount = 0;
	Debugger.Log(self.HeroItem);
end

function PanelHeroList:AddHero(isOwned)
	Debugger.Log("PanelHeroList.AddHero");
	local lines = 0;
	local panel = nil;
	if (isOwned) then 
		panel = self.OwnedHeros;
		self.ownedHeroCount = self.ownedHeroCount + 1;
		lines = math.floor((self.ownedHeroCount + 1)/2);
	else 
		panel = self.NotOwnedHeros;
		self.notOwnedHeroCount = self.notOwnedHeroCount + 1;
		lines = math.floor((self.notOwnedHeroCount +1)/2);
	end
	local glg = panel:GetComponent(typeof(UnityEngine.UI.GridLayoutGroup));
	local height = self.HeroItem.sizeDelta.y * lines + (lines - 1) * glg.spacing.y + glg.padding.top + glg.padding.bottom;
	--newItem = self:Instantiate(typeof(UnityEngine.RectTransform), self.HeroItem);
	local goNewItem = GameObject.Instantiate(self.HeroItem.gameObject);
	print("goNewItem");
	print(goNewItem);
	goNewItem.name = self.HeroItem.gameObject.name;
	local newItem = goNewItem:GetComponent(typeof(UnityEngine.RectTransform));
    newItem:SetParent(panel);
    newItem.gameObject:SetActive(true);
    panel.sizeDelta = UnityEngine.Vector2.New(panel.sizeDelta.x, height);
    --local newHeroItem = newItem:GetComponent(typeof(HeroItem));
    local newHeroItem = newItem:GetComponent(typeof(LuaPanel));
    print('newHeroItem:');
    print(newHeroItem);
    newHeroItem:Init();
    print(newHeroItem.self);
    print(newHeroItem.self.Init);
    return newHeroItem.self;
end

function PanelHeroList:RefreshOwnedHeros()
	local heros = Gamedata.me.userinfo.HeroList;
	local count = heros.Count;
	for i=0,count-1,1 do
		local newHeroItem = PanelHeroList.AddHero(self, true);
		local heroInfo = heros:get_Item(i);
		newHeroItem:SetHeroInfo(heroInfo);
		Debugger.Log(heros:get_Item(i));
	end
end

function PanelHeroList:RefreshNotOwnedHeros()
	local heros = Database.me:GetAll(typeof(HeroTableData));
	local count = heros.Length;
	for i=0,count-1,1 do
		local newHeroItem = PanelHeroList.AddHero(self, false);
		local heroData = heros[i];
		newHeroItem:SetHeroData(heroData);

	end
end

function PanelHeroList:Refresh()
	Debugger.Log("PanelHeroList.Refresh");
	PanelHeroList.RefreshOwnedHeros(self);
	PanelHeroList.RefreshNotOwnedHeros(self);
	-- local heros = Gamedata.me.userinfo.HeroList;
	-- local count = heros.Count;
	-- for i=0,count-1,1 do
	-- 	local newHeroItem = PanelHeroList.AddHero(self, true);
	-- 	local heroInfo = heros:get_Item(i);
	-- 	newHeroItem:SetHeroInfo(heroInfo);
	-- 	Debugger.Log(heros:get_Item(i));
	-- end


	-- local a = {"Sunday"};
	-- a.a = "aaa";
	-- Debugger.Log(a.a);
	-- for k, v in ipairs(self) do
	-- 	Debugger.Log(k);
	-- 	Debugger.Log(v);
	-- end
	-- Debugger.Log("PanelHeroList.RefreshEnd");
	-- ;
end

function PanelHeroList:OnButtonCloseClick()
	self.base:Hide();
end
function PanelHeroList:OnButtonLogonClick()
	Debugger.Log("Lua_PanelHeroList.OnButtonLogonClick")
	Debugger.Log(self.username);
	Debugger.Log("username:"..self.username.text);
	Debugger.Log("password:"..self.password.text);

	reqLogon = ReqLogon.New();
    reqLogon.Username = self.username.text;
    reqLogon.Password = self.password.text;
    Net.me:Send2(reqLogon);
end

function PanelHeroList:OnButtonRegistClick()
	Debugger.Log("Lua_PanelHeroList.OnButtonLogonClick")
	Debugger.Log("username:"..self.username.text);
	Debugger.Log("password:"..self.password.text);

	reqRegist = ReqRegist.New();
	reqRegist.Username = self.username.text;
	reqRegist.Password = self.password.text;
	Net.me:Send2(reqRegist);
end

