using UnityEngine;
using System.Collections;

public class EnglishLanguage : Language{
	private  string[] _settingsItems = { "Input","Graphics","Sound","Interface"};
	public override string[] SettingsItems { get { return _settingsItems; } }
	private  string[] _menu1Items = {
		"Menu",
		"Achievements",
		"Tutorial",
		"Settings",
		"Exit"
	};
	public override string[] Menu1Items { get {return _menu1Items;}}
	private string[] _menu2Items = {
		"_instance","Character","Skills","Buffs"};
	private string[] _statusItems = {
		"HP:","MP:","Attack:","Defense:","Speed:","Intellect:","Luck:","Parry:","Reflex:"};
	public override string[] StatusItems { get { return _statusItems; } }
	public override string[] Menu2Items {get {return _menu2Items;}}
	private string[] _propItems = {"Heal:","Refresh:","ParticipantCondition:","Attack:","Defense:","Speed:","Intellect:","Slot:"};
	public override string[] PropItems { get { return _propItems; } }
	private string[] _itemMenuCat = {"Consumable","Equipment"};
	public override string[] ItemMenuCat {get { return _itemMenuCat;}}
	private string[] _inputItems = { "Sprint","Interact","Menu1","Menu2","MoveTransform forward","MoveTransform backward","MoveTransform right","MoveTransform left"};
	public override string[] InputItems {get{return _inputItems;}}
}
