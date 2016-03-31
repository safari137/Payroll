using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Payroll.UI
{
    public class MenuEngine
    {
        private MenuItem _currentMenu;
        private bool _runningState;
        private IHistoryManager _historyManager;

        public MenuEngine(MenuItem baseMenu)
        {
            _currentMenu = baseMenu;
            if (_currentMenu.MenuItems.ElementAt(0).Run == null)
            {
                var exitMenu = new MenuItem(MenuItemType.Option, 0, "Exit", this.Exit);
                _currentMenu.RegisterMenu(exitMenu);
            }

            Console.Title = "Payroll 1.0";
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public void Start(IHistoryManager historyManager)
        {
            if (_runningState)
                throw new InvalidOperationException("Menu Engine has already been started.");

            this._historyManager = historyManager;

            this._runningState = true;

            this.CommandPromptLoop();
        }

        public void RegisterMenu(params MenuItem[] newMenus)
        {
            var sortedMenus = newMenus.OrderBy(m => m.Line);

            if (_runningState)
            {
                throw new InvalidOperationException("Cannot register menus, while engine is running.");
            }

            foreach (var newMenu in sortedMenus)
                _currentMenu.RegisterMenu(newMenu);
        }

        private void Exit()
        {
            Console.WriteLine("GoodBye!");
            this._runningState = false;
        }

        private void CommandPromptLoop()
        {
            int command = 0;
            while (_runningState)
            {
                Display();
                Console.Write(">");

                var input = Console.ReadLine();

                int.TryParse(input, out command);

                if (command == 0 && input != "0")
                    throw new InvalidOperationException("Input was invalid.");

                ProcessCommand(command);
            }
        }

        private void Display()
        {
            Console.WriteLine(_currentMenu.Title);
            foreach (var menuItem in _currentMenu.MenuItems)
            {
                Console.WriteLine("{0} : {1}", menuItem.Line, menuItem.Title);
            }            
        }

        private void ProcessCommand(int command)
        {           
            var option = _currentMenu.MenuItems.SingleOrDefault(m => m.Line == command);

            if (option == null)
                return;

            if (option.Type == MenuItemType.Option)
            {
                if (option.Run != null)
                    option.Run();
                else if (option.Line == 0)
                {                    
                    _currentMenu = this._historyManager.GetLast();
                    this._historyManager.Pop();
                }
            }
            else
            {
                _historyManager.Add(_currentMenu);
                _currentMenu = option;
            }
        }
    }
}
