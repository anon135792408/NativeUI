﻿using System;
using System.Drawing;
using System.Collections.Generic;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using NativeUI;

public class MenuExample : BaseScript
{
	private bool ketchup = false;
	private string dish = "Banana";
	private MenuPool _menuPool;

	public void AddMenuKetchup(UIMenu menu)
	{
		var newitem = new UIMenuCheckboxItem("Add ketchup?", UIMenuCheckboxStyle.Cross, ketchup, "Do you wish to add ketchup?");
		menu.AddItem(newitem);
		menu.OnCheckboxChange += (sender, item, checked_) =>
		{
			if (item == newitem)
			{
				ketchup = checked_;
				Screen.ShowNotification("~r~Ketchup status: ~b~" + ketchup);
			}
		};
	}

	public void AddMenuFoods(UIMenu menu)
	{
		var foods = new List<dynamic>
		{
			"Banana",
			"Apple",
			"Pizza",
			"Quartilicious",
			0xF00D, // Dynamic!
        };
		var newitem = new UIMenuListItem("Food", foods, 0);
		menu.AddItem(newitem);
		menu.OnListChange += (sender, item, index) =>
		{
			if (item == newitem)
			{
				dish = item.Items[index].ToString();
				Screen.ShowNotification("Preparing ~b~" + dish + "~w~...");
			}

		};
	}

	public void HeritageMenu(UIMenu menu)
	{
		var heritagemenu = _menuPool.AddSubMenu(menu, "Heritage Menu");
		var heritageWindow = new UIMenuHeritageWindow(0, 0);
		heritagemenu.AddWindow(heritageWindow);
		List<dynamic> momfaces = new List<dynamic>() { "Hannah", "Audrey", "Jasmine", "Giselle", "Amelia", "Isabella", "Zoe", "Ava", "Camilla", "Violet", "Sophia", "Eveline", "Nicole", "Ashley", "Grace", "Brianna", "Natalie", "Olivia", "Elizabeth", "Charlotte", "Emma", "Misty" };
		List<dynamic> dadfaces = new List<dynamic>() { "Benjamin", "Daniel", "Joshua", "Noah", "Andrew", "Joan", "Alex", "Isaac", "Evan", "Ethan", "Vincent", "Angel", "Diego", "Adrian", "Gabriel", "Michael", "Santiago", "Kevin", "Louis", "Samuel", "Anthony", "Claude", "Niko", "John" };
		List<dynamic> lista = new List<dynamic>();
		for (int i = 0; i < 101; i++) lista.Add(i);
		var mom = new UIMenuListItem("Mamma", momfaces, 0);
		var dad = new UIMenuListItem("Papà", dadfaces, 0);
		var newItem = new UIMenuSliderHeritageItem("Heritage Slider", "This is Useful on heritage", true);
		heritagemenu.AddItem(mom);
		heritagemenu.AddItem(dad);
		heritagemenu.AddItem(newItem);
		int MomIndex = 0;
		int DadIndex = 0;
		heritagemenu.OnListChange += (_sender, _listItem, _newIndex) =>
		{
			if (_listItem == mom)
			{
				MomIndex = _newIndex;
				heritageWindow.Index(MomIndex, DadIndex);
			}
			else if (_listItem == dad)
			{
				DadIndex = _newIndex;
				heritageWindow.Index(MomIndex, DadIndex);
			}
			// This way the heritage window changes only if you change a list item!
		};

		heritagemenu.OnSliderChange += (_sender, _item, _newIndex) =>
		{
			if (_item == newItem)
			{
				Screen.ShowNotification("Wow the slider changed! Who do i look like??");
			}
		};
	}

	public void AddMenuCook(UIMenu menu)
	{
		var newitem = new UIMenuItem("Cook!", "Cook the dish with the appropiate ingredients and ketchup.");
		newitem.SetLeftBadge(UIMenuItem.BadgeStyle.Star);
		newitem.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
		menu.AddItem(newitem);
		menu.OnItemSelect += (sender, item, index) =>
		{
			if (item == newitem)
			{
				string output = ketchup ? "You have ordered ~b~{0}~w~ ~r~with~w~ ketchup." : "You have ordered ~b~{0}~w~ ~r~without~w~ ketchup.";
				Screen.ShowSubtitle(String.Format(output, dish));
			}
		};

		menu.OnIndexChange += (sender, index) =>
		{
			if (sender.MenuItems[index] == newitem)
				newitem.SetLeftBadge(UIMenuItem.BadgeStyle.None);
		};

		var colorItem = new UIMenuItem("UIMenuItem with Colors", "~b~Look!!~r~I can be colored ~y~too!!~w~", Color.FromArgb(150, 185, 230, 185), Color.FromArgb(170, 174, 219, 242));
		menu.AddItem(colorItem);

		var foods = new List<dynamic>
		{
			"Banana",
			"Apple",
			"Pizza",
			"Quartilicious",
			0xF00D, // Dynamic!
        };

		var BlankItem = new UIMenuSeparatorItem();
		menu.AddItem(BlankItem);

		var colorListItem = new UIMenuListItem("Colored ListItem.. Really?", foods, 0, "~b~Look!!~r~I can be colored ~y~too!!~w~", Color.FromArgb(150, 185, 230, 185), Color.FromArgb(170, 174, 219, 242));
		menu.AddItem(colorListItem);


		var SliderProgress = new UIMenuSliderProgressItem("Slider Progress Item", 10, 0);
		menu.AddItem(SliderProgress);

		var Progress = new UIMenuProgressItem("Progress Item", "descriptiom", 10 , 0, true);
		menu.AddItem(Progress);

		var listPanelItem1 = new UIMenuListItem("Change Color", new List<object> { "Example", "example2" }, 0);
		var ColorPanel = new UIMenuColorPanel("Color Panel Example", UIMenuColorPanel.ColorPanelType.Hair);
		// you can choose between hair palette or makeup palette
		menu.AddItem(listPanelItem1);
		listPanelItem1.AddPanel(ColorPanel);

		var listPanelItem2 = new UIMenuListItem("Change Percentage", new List<object> { "Example", "example2" }, 0);
		var PercentagePanel = new UIMenuPercentagePanel("Percentage Panel Example", "0%", "100%");
		// You can change every text in this Panel
		menu.AddItem(listPanelItem2);
		listPanelItem2.AddPanel(PercentagePanel);

		var listPanelItem3 = new UIMenuListItem("Change Grid Position", new List<object> { "Example", "example2" }, 0);
		var GridPanel = new UIMenuGridPanel("Up", "Left", "Right", "Down", new System.Drawing.PointF(.5f, .5f));
		// you can choose the text in every position and where to place the starting position of the cirlce
		// the other 2 grids panel available are not listed as they work the same way but in only 2 direction (horizontally or vertically)
		// and to use them you must stream the stream folder provided with NativeUI project
		menu.AddItem(listPanelItem3);
		listPanelItem3.AddPanel(GridPanel);

		var listPanelItem4 = new UIMenuListItem("Look at Statistics", new List<object> { "Example", "example2" }, 0);
		var statistics = new UIMenuStatisticsPanel();
		statistics.AddStatistics("Look at this!");
		statistics.AddStatistics("I'm a statistic too!");
		statistics.AddStatistics("Am i not?!");
		//you can add as menu statistics you want 
		statistics.SetPercentage(0, 10f);
		statistics.SetPercentage(1, 50f);
		statistics.SetPercentage(2, 100f);
		//and you can get / set their percentage
		menu.AddItem(listPanelItem4);
		listPanelItem4.AddPanel(statistics);

		// THERE ARE NO EVENTS FOR PANELS.. WHEN YOU CHANGE WHAT IS CHANGABLE THE LISTITEM WILL DO WHATEVER YOU TELL HIM TO DO

		menu.OnListChange += (sender, item, index) =>
		{
			if (item == listPanelItem1)
			{
				Screen.ShowNotification("Selected color " + ((item.Panels[0] as UIMenuColorPanel).CurrentSelection + 1) + "...");
				item.Description = "Selected color " + ((item.Panels[0] as UIMenuColorPanel).CurrentSelection + 1) + "...";
				item.Parent.UpdateDescription(); // this is neat.. this will update the description of the item without refresh index.. try it by changing color
			}
			else if (item == listPanelItem2)
			{
				Screen.ShowSubtitle("Percentage = " + (item.Panels[0] as UIMenuPercentagePanel).Percentage + "...");
			}
			else if (item == listPanelItem3)
			{
				Screen.ShowSubtitle("GridPosition = " + (item.Panels[0] as UIMenuGridPanel).CirclePosition + "...");
			}
		};
	}

	public void AddMenuAnotherMenu(UIMenu menu)
	{
		var submenu = _menuPool.AddSubMenu(menu, "Another Menu");
		for (int i = 0; i < 20; i++)
			submenu.AddItem(new UIMenuItem("PageFiller", "Sample description that takes more than one line. Moreso, it takes way more than two lines since it's so long. Wow, check out this length!"));
	}

	public MenuExample()
	{
		_menuPool = new MenuPool();
		var mainMenu = new UIMenu("Native UI", "~b~NATIVEUI SHOWCASE");
		_menuPool.Add(mainMenu);
		HeritageMenu(mainMenu);
		AddMenuKetchup(mainMenu);
		AddMenuFoods(mainMenu);
		AddMenuCook(mainMenu);
		AddMenuAnotherMenu(mainMenu);
		_menuPool.RefreshIndex();

		Tick += async () =>
		{
			_menuPool.ProcessMenus();
			if (Game.IsControlJustPressed(0, Control.SelectCharacterMichael) && !_menuPool.IsAnyMenuOpen()) // Our menu on/off switch
				mainMenu.Visible = !mainMenu.Visible;
		};
	}
}
