print("PanelMain has loaded.")

local GameObject = UnityEngine.GameObject
local LuaPanel = Assets.Script.Frame.LuaPanel
local panelMgr = Assets.Script.Frame.PanelManager.me
local gamedata = Gamedata.me

PanelMain = {}
setmetatable(PanelMain, LuaPanel)
PanelMain.__index = PanelMain

function PanelMain.New(base)
    o = {}
    o.base = base;
    return o;
end




function PanelMain:OnButtonSettingClick()
    local panel = panelMgr:Get(3)
    panel:Show()
    self.gameObject:SetActive(false)
end

function PanelMain:Init()
    
    self.txUsername = self.base:GetText("TextUsername");
    -- txCoin = self.panel:GetText("TextCoin");
    -- txDiamond = self.panel:GetText("TextDiamond");
    -- txVigour = self.panel:GetText("TextVigour");
    self.txVipLevel = self.base:GetText("TextVipLevel");
    self.txTeamLevel = self.base:GetText("TextTeamLevel");
    self.txBattlePower = self.base:GetText("TextBattlePower");

end

function PanelMain:OnButtonHeroListClick()
    local panel = panelMgr:Get(4)
    panel:Show()
end

function PanelMain:OnButtonTeamBoxClick()
    local panel = panelMgr:Get(5)
    panel:Show()
end


function PanelMain:OnShow()
    self.txUsername.text = gamedata.userinfo.Username;
    -- txCoin.text = tostring(gamedata.userinfo.Coin);
    -- txDiamond.text = tostring(gamedata.userinfo.Diamond);
    -- txVigour.text = tostring(gamedata.userinfo.Vigour);
    self.txVipLevel.text = tostring(gamedata.userinfo.VipLevel);
    self.txTeamLevel.text = tostring(gamedata.userinfo.TeamLevel);
    self.txBattlePower.text = "戰鬥力："..tostring(gamedata.userinfo.BattlePower);
end

function PanelMain:Refresh()
    self.txUsername.text = gamedata.userinfo.Username;
    -- txCoin.text = tostring(gamedata.userinfo.Coin);
    -- txDiamond.text = tostring(gamedata.userinfo.Diamond);
    -- txVigour.text = tostring(gamedata.userinfo.Vigour);
    self.txVipLevel.text = tostring(gamedata.userinfo.VipLevel);
    self.txTeamLevel.text = tostring(gamedata.userinfo.TeamLevel);
    self.txBattlePower.text = "戰鬥力："..tostring(gamedata.userinfo.BattlePower);
end