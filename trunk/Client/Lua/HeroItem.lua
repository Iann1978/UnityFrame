-- HeroItem.lua
print("HeroItem.lua loaded")


local GameObject = UnityEngine.GameObject
local LuaPanel = Assets.Script.Frame.LuaPanel
local panelMgr = Assets.Script.Frame.PanelManager.me
local database = Database.me;
local gamedata = Gamedata.me;
local spriteFactory = Assets.Script.SpriteFactory.me;

HeroItem = {}
HeroItem.__index = HeroItem;

function HeroItem.New(base)
    print("HeroItem.New")
    o = {}
    o.base = base;
    setmetatable(o,HeroItem);
    print(o.Init);
    return o;
end

function HeroItem:Init()
	Debugger.Log("HeroItem.Init")
	Debugger.Log(self.base.controls);
	local controls = self.base.controls;

	self.ImageHeroHead = controls:GetImage("ImageHeroHead");
    self.ImageHeroQuality = controls:GetImage("ImageHeroQuality");
    self.ImageHeroOccupation = controls:GetImage("ImageHeroOccupation");
    self.HeroLevel = controls:GetText("HeroLevel");
    self.TextHeroName = controls:GetText("TextHeroName");
    self.TextHeroLevel = controls:GetText("TextHeroLevel");
    self.TextChips = controls:GetText("TextChips");
    self.SliderChips = controls:GetSlider("SliderChips");

end



function HeroItem:SetHeroInfo(heroInfo)
    Debugger.Log("HeroItem.SetHeroInfo")
    local heroResData = heroInfo.Id % 0x10000;
    local heroTableData = database:GetById(typeof(HeroTableData), heroResData);
    self.ImageHeroHead.sprite = spriteFactory:Get("HeroHead", heroResData);
    self.ImageHeroQuality.sprite = spriteFactory:Get("HeroQuality", heroInfo.Quality);
    self.ImageHeroOccupation.sprite = spriteFactory:Get("HeroOccupation", heroTableData.HeroOccupation);
    self.TextHeroName.text = heroTableData.HeroName;
    self.TextHeroLevel.text = "Lv."..tostring(heroInfo.Level);
    self.HeroLevel.gameObject:SetActive(true);
    self.SliderChips.gameObject:SetActive(false);
end


function HeroItem:SetHeroData(heroData)
 	Debugger.Log("HeroItem.SetHeroData")
    self.ImageHeroHead.sprite = spriteFactory:Get("HeroHead", heroData.Id);
    self.ImageHeroQuality.sprite = spriteFactory:Get("HeroQuality", 1);
    self.ImageHeroOccupation.sprite = spriteFactory:Get("HeroOccupation", heroData.HeroOccupation);
    self.TextHeroName.text = heroData.HeroName;
    self.HeroLevel.gameObject:SetActive(false);
    self.SliderChips.gameObject:SetActive(true);
end
